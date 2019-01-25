using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGamepadsChanged : GameEvent
{
    public EventGamepadsChanged(List<Gamepad> gamepads) : base(GameEventType.EVENT_GAMEPADS_CHANGED)
    {
        this.gamepads = gamepads;
    }

    public List<Gamepad> gamepads = new List<Gamepad>();
}
