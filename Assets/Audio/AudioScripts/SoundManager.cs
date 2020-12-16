//using System.Collections;
//using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private static SoundManager main;
    private static AudioSource source;
    // Start is called before the first frame update
    void Awake()
    {
        main = this;
        source = GetComponent<AudioSource>();
    }

    public static void Play (AudioClip sound)
    {
        source.clip = sound;
        source.Play();
    }
}
