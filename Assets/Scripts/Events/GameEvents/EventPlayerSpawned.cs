using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlayerSpawned: GameEvent
{
    public EventPlayerSpawned(PlayerStats player) : base(GameEventType.EVENT_PLAYER_SPAWNED)
    {
        this.player = player;
    }

    public PlayerStats player = null;
}

