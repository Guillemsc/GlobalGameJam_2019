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
            for (int i = 0; i < gamepads_names.Length; ++i)
            {
                string curr_gamepad_name = gamepads_names[i];

                if (curr_gamepad_name == "")
                {
                    RemoveGamepad(i);
                }
                else
                {
                    AddGamepad(i, curr_gamepad_name);
                }
            }
        }
    }

    private void AddGamepad(int index, string gamepad_name)
    {
        if (!gamepads.ContainsKey(index))
        {
            Gamepad new_gamepad = new Gamepad();
            new_gamepad.SetGamepadName(gamepad_name);
            new_gamepad.SetGamepadIndex(index);

            gamepads.Add(index, new_gamepad);

            EventGamepadAdded ev = new EventGamepadAdded(new_gamepad);
            EventManager.Instance.SendEvent(ev);
        }
    }

    private void RemoveGamepad(int index)
    {
        if (gamepads.ContainsKey(index))
        {
            Gamepad to_remove = gamepads[index];

            gamepads.Remove(index);

            EventGamepadRemoved ev = new EventGamepadRemoved(to_remove);
            EventManager.Instance.SendEvent(ev);
        }
    }

    private Dictionary<int, Gamepad> gamepads = new Dictionary<int, Gamepad>();
}
