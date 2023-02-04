using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private ArrayList children = new ArrayList();

    private void Awake()
    {
        OnValidate();
        foreach (SpaceObject s in GetComponentsInChildren<SpaceObject>())
        {
            children.Add(s);
        }
    }

    public void OnValidate()
    {
        if (transform.parent == null) return;
        Vector3 scale = transform.parent.localScale;
        scale.x = 1 / scale.x;
        scale.y = 1 / scale.y;
        scale.z = 1 / scale.z;
        transform.localScale = scale;
    }

    private void FixedUpdate()
    {
        transform.Rotate(transform.up, speed);
        foreach (SpaceObject s in children) s.gameObject.transform.LookAt(Vector3.forward * float.PositiveInfinity);
    }

    public void RemoveObject(SpaceObject object1)
    {
        children.Remove(object1);
        if (children.Count <= 0) Destroy(gameObject);
    }

}
