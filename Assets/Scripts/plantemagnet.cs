using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantemagnet : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject end;
    [SerializeField] private float velocity;
    private float besch = 0.0f;
    private Vector3 begpos;
    
    void Start()
    {
       begpos = transform.position;
        /*
        go = GetComponent<GameObject>();
    */
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        Vector3 endpos = end.transform.position;

        
        transform.position = Vector3.MoveTowards(pos, endpos, velocity + besch );

        /*if(begpos.x )
        besch =+ 0.05f;*/
        /*float endx =endpos.x;
        float endy = endpos.y;
        float endz = endpos.z;*/
    }

    public void movePlanets()
    {
        
        
    }
}
