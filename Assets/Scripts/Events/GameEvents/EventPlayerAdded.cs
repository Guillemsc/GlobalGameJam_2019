using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlayerAdded : GameEvent
{
    public EventPlayerAdded(Player player) : base(GameEventType.EVENT_PLAYER_ADDED)
    {
        this.player = player;
    }

    public Player player = null;
}
