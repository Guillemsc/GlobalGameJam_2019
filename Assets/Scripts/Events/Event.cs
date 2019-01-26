using UnityEngine;

public enum GameEventType
{
    EVENT_NULL,

    EVENT_PLAYER_ADDED,
    EVENT_PLAYER_REMOVED,
    EVENT_PLAYER_SPAWNED,
    EVENT_PLAYER_DESPAWNED,

    EVENT_GAMEPADS_CHANGED,
    EVENT_GAMEPAD_ADDED,
    EVENT_GAMEPAD_REMOVED,

    EVENT_ADD_SCORE,
    EVENT_SUBSTRACT_SCORE,

    EVENT_START_QUEST,
    EVENT_END_QUEST,

    EVENT_HOUSES_SPAWNED,

    EVENT_MAP_LOAD,
    EVENT_MAP_UNLOAD,
    EVENT_MATCH_START,
    EVENT_MATCH_FINISH,
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

