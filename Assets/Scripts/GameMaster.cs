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

        Vector3 position = Vector3.Lerp(object1.transform.position, object2.transform.position, 0.5f);
        Destroy(object1.gameObject);
        Destroy(object2.gameObject);
        Instantiate(instance.spaceObject, position, object1.transform.rotation);
        return true;
    }
}
