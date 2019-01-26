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
        EventManager.Instance.Suscribe(GameEventType.EVENT_MAP_LOAD, OnEvent);
    }

    public void PlayMusic()
    {
        audio_source.Play();
    }

    public void OnEvent(GameEvent ev)
    {
        switch (ev.Type())
        {
            case GameEventType.EVENT_MAP_LOAD:
                PlayMusic();
                break;
        }
    }
}
