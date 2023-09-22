using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpin : MonoBehaviour
{
    [SerializeField] [Range(-0.01f, 0.01f)] private float spin = 0f;

    private void FixedUpdate()
    {
        transform.RotateAround(Vector3.up, spin);
    }
}
