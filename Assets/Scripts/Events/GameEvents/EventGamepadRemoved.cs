using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGamepadRemoved : GameEvent
{
    public EventGamepadRemoved(Gamepad gamepad) : base(GameEventType.EVENT_GAMEPAD_REMOVED)
    {
        this.gamepad = gamepad;
    }

    public Gamepad gamepad = null;
}
