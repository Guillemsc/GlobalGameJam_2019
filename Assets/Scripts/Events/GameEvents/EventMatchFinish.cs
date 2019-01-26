using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMatchFinish : GameEvent
{
    public EventMatchFinish(PlayerStats winner, List<PlayerStats> players) : base(GameEventType.EVENT_MATCH_FINISH)
    {
        this.winner = winner;
        this.players = players;
    }

    public PlayerStats winner = null;
    public List<PlayerStats> players = new List<PlayerStats>();
}
