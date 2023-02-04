using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantemagnet : MonoBehaviour
{
    // Start is called before the first frame update

    /*
    [SerializeField] GameObject end;
    */
    [SerializeField] private float velocity;
    [SerializeField] float besch = 0.0f;
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

        Vector3 endpos = new Vector3(-5.7f,0.0f,-57.3f);
        
        
        transform.position = Vector3.MoveTowards(pos, endpos, velocity + besch );
        besch = +besch+0.0001f;

      
    }

    public void movePlanets()
    {
        
        
    }
}
