using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubstractScore : GameEvent {
    public EventSubstractScore(PlayerStats player, int amount) : base(GameEventType.EVENT_SUBSTRACT_SCORE) {
        this.player = player;
        this.amount = amount;
    }

    public PlayerStats player = null;
    public int amount = 0;

}
