using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSetScore : GameEvent
{
    public EventSetScore(PlayerStats player, int amount) : base(GameEventType.EVENT_SET_SCORE) 
    {
        this.player = player;
        this.amount = amount;
    }

    public PlayerStats player = null;
    public int amount = 0;

}
