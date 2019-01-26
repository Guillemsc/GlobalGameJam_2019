using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventItemGrabbed : GameEvent
{
    public EventItemGrabbed(Item item, PlayerStats player) : base(GameEventType.EVENT_ITEM_GRABBED)
    {
        this.item = item;
        this.player = player;
    }

    public Item item = null;
    public PlayerStats player = null;
}
