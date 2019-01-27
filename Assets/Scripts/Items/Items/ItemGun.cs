using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGun : Item
{
    [SerializeField]
    GameObject SpawnPosition;

    Vector2 spawn_position;

    bool is_projectile = false;
    Vector2 dir_vec;
    float speed = 10.0f;

    private void Awake()
    {
        spawn_position = SpawnPosition.GetComponent<Transform>().position;
        InitGun();
    }

    private void InitGun()
    {
        CollisionDetector cd = GetComponent<CollisionDetector>();
        cd.SuscribeOnTriggerEnter2D(CustomOnTriggerEnter2D);
    }

    private void Update()
    {
        if (is_projectile)
        {
            transform.position = new Vector2(transform.position.x + dir_vec.x, transform.position.y + dir_vec.y) * speed * Time.deltaTime;
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
            ItemManager.Instance.StopGrabbingItem(GetGrabbedBy());
            GetComponent<AudioSource>().Play();

            dir_vec = GetGrabbedBy().GetComponent<PlayerActions>().GetDirectionVector();
            is_projectile = true;
        }
    }

    private void CustomOnTriggerEnter2D(Collider2D coll)
    {
        if (is_projectile)
        {
            PlayerStats ps = coll.GetComponent<PlayerStats>();

            if (ps != null && ps != GetComponent<PlayerStats>())
            {
                ItemManager.Instance.StopGrabbingItem(ps);
                GetComponent<AudioSource>().Play();
                is_projectile = false;
                dir_vec = Vector2.zero;
            }
        }
    }
}
