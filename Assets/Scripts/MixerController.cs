using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    private void Start()
    {
        float musicVol = PlayerPrefs.GetFloat("musicVol");
        float sfxVol = PlayerPrefs.GetFloat("sfxVol");
        _musicSlider.value = musicVol == 0f ? 0.8f : musicVol;
        _sfxSlider.value = sfxVol == 0f ? 0.8f : sfxVol;
    }

    public void SetVolumeMusic(float sliderValue)
    {
        _audioMixer.SetFloat("musicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("musicVol", sliderValue);
    }
    public void SetVolumeSFX(float sliderValue)
    {
        _audioMixer.SetFloat("sfxVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("sfxVol", sliderValue);
    }
}
