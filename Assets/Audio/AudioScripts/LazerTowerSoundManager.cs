using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerTowerSoundManager : MonoBehaviour
{
    private static LazerTowerSoundManager _main;
    const int MAX_SOUND = 5;
    private AudioSource[] sources;


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

    public void PlaySound(Sound s)
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
