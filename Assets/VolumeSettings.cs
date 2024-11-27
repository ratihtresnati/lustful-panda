using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class VolumeSettings : MonoBehaviour
{
    public static VolumeSettings instance;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start() {
    if(PlayerPrefs.HasKey("musicVolume")){
            LoadVolume();
        }
    else{
            SetMusicVolume();
        }
    }

    public void SetMusicVolume(){
        float volume;

        if (musicSlider == null) {
            if (PlayerPrefs.HasKey("musicVolume")) {
                volume = PlayerPrefs.GetFloat("musicVolume");
            } else {
                volume = 0.5f;
            }
        } else {
            volume = musicSlider.value;
        }

        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void SetSFXVolume(){
        float volume;

        if (sfxSlider == null) {
            if (PlayerPrefs.HasKey("sfxVolume")) {
                volume = PlayerPrefs.GetFloat("sfxVolume");
            } else {
                volume = 0.5f;
            }
        } else {
            volume = sfxSlider.value;
        }

        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    public void LoadVolume(){
        if (musicSlider == null && sfxSlider == null) {
            SetMusicVolume();
            SetSFXVolume();
            return;
        }

        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        SetMusicVolume();
        SetSFXVolume();
    }
}
