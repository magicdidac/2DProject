using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{

    public string name;
    public AudioClip clip;
    public bool loop;
    public bool playOnAwake;
    public AudioMixerGroup mixer;
    public bool ignoreListenerPause;

    [Range(0f, 1f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;

}

