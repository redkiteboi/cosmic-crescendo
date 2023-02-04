using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    [SerializeField] public float volume = 1f;
    [SerializeField] public float mass = 1f;

    public float mergeRange { get; private set; }
    public bool isOriginal = true;
    public bool isMerging = false;

    [SerializeField] private new LineRenderer renderer;
    private IEnumerator ringFadeAnim;

    private void Awake()
    {
        ringFadeAnim = AnimateFade(0f);
    }

    private void Start()
    {
        OnValidate();
        if (isOriginal) RandomizeMaterial();
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

    public void Select()
    {
        StopCoroutine(ringFadeAnim);
        StartCoroutine(AnimateFade(1));
    }

    public void Deselect()
    {
        StopCoroutine(ringFadeAnim);
        StartCoroutine(AnimateFade(0f));
    }

    private IEnumerator AnimateFade(float alpha)
    {
        Color c = renderer.startColor;
        while(Mathf.Abs(alpha - c.a) >= 0.2f)
        {
            c.a = Mathf.Lerp(c.a, alpha, 0.5f);
            renderer.startColor = c;
            renderer.endColor = c;
            yield return new WaitForFixedUpdate();
        }
        c.a = alpha;
        renderer.startColor = c;
        renderer.endColor = c;
    }

    public void RandomizeMaterial()
    {
        Material m = GetComponent<Renderer>().material;
        switch (m.shader.name)
        {
            case "Shader Graphs/PlanetShader":
                m.SetColor("_Color", Random.ColorHSV());
                m.SetColor("_AtmoColor", Random.ColorHSV());
                break;
            case "Shader Graphs/GasGiant":
                m.SetColor("_BaseColor", Random.ColorHSV());
                m.SetColor("_AtmoColor", Random.ColorHSV());
                break;
            case "Shader Graphs/SunShader":
                Color bass = m.GetColor("_BaseColor");
                Color cell = m.GetColor("_CellColor");

                float bassH, bassS, bassV;
                float cellH, cellS, cellV;

                Color.RGBToHSV(bass, out bassH, out bassS, out bassV);
                Color.RGBToHSV(cell, out cellH, out cellS, out cellV);

                bassH = Random.Range(0f, 1f);
                cellH = Random.Range(0f, 1f);

                bass = Color.HSVToRGB(bassH, bassS, bassV);
                cell = Color.HSVToRGB(cellH, cellS, cellV);

                m.SetColor("_BaseColor", bass);
                m.SetColor("_CellColor", cell);
                break;
            case "Shader Graphs/BlackHole":
                break;
            case "Shader Graphs/Universe":
                break;
            default:
                Debug.LogWarning(string.Format("Uncompatible Shader Material found at GameObject {0}!", gameObject.name));
                break;
        }
    }

}
