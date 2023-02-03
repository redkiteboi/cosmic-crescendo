using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RangeCircle : MonoBehaviour
{
    [SerializeField] private new LineRenderer renderer;
    [SerializeField] private SpaceObject parent;

    private void Start()
    {
        DrawCircle(100, parent.range);
    }

    private void OnValidate()
    {
        DrawCircle(100, parent.range);
    }

    private void DrawCircle(int corners, float radius)
    {
        renderer.positionCount = corners;
        for (int i = 0; i < corners; i++)
        {
            float circumference = (float) i / corners;
            float radian = circumference * 2 * Mathf.PI;
            float x = Mathf.Cos(radian) * radius;
            float z = Mathf.Sin(radian) * radius;
            Vector3 pos = new Vector3(x, 0, z);
            renderer.SetPosition(i, pos);
        }
    }
}
