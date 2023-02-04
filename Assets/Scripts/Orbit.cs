using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private ArrayList children = new ArrayList();

    private void Awake()
    {
        foreach (SpaceObject s in GetComponentsInChildren<SpaceObject>())
        {
            children.Add(s);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(transform.up, speed);
        foreach (SpaceObject s in children) s.gameObject.transform.LookAt(Vector3.forward * float.PositiveInfinity);
    }

    public void RemoveObject(SpaceObject object1)
    {
        children.Remove(object1);
    }

}
