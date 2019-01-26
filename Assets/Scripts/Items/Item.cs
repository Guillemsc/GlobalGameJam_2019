using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    ITEM_MAGNET,
}

public class Item : MonoBehaviour
{
    protected void Init(ItemType type)
    {
        this.type = type;
    }

    public ItemType Type()
    {
        return type;
    }

    public void SetGrabbedBy(PlayerStats set)
    {
        grabbed_by = set;
    }

    public PlayerStats GetGrabbedBy()
    {
        return grabbed_by;
    }

    public bool GetIsGrabbed()
    {
        return grabbed_by != null;
    }

    private ItemType type = new ItemType();
    private PlayerStats grabbed_by = null;
}
