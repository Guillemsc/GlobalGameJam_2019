using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGun : Item
{
    [SerializeField]
    GameObject SpawnPosition;

    Vector2 spawn_position;

    private void Awake()
    {
        spawn_position = SpawnPosition.GetComponent<Transform>().position;
    }

    public override void OnPlayerGrab(PlayerStats player)
    {
        //base.OnPlayerGrab(player);
    }

    public override void OnPlayerUses()
    {
        //if (!degradated)
        {

            GetComponent<AudioSource>().Play();
        }
    }
}
