using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    [SerializeField] public float mass = 1f;

    private void Awake()
    {
        transform.localScale = new Vector3(mass, mass, mass);
    }

    private void OnValidate()
    {
        transform.localScale = new Vector3(mass, mass, mass);
    }

}
