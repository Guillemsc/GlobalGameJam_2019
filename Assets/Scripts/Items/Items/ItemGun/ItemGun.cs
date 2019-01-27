using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGun : Item
{
    private void Awake()
    {
        InitGun();
    }

    private void InitGun()
    {
        CollisionDetector cd = GetComponent<CollisionDetector>();
        cd.SuscribeOnTriggerEnter2D(CustomOnTriggerEnter2D);
    }

    private void Update()
    { 

    }

    public override void OnPlayerGrab(PlayerStats player)
    {
        
    }

    public override void OnPlayerUses()
    {
        ShootBullet();
    }

    private void CustomOnTriggerEnter2D(Collider2D coll)
    {

    }

    private void ShootBullet()
    {
        GameObject ins = Instantiate(bullet_prefab, gameObject.transform.position, Quaternion.identity);

        GunBullet bull = ins.GetComponent<GunBullet>();

        if(bull != null)
        {
            PlayerStats owner = GetGrabbedBy();

            if(owner != null)
            {
                PlayerActions pa = owner.gameObject.GetComponent<PlayerActions>();

                bull.SetMovementData(pa.GetInputAngle(), pa.GetInputDirectionVector(), bullet_speed);
                bull.SetShooter(owner);
            }
        }
    }

    [SerializeField]
    private GameObject bullet_prefab = null;

    [SerializeField]
    private float bullet_speed = 0.0f;
}
