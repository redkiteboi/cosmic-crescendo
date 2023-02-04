using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    [SerializeField] public float volume = 1f;
    [SerializeField] public float mass = 1f;

    public float mergeRange { get; private set; }

    [SerializeField] private new LineRenderer renderer;

    private void Start()
    {
        OnValidate();
        Material m = GetComponent<Renderer>().material;
        Debug.Log(m.shader.name);
        switch (m.shader.name)
        {
            case "Shader Graphs/PlanetShader":
                Debug.Log("Planeto");
                m.SetColor("_Color", Random.ColorHSV());
                m.SetColor("_AtmoColor", Random.ColorHSV());
                break;
        }
    }

    private void OnValidate()
    {
        transform.localScale = new Vector3(volume, volume, volume);
        mergeRange = mass;
        DrawCircle(100, mass);
    }

    private void DrawCircle(int corners, float radius)
    {
        renderer.positionCount = corners;
        for (int i = 0; i < corners; i++)
        {
            float circumference = (float)i / corners;
            float radian = circumference * 2 * Mathf.PI;
            float x = Mathf.Cos(radian) * radius / transform.localScale.x;
            float z = Mathf.Sin(radian) * radius / transform.localScale.x;
            Vector3 pos = new Vector3(x, 0, z);
            renderer.SetPosition(i, pos);
        }
    }

}
