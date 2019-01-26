using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : Item
{
    List<PlayerStats> collided_go;

    private void Awake()
    {
        InitCone();
        collided_go = new List<PlayerStats>();
    }

    private void InitCone()
    {
        CollisionDetector cd = gameObject.GetComponentInChildren<CollisionDetector>();
        cd.SuscribeOnTriggerEnter2D(CustomOnTriggerEnter2D);
        cd.SuscribeOnTriggerExit2D(CustomOnTriggerExit2D);
    }

    public override void OnPlayerGrab(PlayerStats player)
    {
        base.OnPlayerGrab(player);
    }

    public override void OnPlayerUses()
    {
        if (collided_go.Count > 0 && !destroyed)
        {
            PlayerStats ps = collided_go[0];

            PlayerStats thief = GetGrabbedBy();
            Item item_to_get = ps.GetGrabbedItem();

            ItemManager.Instance.StopGrabbingItem(thief);
            ItemManager.Instance.StopGrabbingItem(ps);
            ItemManager.Instance.StartGrabbingItem(thief, item_to_get);

            GetComponentInChildren<SpriteRenderer>().color = Color.red;

            GetComponent<AudioSource>().Play();
        }
    }

    private void CustomOnTriggerEnter2D(Collider2D coll)
    {
        PlayerStats ps = coll.GetComponent<PlayerStats>();

        if (ps != null)
            collided_go.Add(ps);
    }

    private void CustomOnTriggerExit2D(Collider2D coll)
    {
        PlayerStats ps = coll.GetComponent<PlayerStats>();

        if (ps != null)
            collided_go.Remove(ps);
    }
}
