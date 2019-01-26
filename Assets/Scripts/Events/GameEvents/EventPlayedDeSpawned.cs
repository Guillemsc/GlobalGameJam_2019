using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlayerDeSpawned : GameEvent
{
    public EventPlayerDeSpawned(PlayerStats player) : base(GameEventType.EVENT_PLAYER_DESPAWNED)
    {
        this.player = player;
    }

    public PlayerStats player = null;
}

