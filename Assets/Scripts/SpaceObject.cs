using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    [SerializeField] public float mass = 1f;
    [SerializeField] public float range = 1f;

    [SerializeField] private RangeCircle circle;

    private void Awake()
    {
        circle = GetComponentInChildren<RangeCircle>();
        transform.localScale = new Vector3(mass, mass, mass);
    }

    private void OnValidate()
    {
        transform.localScale = new Vector3(mass, mass, mass);
    }

}
