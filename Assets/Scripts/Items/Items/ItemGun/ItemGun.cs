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

    }

    private void CustomOnTriggerEnter2D(Collider2D coll)
    {

    }

    [SerializeField]
    private GameObject bullet_prefab = null;
}
