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

    public AudioClip music;

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

    private void Start()
    {
        musicSource.clip = music;
        musicSource.Play();
    }

    public void Play(string name)
    {
        PlayableSound sound = sounds.Find(x => x.name == name);

        if (sound != null)
        {
            source.PlayOneShot(sound.clip);
        }
    }
}
