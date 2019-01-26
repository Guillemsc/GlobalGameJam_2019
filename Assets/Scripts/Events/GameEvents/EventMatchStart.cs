using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMatchStart : GameEvent
{
    public EventMatchStart(float duration, List<PlayerStats> players) : base(GameEventType.EVENT_MATCH_START)
    {
        this.duration = duration;
        this.players = players;
    }

    public float duration = 0.0f;
    public List<PlayerStats> players = new List<PlayerStats>();
}
