using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayableSound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource source;
    public AudioSource musicSource;
    public AudioSource thrustSource;

    public JuicyToggler sfxToggler;
    public JuicyToggler musicToggler;

    public List<PlayableSound> sounds = new List<PlayableSound>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    public void Update()
    {
        if (!musicToggler.toggleState)
        {
            musicSource.volume = 0f;
        }
        else
        {
            musicSource.volume = 1f;
        }

        if (!sfxToggler.toggleState)
        {
            thrustSource.volume = 0f;
        }
        else
        {
            thrustSource.volume = 1f;
        }
    }

    public void Play(string name)
    {
        if (!sfxToggler.toggleState) return;

        PlayableSound sound = sounds.Find(x => x.name == name);

        if (sound != null)
        {
            source.PlayOneShot(sound.clip);
        }
    }
}
