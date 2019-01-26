using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    AudioSource audio_source;

    MusicManager()
    {
        InitInstance(this);
    }

    private void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        audio_source.Play();
    }
}
