using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance { get; private set; }

    [SerializeField] private int il;
    [SerializeField] public MainCam cam;
    [SerializeField] private SpaceObject defaultObj;
    [SerializeField] private SpaceObject defaultGalaxy;
    [SerializeField] private SpaceObject defaultBlackHole;
    [SerializeField] private Material asteroidMat;
    [SerializeField] private ParticleSystem poofEffect;
    [SerializeField] private ParticleSystem winEffect;
    [SerializeField] private int mergeCount = 0;
    [SerializeField] private bool isPaused = false;

    private ArrayList spaceObjects = new ArrayList();

    public int CurrentLayer
    {
        get => currentLayer;
        private set => currentLayer = value;
    }
    [SerializeField] private int currentLayer = 0;
    [SerializeField] private int[] layerRequirements = new int[5];
    [SerializeField] private Vector3[] layerCamPos = new Vector3[6];

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Debug.LogWarning(string.Format("Second GameMaster instance detected. Deleting {0}!", gameObject.name));
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        cam.goalPos = layerCamPos[0];
        if (PlayerPrefs.GetInt("SkipIntro") == 1)
            cam.transform.position = Vector3.Lerp(cam.transform.position, layerCamPos[0], 0.999f);
        AudioManager.SetLayer(currentLayer);
    }

    public static void SetPaused(bool pause)
    {
        if (instance != null) instance.isPaused = pause;
    }

    public static bool GetPaused()
    {
        if (instance != null) return instance.isPaused;
        else return true;
    }

    public void RegisterPlanet(SpaceObject object1)
    {
        spaceObjects.Add(object1);
    }

    public static bool MergePlanets(SpaceObject[] objects)
    {
        if (objects[0] == null || objects[1] == null) return false;
        if (objects[0] == objects[1]) return false;
        return MergePlanets(objects[0], objects[1]);
    }

    public static bool MergePlanets(SpaceObject object1, SpaceObject object2)
    {
        if(instance == null)
        {
            Debug.LogError("No GameMaster has been established yet!");
            return false;
        }

        if (instance.isPaused) return false;

        if (object1.isMerging || object2.isMerging) return false;

        if (object1.layer > instance.currentLayer || object2.layer > instance.currentLayer) return false;

        if (object1.GetComponentInChildren<Orbit>() != null || object2.GetComponentInChildren<Orbit>() != null) return false;

        float distance = Vector3.Distance(object1.transform.position, object2.transform.position);
        if (object1.mergeRange < distance && object2.mergeRange < distance) return false;

        object1.isMerging = true;
        object2.isMerging = true;
        instance.StartCoroutine(instance.AnimateMerge(object1, object2));

        return true;
    }

    private IEnumerator AnimateMerge(SpaceObject object1, SpaceObject object2)
    {
        Vector3 pos1 = object1.transform.position;
        Vector3 pos2 = object2.transform.position;
        float distance = Vector3.Distance(pos1, pos2);
        float ogDistance = distance;

        float massSum = object1.mass + object2.mass;
        float relDist = object2.mass / massSum;
        Vector3 mergePoint = Vector3.Lerp(pos1, pos2, relDist);

        while (distance > ogDistance / 8f)
        {
            object1.transform.position = Vector3.Lerp(pos1, mergePoint, 0.025f);
            object2.transform.position = Vector3.Lerp(pos2, mergePoint, 0.025f);

            pos1 = object1.transform.position;
            pos2 = object2.transform.position;

            distance = Vector3.Distance(pos1, pos2);

            yield return new WaitForFixedUpdate();
        }
        
        SpawnNewPlanet(object1, object2);
        
        mergeCount++;
        if (mergeCount >= layerRequirements[currentLayer])
        {
            mergeCount = 0;
            AdjustCam(layerCamPos[++currentLayer]);
            AudioManager.SetLayer(currentLayer);
            if (currentLayer >= 5) WinGame();
        }
    }

    private void SpawnNewPlanet(SpaceObject object1, SpaceObject object2)
    {
        float massSum = object1.mass + object2.mass;
        float relDist = object2.mass / massSum;
        float volumeSum = object1.volume + object2.volume;
        Material mat;
        SpaceObjectType type;

        if (object1.type == SpaceObjectType.Asteroid && object2.type == SpaceObjectType.Asteroid)
        {
            type = SpaceObjectType.RockPlanet;
            mat = asteroidMat;
        }
        else if (object1.type == SpaceObjectType.BlackHole)
        {
            type = SpaceObjectType.BlackHole;
            mat = object1.GetComponent<Renderer>().material;
        }
        else if (object2.type == SpaceObjectType.BlackHole)
        {
            type = SpaceObjectType.BlackHole;
            mat = object2.GetComponent<Renderer>().material;
        }
        else if (object1.mass >= object2.mass)
        {
            type = object1.type;
            mat = object1.GetComponent<Renderer>().material;
        }
        else
        {
            type = object2.type;
            mat = object2.GetComponent<Renderer>().material;
        }
        

        Vector3 position = Vector3.Lerp(object1.transform.position, object2.transform.position, relDist);
        Destroy(object1.gameObject);
        Destroy(object2.gameObject);

        SpaceObject newObject;
        if (type == SpaceObjectType.Galaxy) newObject = Instantiate(instance.defaultGalaxy, position, object1.transform.rotation);
        else if (type == SpaceObjectType.BlackHole) newObject = Instantiate(instance.defaultBlackHole, position, object1.transform.rotation);
        else newObject = Instantiate(instance.defaultObj, position, object1.transform.rotation);
        newObject.type = type;
        newObject.mass = massSum;
        newObject.volume = volumeSum;
        newObject.isOriginal = false;
        newObject.GetComponent<Renderer>().material = mat;

        Instantiate(poofEffect.gameObject, newObject.transform).transform.localScale *= volumeSum * 0.75f;
        /*
        VisualEffect mergePoof = Instantiate(poofEffect, newObject.transform);
        mergePoof.SetFloat("Scaling", volumeSum * 0.75f);
        mergePoof.Play();
        */
    }

    private void AdjustCam(Vector3 pos)
    {
        cam.goalPos = pos;
    }

    private void WinGame()
    {
        StartCoroutine(WinAnim());
    }

    private IEnumerator WinAnim()
    {
        yield return new WaitForSeconds(1.5f);
        winEffect.Play();
        Instantiate(poofEffect.gameObject, Vector3.zero, transform.rotation).transform.localScale *= 1000000f;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
        MainMenu.gameStarted = false;
    }

}
