using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class plantemagnet : MonoBehaviour
{
    // Start is called before the first frame update

    /*
    [SerializeField] GameObject end;
    */
    [SerializeField] private float velocity;
    [SerializeField] float besch = 0.0f;
    private Vector3 begpos;
    private Collider col;
    
    void Start()
    {
       begpos = transform.position;
       if (GetComponent<Renderer>() != null)
       {
           RandomizeMaterial();
       }

       Time.timeScale = 0;
    }

    private void Update()
    {
        if (MainMenu.gameStarted)
        {
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        if (MainMenu.gameStarted)
        {
            Vector3 pos = transform.position;

            Vector3 target = new Vector3(-5.7f,0.0f,-57.3f);
      
            transform.position = Vector3.MoveTowards(pos, target, velocity + besch);
            besch = +besch+0.0001f;
        }
      
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Equals("blackhole"))
        gameObject.SetActive(false);
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
