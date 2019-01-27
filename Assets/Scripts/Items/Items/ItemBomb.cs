using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class ItemBomb : Item
{
    List<PlayerStats> collided_go;

    private void Awake()
    {
        InitBomb();
        collided_go = new List<PlayerStats>();
    }

    private void InitBomb()
    {
        CollisionDetector cd = GetComponentInChildren<CollisionDetector>();
        cd.SuscribeOnTriggerEnter2D(CustomOnTriggerEnter2D);
        cd.SuscribeOnTriggerExit2D(CustomOnTriggerExit2D);
    }

    private void Update()
    {
        
    }

    public override void OnPlayerGrab(PlayerStats player)
    {
        if (!destroyed) base.OnPlayerGrab(player);
    }

    public override void OnPlayerUses()
    {
        if (!destroyed)
        {
            PlayerStats bombarder = GetGrabbedBy();

            ItemManager.Instance.StopGrabbingItem(bombarder);

            StartCoroutine(PlayExplosion());

        }
    }

    IEnumerator PlayExplosion()
    {
        yield return new WaitForSeconds(0.5F);
        GetComponentInChildren<Animator2D>().PlayAnimation("Explosion", 0.05F, false);

        for (int i = 0; i < collided_go.Count; ++i)
        {
            ItemManager.Instance.StopGrabbingItem(collided_go[i]);
        }

        ItemManager.Instance.RemoveFromitemsInstances(this);
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
