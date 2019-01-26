using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : Item
{
    bool magnet_used = false;
    bool degradated = false;

    private void Awake()
    {
        InitCone();
    }

    private void InitCone()
    {
        CollisionDetector cd = gameObject.GetComponentInChildren<CollisionDetector>();
        cd.SuscribeOnTriggerStay2D(CustomOnTriggerStay2D);
    }

    public override void OnPlayerGrab(PlayerStats player)
    {
        base.OnPlayerGrab(player);
    }

    public override void OnPlayerUses()
    {
        Debug.Log("used item");
        magnet_used = true;
    }

    private void CustomOnTriggerStay2D(Collider2D coll)
    {
        if (!degradated)
        {
            PlayerStats ps = coll.GetComponent<PlayerStats>();

            if (ps != null && magnet_used)
            {
                // Degradar item
                PlayerStats thief = GetGrabbedBy();
                Item item_to_get = ps.GetGrabbedItem();

                ItemManager.Instance.StopGrabbingItem(thief);
                ItemManager.Instance.StopGrabbingItem(ps);
                ItemManager.Instance.StartGrabbingItem(thief, item_to_get);

                degradated = true;
                magnet_used = false;

                GetComponentInChildren<SpriteRenderer>().color = Color.red;
            }
        }
    }
}
