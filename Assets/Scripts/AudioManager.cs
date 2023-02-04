using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource part1;
    [SerializeField] private AudioSource part2;
    [SerializeField] private AudioSource part3;
    [SerializeField] private AudioSource part4;

    [SerializeField] private AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("TestAudio");
        part1.Play();
        part2.Play();
        part3.Play();
        part4.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "part1_vol",  2.0f, 1.0f));
        
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "part2_vol",  2.0f, 1.0f));
        
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "part3_vol",  2.0f, 1.0f));
        
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "part4_vol",  2.0f, 1.0f));
        
    }
}
