using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [SerializeField] private AudioSource mainmenu;
    [SerializeField] private AudioSource part1;
    [SerializeField] private AudioSource part2;
    [SerializeField] private AudioSource part3;
    [SerializeField] private AudioSource part4;    
    [SerializeField] private AudioSource part5;
    [SerializeField] private AudioSource endsequence;
    
    [SerializeField] private AudioSource start;


    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        if (instance != null)
        {
            instance.FadeOut();
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        mainmenu.Play();
        part1.Play();
        part2.Play();
        part3.Play();
        part4.Play();
        part5.Play();
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "mainmenu_vol", 5.0f, 1.0f));
    }

    public static void SetLayer(int layer)
    {
        if (instance != null) instance.SetLayerInternal(layer);
    }

    private void SetLayerInternal(int layer)
    {
        switch (layer)
        {
            case 0:
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "part5_vol", .5f, 0f));
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "part4_vol", .5f, 0f));
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "part3_vol", .5f, 0f));
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "part2_vol", .5f, 0f));
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "part1_vol", 5.0f, 1.0f));
                break;
            case 1:
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "part2_vol", 2.0f, 1.0f));
                break;
            case 2:
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "part3_vol", 2.0f, 1.0f));
                break;
            case 3:
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "part4_vol", 2.0f, 1.0f));
                break;
            case 4:
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "part5_vol", 2.0f, 1.0f));
                break;
            case 5:
                endsequence.Play();
                FadeOut();
                break;
            default:
                Debug.LogError("Invalid layer number!");
                break;
        }
    }

    private void FadeOut()
    {
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "part5_vol", .5f, 0f));
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "part4_vol", .5f, 0f));
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "part3_vol", .5f, 0f));
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "part2_vol", .5f, 0f));
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "part1_vol", .5f, 0f));
    }
}
