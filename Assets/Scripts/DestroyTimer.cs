using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField] private float timer = 4f;

    private void Start()
    {
        Destroy(gameObject, timer);
    }
}
