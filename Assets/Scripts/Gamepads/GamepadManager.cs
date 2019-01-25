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

        int gamepads_count = 0;

        for (int i = 0; i < gamepads_names.Length; ++i)
        {
            if (gamepads_names[i] != "")
                ++gamepads_count;
        }

        if(gamepads.Count > gamepads_count)
        {
            UpdateGamepads(gamepads_names);
        }
        else if(gamepads.Count < gamepads_count)
        {
            UpdateGamepads(gamepads_names);
        }
    }

    private void UpdateGamepads(string[] gamepads_names)
    {
        if (gamepads_names != null)
        {
            gamepads.Clear();

            for (int i = 0; i < gamepads_names.Length; ++i)
            {
                string curr_gamepad_name = gamepads_names[i];

                if (curr_gamepad_name != "")
                {
                    Gamepad gamepad = new Gamepad();
                    gamepad.SetGamepadName(curr_gamepad_name);
                    gamepad.SetGamepadIndex(i);

                    gamepads.Add(gamepad);
                }
            }

            EventGamepadsChanged ev = new EventGamepadsChanged(gamepads);
            EventManager.Instance.SendEvent(ev);
        }
    }

    private List<Gamepad> gamepads = new List<Gamepad>();
}
