using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBulletHitsPlayer : GameEvent
{
    public EventBulletHitsPlayer(PlayerStats shooter, PlayerStats hit) : base(GameEventType.EVENT_BULLET_HITS_PLAYER)
    {
        this.shooter = shooter;
        this.hit = hit;
    }

    public PlayerStats shooter = null;
    public PlayerStats hit = null;
}
