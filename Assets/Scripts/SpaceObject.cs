using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpaceObject : MonoBehaviour
{
    [SerializeField] public SpaceObjectType type;
    [SerializeField] public float volume = 1f;
    [SerializeField] public float mass = 1f;
    [SerializeField] private float multiplier = 1f;
    [SerializeField] private ParticleSystem poof;
    public float mergeRange { get; private set; }
    public bool isOriginal = true;
    public bool isMerging = false;
    public int layer = 0;

    [SerializeField] private new LineRenderer renderer;
    private IEnumerator ringFadeAnim;
    private TrailRenderer trail;

    #if UNITY_WEBGL
    [SerializeField] private Mesh altMesh;
#endif

    private void Awake()
    {
        ringFadeAnim = AnimateFade(0f);
        trail = GetComponentInChildren<TrailRenderer>();

        #if UNITY_WEBGL
        //Switch Main Mesh to UV_Sphere
        MeshFilter filter = GetComponent<MeshFilter>();
        if (filter != null && type != SpaceObjectType.Asteroid && altMesh != null) filter.sharedMesh = altMesh;
        
        //Switch Sub Meshes to UV_Sphere
        if (type == SpaceObjectType.BlackHole)
        {
            MeshFilter[] subFilters = GetComponentsInChildren<MeshFilter>();
            if (subFilters[1] != null && subFilters[1].sharedMesh.name == "Sphere" && altMesh != null) subFilters[1].sharedMesh = altMesh;
        }
        #endif
    }

    private void Start()
    {
        OnValidate();

        SpaceLayer l = transform.parent?.GetComponent<SpaceLayer>();
        if (l != null) layer = l.Layer;

        if (trail)
        {
            float width = Vector3.Distance(transform.position, Vector3.zero) * 0.06125f;
            trail.startWidth = width;
            trail.endWidth = 0f;
        }
        if (isOriginal) RandomizeMaterial();
    }

    private void OnValidate()
    {
        transform.localScale = new Vector3(volume, volume, volume);
        mergeRange = mass * multiplier;
        DrawCircle(100, mergeRange);
        foreach(Orbit o in GetComponentsInChildren<Orbit>())
        {
            o.OnValidate();
        }
    }

    private void DrawCircle(int corners, float radius)
    {
        renderer.positionCount = corners;

        float width;
        if (Camera.main != null) width = Vector3.Distance(Camera.main.transform.position, Vector3.zero) * 0.00625f;
        else width = 0.06f;
        renderer.startWidth = width;
        renderer.endWidth = width;

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
        DrawCircle(100, mergeRange);
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
        switch (type)
        {
            case SpaceObjectType.Asteroid:
                break;
            case SpaceObjectType.RockPlanet:
                m.SetColor("_Color", Random.ColorHSV(0, 1, 0, 1, 0.25f, 1));
                m.SetColor("_AtmoColor", Random.ColorHSV());
                break;
            case SpaceObjectType.GasGiant:
                m.SetColor("_BaseColor", Random.ColorHSV(0, 1, 0, 1, 0.25f, 1));
                m.SetColor("_AtmoColor", Random.ColorHSV());
                break;
            case SpaceObjectType.Star:
                Color bass = m.GetColor("_BaseColor");
                Color cell = m.GetColor("_CellColor");

                float bassH, bassS, bassV;
                float cellH, cellS, cellV;

                Color.RGBToHSV(bass, out bassH, out bassS, out bassV);
                Color.RGBToHSV(cell, out cellH, out cellS, out cellV);

                float c;
                if (Random.Range(0f, 1f) >= 0.5f) c = Random.Range(0f, 0.2f);
                else c = Random.Range(0.5f, 0.666f);
                bassH = c;
                cellH = c;

                bassS = Random.Range(0.75f, 1f);
                cellS = Random.Range(0.75f, 1f);

                bass = Color.HSVToRGB(bassH, bassS, bassV);
                cell = Color.HSVToRGB(cellH, cellS, cellV);

                m.SetColor("_BaseColor", bass);
                m.SetColor("_CellColor", cell);
                break;
            case SpaceObjectType.BlackHole:
                break;
            case SpaceObjectType.Galaxy:
                break;
            case SpaceObjectType.Universe:
                break;
            default:
                Debug.LogWarning(string.Format("Unknown SpaceObjectType found at GameObject {0}!", gameObject.name));
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        SpaceObject other = collision.gameObject.GetComponent<SpaceObject>();
        if (other != null && other.volume > volume)
            Instantiate(poof.gameObject, collision.GetContact(0).point, collision.transform.rotation).transform.localScale *= volume;

        /*
        VisualEffect colPoof = Instantiate(poof, collision.GetContact(0).point, collision.transform.rotation);
        colPoof.SetFloat("Scaling", volume * 0.6f);
        colPoof.Play();
        */
    }

    private void OnDestroy()
    {
        if (transform.parent == null) return;
        Orbit o = transform.parent.GetComponent<Orbit>();
        if (o) o.RemoveObject(this);
    }

}
