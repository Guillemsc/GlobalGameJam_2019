using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHousesSpawned : GameEvent
{
    public EventHousesSpawned(List<House> houses) : base(GameEventType.EVENT_HOUSES_SPAWNED)
    {
        this.houses = houses;
    }

    public List<House> houses = new List<House>();
}
