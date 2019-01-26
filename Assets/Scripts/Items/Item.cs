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
    public Sprite base_sprite = null; 
    public Sprite hidden_sprite;
    [HideInInspector]
    public SpriteRenderer sr;

    [HideInInspector]
    public bool destroyed = false;

    public void BaseStart() {
        sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null) {
            base_sprite = sr.sprite;
        }
    }

    public ItemType Type()
    {
        return type;
    }

    public void SetHidden() {

        sr.sprite = hidden_sprite;
    }

    public void SetBaseSprite() {
        sr.sprite = base_sprite;
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
        return (destroyed) ? (points_destroyed) : points_to_give;
    }

    public void OnPlayerGrabBase(PlayerStats player) 
    {
        SetBaseSprite();
    }

    public virtual void OnPlayerGrab(PlayerStats player)
    {

    }

    public virtual void OnPlayerGrabbed()
    {

    }

    public void OnPlayerUsesBase()
    {
        destroyed = true;
        sr.color = Color.red;
    }

    public virtual void OnPlayerUses()
    {

    }

    public void OnPlayerThrowBase() {
        if (ItemManager.Instance.hidden_items)
            SetHidden();
    }

    public virtual void OnPlayerThrows()
    {

    }

    [SerializeField]
    private ItemType type = new ItemType();

    [SerializeField]
    public int points_to_give = 0;

    [SerializeField]
    private int points_destroyed = 0;

    private PlayerStats grabbed_by = null;
    private House house = null;
}
