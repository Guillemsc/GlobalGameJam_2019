using UnityEngine;

public enum GameEventType
{
    EVENT_NULL,

    EVENT_PLAYER_ADDED,
    EVENT_PLAYER_REMOVED,
}

public class GameEvent
{
    public GameEvent(GameEventType e_type)
    {
        event_type = e_type;
    }

    public GameEventType Type()
    {
        return event_type;
    }

    private GameEventType event_type = GameEventType.EVENT_NULL;
}

