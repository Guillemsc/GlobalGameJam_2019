using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadManager : Singleton<GamepadManager>
{
    GamepadManager()
    {
        InitInstance(this);
    }

    private void Start()
    {
        detect.Start();
        detect.AddTime(detect_time);

        DetectGamepadsConnection();
    }

    private void Update()
    {
        //if (detect.ReadRealTime() > detect_time)
        //{
        //    DetectGamepadsConnection();

        //    detect.Start();
        //}
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
            ClearGamepads();

            int gamepad_added_counter = 0;
            for (int i = 0; i < gamepads_names.Length; ++i)
            {
                string curr_gamepad_name = gamepads_names[i];

                if (curr_gamepad_name != "")
                {
                    AddGamepad(gamepad_added_counter++, curr_gamepad_name);
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

    private void ClearGamepads()
    {
        foreach (KeyValuePair<int, Gamepad> to_remove in gamepads)
        {
            EventGamepadRemoved ev = new EventGamepadRemoved(to_remove.Value);
            EventManager.Instance.SendEvent(ev);
        }

        gamepads.Clear();
    }

    private Dictionary<int, Gamepad> gamepads = new Dictionary<int, Gamepad>();

    private Timer detect = new Timer();
    private float detect_time = 5.0f;
}
