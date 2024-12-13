using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public SoundClip[] soundClips;

    public AudioClip BGM;
    public AudioClip BGMChase;
    public AudioClip SFXButtonClick;
    
    private bool _played = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (SoundClip sound in soundClips)
        {
            sound.source = gameObject.AddComponent<AudioSource>(); 
            sound.source.clip = sound.clip;
        }
    }
    
    public void Play(string name)
    {
        SoundClip sound = Array.Find(soundClips, sound => sound.name == name);
        PlaySFX(sound.clip);
    }
    
    private void Start() {
        PlayBGM();
    }

    public void StopBGM()
    {
        musicSource.Stop();
    }

    public void Chase()
    {
        musicSource.clip = BGMChase;

        if(_played == false) 
        {
            _played = true;
            musicSource.Play();
        }
    }

    public void PlayBGM()
    {
        _played = false;
        musicSource.clip = BGM;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }
}