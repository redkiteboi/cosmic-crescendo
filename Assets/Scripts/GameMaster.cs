using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;

    [SerializeField] private SpaceObject spaceObject;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Debug.LogWarning(string.Format("Second GameMaster instance detected. Deleting {0}!", gameObject.name));
            Destroy(gameObject);
        }
    }

    public static bool MergePlanets(SpaceObject[] objects)
    {
        if (objects[0] == null || objects[1] == null) return false;
        return MergePlanets(objects[0], objects[1]);
    }

    public static bool MergePlanets(SpaceObject object1, SpaceObject object2)
    {
        if(instance == null)
        {
            Debug.LogError("No GameMaster has been established yet!");
            return false;
        }

        float distance = Vector3.Distance(object1.transform.position, object2.transform.position);
        if (object1.mergeRange < distance && object2.mergeRange < distance) return false;

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
    }

    private void SpawnNewPlanet(SpaceObject object1, SpaceObject object2)
    {
        float massSum = object1.mass + object2.mass;
        float relDist = object2.mass / massSum;

        Vector3 position = Vector3.Lerp(object1.transform.position, object2.transform.position, relDist);
        Destroy(object1.gameObject);
        Destroy(object2.gameObject);

        SpaceObject newObject = Instantiate(instance.spaceObject, position, object1.transform.rotation);
        newObject.mass = massSum;
    }
}
