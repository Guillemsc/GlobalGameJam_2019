using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlayerRemoved : GameEvent
{
    public EventPlayerRemoved(Player player) : base(GameEventType.EVENT_PLAYER_REMOVED)
    {
        this.player = player;
    }

    public Player player = null;
}
