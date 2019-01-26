using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventItemEntersHouse : GameEvent
{
    public EventItemEntersHouse(Item item, House house) : base(GameEventType.EVENT_ITEM_ENTERS_HOUSE)
    {
        this.item = item;
        this.house = house;
    }

    public Item item = null;
    public House house = null;
}

