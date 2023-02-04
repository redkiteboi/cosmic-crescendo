using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyRing : MonoBehaviour
{
    private void Start()
    {
        transform.localScale *= transform.parent.parent.transform.localScale.x;
    }
}
