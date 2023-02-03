using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    [SerializeField] public float mass = 1f;
    [SerializeField] public float range = 1f;

    [SerializeField] private new LineRenderer renderer;

    private void Start()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        transform.localScale = new Vector3(mass, mass, mass);
        DrawCircle(100, mass * range);
    }

    private void DrawCircle(int corners, float radius)
    {
        renderer.positionCount = corners;
        for (int i = 0; i < corners; i++)
        {
            float circumference = (float)i / corners;
            float radian = circumference * 2 * Mathf.PI;
            float x = Mathf.Cos(radian) * radius;
            float z = Mathf.Sin(radian) * radius;
            Vector3 pos = new Vector3(x, 0, z);
            renderer.SetPosition(i, pos);
        }
    }

}
