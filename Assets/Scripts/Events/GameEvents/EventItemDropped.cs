using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventItemDropped : GameEvent
{
    public EventItemDropped(Item item, PlayerStats playe) : base(GameEventType.EVENT_ITEM_DROPPED)
    {
        this.item = item;
        this.player = playe;
    }

    public Item item = null;
    public PlayerStats player = null;
}
