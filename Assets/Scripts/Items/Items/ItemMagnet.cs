﻿using System.Collections;
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
        CollisionDetector cd = gameObject.GetComponentInChildren<CollisionDetector>();

        Init(ItemType.ITEM_MAGNET, cd);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
