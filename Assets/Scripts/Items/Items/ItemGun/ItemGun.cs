using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGun : Item
{
    private void Awake()
    {
        InitGun();
        InitEvents();
    }

    private void InitGun()
    {
        CollisionDetector cd = GetComponent<CollisionDetector>();
        cd.SuscribeOnTriggerEnter2D(CustomOnTriggerEnter2D);
    }

    private void InitEvents()
    {
        EventManager.Instance.Suscribe(GameEventType.EVENT_BULLET_HITS_PLAYER, OnEvent);
    }

    private void OnEvent(GameEvent ev)
    {
        switch(ev.Type())
        {
            case GameEventType.EVENT_BULLET_HITS_PLAYER:
                {
                    EventBulletHitsPlayer r_ev = (EventBulletHitsPlayer)ev;

                    ItemManager.Instance.DestroyItem(r_ev.hit.GetGrabbedItem());

                    break;
                }
        }
    }

    public override void OnPlayerGrab(PlayerStats player)
    {
        
    }

    public override void OnPlayerUses()
    {
        if(!used)
            ShootBullet();
    }

    private void CustomOnTriggerEnter2D(Collider2D coll)
    {

    }

    private void ShootBullet()
    {
        GameObject ins = Instantiate(bullet_prefab, bullet_start_pos.transform.position, Quaternion.identity);

        GunBullet bull = ins.GetComponent<GunBullet>();

        if(bull != null)
        {
            PlayerStats owner = GetGrabbedBy();

            if(owner != null)
            {
                PlayerActions pa = owner.gameObject.GetComponent<PlayerActions>();

                bull.SetMovementData(pa.GetItemAngle(), pa.GetItemDirectionVector(), bullet_speed);
                bull.SetShooter(owner);

                used = true;
            }
        }
    }

    [SerializeField]
    private GameObject bullet_prefab = null;

    [SerializeField]
    private GameObject bullet_start_pos = null;

    [SerializeField]
    private float bullet_speed = 0.0f;

    private bool used = false;
}
