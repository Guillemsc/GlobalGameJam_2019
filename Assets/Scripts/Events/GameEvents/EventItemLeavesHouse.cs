using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventItemLeavesHouse : GameEvent
{
    public EventItemLeavesHouse(Item item, House house) : base(GameEventType.EVENT_ITEM_LEAVES_HOUSE)
    {
        this.item = item;
        this.house = house;
    }

    public Item item = null;
    public House house = null;
}
