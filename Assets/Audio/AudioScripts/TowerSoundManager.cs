//using System.Collections;
//using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class TowerSoundManager : MonoBehaviour
{

    private static TowerSoundManager _main;
    const int MAX_SOUND = 3;
    private AudioSource [] sources;


    void Awake()
    {
        _main = this;
    }

    void Start()
    {
        sources = new AudioSource[MAX_SOUND];
        for (int i = 0; i < MAX_SOUND; i++)
        {
            sources[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    public static void Play(Sound s)
    {
        _main.PlaySound(s);
    }

    public void PlaySound (Sound s)
    {
        s.source = getAudioSource();
        if (s.source == null)
            return;
        s.source.clip = s.clip;

        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.Play();
    }

    private AudioSource getAudioSource()
    {
        foreach (var s in sources)
        {
            if (!s.isPlaying)
                return s;
        }
        return null;
    }
}
