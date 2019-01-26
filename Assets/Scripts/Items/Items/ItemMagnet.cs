using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : Item
{
    bool magnet_used = false;

    private void Awake()
    {
        InitItem();

        InitCone();
    }

    private void InitItem()
    {
        CircleCollider2D circle = gameObject.GetComponent<CircleCollider2D>();
        
        Init(circle);
    }

    private void InitCone()
    {
        CollisionDetector cd = gameObject.GetComponentInChildren<CollisionDetector>();
        cd.SuscribeOnTriggerEnter2D(CustomOnTriggerEnter2D);
    }


    public override void OnPlayerGrab(PlayerStats player)
    {
        base.OnPlayerGrab(player);
    }

    public override void OnPlayerUses()
    {
        magnet_used = true;
    }

    private void CustomOnTriggerEnter2D(Collider2D coll)
    {
        PlayerStats ps = coll.GetComponent<PlayerStats>();

        if (ps != null && magnet_used)
        {
            // Degradar item
            ItemManager.Instance.StopGrabbingItem(GetGrabbedBy());
            ItemManager.Instance.StartGrabbingItem(ps, ps.GetGrabbedItem());
            ItemManager.Instance.StopGrabbingItem(ps);

            Debug.Log("Item robbed");

            magnet_used = false;
        }
    }
}
