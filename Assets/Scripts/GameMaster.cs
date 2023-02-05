using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;

    [SerializeField] private int il;
    [SerializeField] public MainCam cam;
    [SerializeField] private SpaceObject defaultObj;
    [SerializeField] private SpaceObject defaultGalaxy;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private VisualEffect poofEffect;
    [SerializeField] private int mergeCount = 0;
    [SerializeField] private bool isPaused = true;
    [SerializeField] private float introTimer = 3f;

    private ArrayList spaceObjects = new ArrayList();

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
        StartCoroutine(IntroSequence());
        audioManager.SetLayer(currentLayer);
    }

    private IEnumerator IntroSequence()
    {
        yield return new WaitForSeconds(introTimer);
        isPaused = false;
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

        float massSum = object1.mass + object2.mass;
        float relDist = object2.mass / massSum;
        Vector3 mergePoint = Vector3.Lerp(pos1, pos2, relDist);

        while (distance >= 1f)
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
            audioManager.SetLayer(currentLayer);
            if (currentLayer >= 5) Debug.Log("You are Winner!");
        }
    }

    private void SpawnNewPlanet(SpaceObject object1, SpaceObject object2)
    {
        float massSum = object1.mass + object2.mass;
        float relDist = object2.mass / massSum;
        float volumeSum = object1.volume + object2.volume;
        Material mat;
        SpaceObjectType type;

        if (object1.mass >= object2.mass)
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
        else newObject = Instantiate(instance.defaultObj, position, object1.transform.rotation);
        newObject.type = type;
        newObject.mass = massSum;
        newObject.volume = volumeSum;
        newObject.isOriginal = false;
        newObject.GetComponent<Renderer>().material = mat;

        VisualEffect mergePoof = Instantiate(poofEffect, newObject.transform);
        mergePoof.SetFloat("Scaling", volumeSum * 0.75f);
        mergePoof.Play();
    }

    private void AdjustCam(Vector3 pos)
    {
        cam.goalPos = pos;
    }

}
