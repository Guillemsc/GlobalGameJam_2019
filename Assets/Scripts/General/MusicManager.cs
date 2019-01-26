using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource main_music_track;
    [SerializeField] AudioClip clock_fx;
    [SerializeField] AudioClip bomb_fx;
    [SerializeField] AudioClip wrench_fx;
    [SerializeField] AudioClip magnet_fx;
    [SerializeField] AudioClip shot_fx;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            main_music_track.PlayOneShot(clock_fx);
        }
    }
}
