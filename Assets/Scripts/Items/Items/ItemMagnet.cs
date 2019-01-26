using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : Item
{
    private void Awake()
    {
        InitItem();
    }

    private void InitItem()
    {
        CircleCollider2D circle = gameObject.GetComponent<CircleCollider2D>();

        Init(circle);
    }


    public override void OnPlayerGrab(PlayerStats player)
    {
        base.OnPlayerGrab(player);
    }
}
