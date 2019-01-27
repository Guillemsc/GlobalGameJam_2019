using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWrench : Item
{
    List<PlayerStats> collided_go;

    private void Awake()
    {
        InitWrench();
        collided_go = new List<PlayerStats>();
    }

    private void InitWrench()
    {
        CollisionDetector cd = gameObject.GetComponentInChildren<CollisionDetector>();
        cd.SuscribeOnTriggerEnter2D(CustomOnTriggerEnter2D);
        cd.SuscribeOnTriggerExit2D(CustomOnTriggerExit2D);
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
            ItemManager.Instance.StopGrabbingItem(GetGrabbedBy());
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
