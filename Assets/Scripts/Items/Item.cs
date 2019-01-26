using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    ITEM_MAGNET,
    ITEM_GUN,
    ITEM_LACAJA,
}

public class Item : MonoBehaviour
{
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

    public void SetHouse(House ho)
    {
        house = ho;
    }

    public House GetHouse()
    {
        return house;
    }

    public bool GetInHouse()
    {
        return house != null;
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
    private House house = null;
}
