using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class ItemBomb : Item
{
    List<GameObject> collided_go;
    bool has_exploded = false;
    bool wrenched = false;

    float speed = 1.5f;

    private void Awake()
    {
        InitBomb();
        collided_go = new List<GameObject>();
    }

    private void InitBomb()
    {
        CollisionDetector cd = GetComponentInChildren<CollisionDetector>();
        cd.SuscribeOnTriggerEnter2D(CustomOnTriggerEnter2D);
        cd.SuscribeOnTriggerExit2D(CustomOnTriggerExit2D);
    }

    private void Update()
    {
        if (sr.color == Color.red)
        {
            wrenched = true;
        }

        if (destroyed && !has_exploded && !wrenched)
        {
            sr.color = Color.white;

            for (int i = 0; i < collided_go.Count; ++i)
            {
                Vector3 dir = collided_go[i].transform.position - transform.position;            
                collided_go[i].transform.position += dir.normalized * speed * Time.deltaTime;
            }
        }      

        if (has_exploded)
        {
            ItemManager.Instance.DestroyItem(this);

        }
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
            ItemManager.Instance.StopGrabbingItem(GetGrabbedBy());

            GetComponentInChildren<Animator2D>().PlayAnimation("Explosion", 0.05F, false);
            StartCoroutine(PlayExplosion());
        }
    }

    IEnumerator PlayExplosion()
    {
        yield return new WaitForSeconds(0.6F);
        has_exploded = true;
    }

    private void CustomOnTriggerEnter2D(Collider2D coll)
    {
        GameObject ps = coll.gameObject;

        if (ps.GetComponent<Item>() != null || ps.GetComponent<PlayerStats>() != null)
            collided_go.Add(ps);
    }

    private void CustomOnTriggerExit2D(Collider2D coll)
    {
        GameObject ps = coll.gameObject;

        if (ps.GetComponent<Item>() != null || ps.GetComponent<PlayerStats>() != null)
            collided_go.Remove(ps);
    }
}
