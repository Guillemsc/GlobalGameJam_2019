using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClock : Item
{
    private AudioSource audio;
    private PlayerStats player;

    public float buff_duration = 2f;
    public float buff_amount = 50f;

    private Timer timer = new Timer();

    private void Awake()
    {
        InitClock();
    }

    private void InitClock()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(destroyed) {
            if(timer.ReadTime()>buff_duration) 
            {
                player.gameObject.GetComponent<PlayerMovement>().SubDeltaSpeed(buff_amount);
                timer.Reset();
            }
        }
    }

    public override void OnPlayerGrab(PlayerStats player)
    {
        base.OnPlayerGrab(player);
    }

    public override void OnPlayerUses()
    {
        if (!destroyed)
        {
            audio.PlayOneShot(audio.clip);

            timer.Start();
            player = GetGrabbedBy();
            player.gameObject.GetComponent<PlayerMovement>().AddDeltaSpeed(buff_amount);
        }
    }

    private void CustomOnTriggerEnter2D(Collider2D coll)
    {

    }
}
