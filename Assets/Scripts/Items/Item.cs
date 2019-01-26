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
    protected void Init(ItemType type, CollisionDetector coll_detector)
    {
        this.type = type;

        coll_detector.SuscribeOnTriggerEnter2D(CustomOnTriggerEnter2D);
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

    private void CustomOnTriggerEnter2D(Collider2D coll)
    {
        if (!GetIsGrabbed())
        {
            PlayerStats ps = coll.gameObject.GetComponent<PlayerStats>();


        }
    }

    public virtual void OnPlayerGrab(PlayerStats player)
    {

    }

    public virtual void OnPlayerGrabbed()
    {

    }

    public virtual void OnPlayerThrows()
    {

    }

    private ItemType type = new ItemType();
    private PlayerStats grabbed_by = null;
}
