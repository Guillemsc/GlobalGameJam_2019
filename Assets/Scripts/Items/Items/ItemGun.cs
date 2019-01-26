using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGun : Item
{
    [SerializeField]
    GameObject SpawnPosition;
    [SerializeField]
    GameObject Collision;

    Vector2 spawn_position;

    private void Awake()
    {
        spawn_position = SpawnPosition.GetComponent<Transform>().position;
        InitGun();
    }

    private void InitGun()
    {
        CollisionDetector cd = Collision.GetComponent<CollisionDetector>();
        cd.SuscribeOnCollisionEnter2D(CustomOnCollisionEnter2D);
    }

 
    public override void OnPlayerGrab(PlayerStats player)
    {
        //base.OnPlayerGrab(player);
    }

    public override void OnPlayerUses()
    {
        if (!destroyed)
        {
            Collision.GetComponent<CircleCollider2D>().enabled = true;
            GetComponent<AudioSource>().Play();
        }
    }

    private void CustomOnCollisionEnter2D(Collision2D coll)
    {

        //if (coll)
    }
}
