using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    public void setVolumeMusic(float sliderValue)
    {
        _audioMixer.SetFloat("musicVol", Mathf.Log10(sliderValue) * 20);
    }
    public void setVolumeSFX(float sliderValue)
    {
        _audioMixer.SetFloat("sfxVol", Mathf.Log10(sliderValue) * 20);
    }
}
