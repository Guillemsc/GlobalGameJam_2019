using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] AudioClip clock_fx;
    [SerializeField] AudioClip bomb_fx;
    [SerializeField] AudioClip wrench_fx;
    [SerializeField] AudioClip magnet_fx;
    [SerializeField] AudioClip shot_fx;

    AudioSource audio_source;

    private void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
