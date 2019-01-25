using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGamepadAdded : GameEvent
{
    public EventGamepadAdded(Gamepad gamepad) : base(GameEventType.EVENT_GAMEPAD_ADDED)
    {
        this.gamepad = gamepad;
    }

    public Gamepad gamepad = null;
}
