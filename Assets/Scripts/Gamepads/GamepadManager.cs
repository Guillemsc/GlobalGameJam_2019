using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadManager : Singleton<GamepadManager>
{
    GamepadManager()
    {
        InitInstance(this);
    }

    private void Update()
    {
        DetectGamepadsConnection();
    }

    private void DetectGamepadsConnection()
    {
        string[] gamepads_names = Input.GetJoystickNames();

        for (int x = 0; x < gamepads_names.Length; x++)
        {
            
        }
    }

    private List<Gamepad> gamepads = new List<Gamepad>();
}
