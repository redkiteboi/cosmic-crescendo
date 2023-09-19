using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceLayer : MonoBehaviour
{
    public int Layer
    {
        get => layer;
        private set => layer = value;
    }

    [SerializeField] private int layer;
}
