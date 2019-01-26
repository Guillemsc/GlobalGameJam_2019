using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    ITEM_MAGNET,
    ITEM_GUN,
}

public class Item : MonoBehaviour
{
    public void Init(CircleCollider2D coll)
    {
        circle_collider = coll;

        circle_collider.isTrigger = true;
    }

    public ItemType Type()
    {
        return type;
    }

    public void SetGrabbedBy(PlayerStats set)
    {
        grabbed_by = set;

        if (grabbed_by != null)
            circle_collider.enabled = true;
        else
            circle_collider.enabled = false;
    }

    public PlayerStats GetGrabbedBy()
    {
        return grabbed_by;
    }

    public bool GetIsGrabbed()
    {
        return grabbed_by != null;
    }

    public int GetPointsToGive()
    {
        return points_to_give;
    }

    public virtual void OnPlayerGrab(PlayerStats player)
    {

    }

    public virtual void OnPlayerGrabbed()
    {

    }

    public virtual void OnPlayerUses()
    {

    }

    public virtual void OnPlayerThrows()
    {

    }

    [SerializeField]
    private ItemType type = new ItemType();

    [SerializeField]
    private int points_to_give = 0;

    private PlayerStats grabbed_by = null;

    private CircleCollider2D circle_collider = null;
}
