using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClock : Item
{
    private void Awake()
    {
        InitClock();
    }

    private void InitClock()
    {
        CollisionDetector cd = GetComponent<CollisionDetector>();
        cd.SuscribeOnTriggerEnter2D(CustomOnTriggerEnter2D);
    }

    private void Update()
    {

    }

    public override void OnPlayerGrab(PlayerStats player)
    {
        base.OnPlayerGrab(player);
    }

    public override void OnPlayerUses()
    {
        if (!destroyed)
        {
            
        }
    }

    private void CustomOnTriggerEnter2D(Collider2D coll)
    {

    }
}
