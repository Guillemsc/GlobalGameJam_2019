using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAddScore : GameEvent
{
    public EventAddScore(PlayerStats player, int amount) : base(GameEventType.EVENT_ADD_SCORE) 
    {
        this.player = player;
        this.amount = amount;
    }

    public PlayerStats player = null;
    public int amount = 0;

}
