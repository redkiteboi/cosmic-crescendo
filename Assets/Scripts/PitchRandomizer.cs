using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchRandomizer : MonoBehaviour
{
    void Awake()
    {
        float randomNumber = Random.Range(.2f, 2.5f);
        gameObject.GetComponent<AudioSource>().pitch = randomNumber;
    }

}
