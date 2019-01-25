using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamepad
{
    public void SetGamepadName(string name)
    {
        gamepad_name = name;
    }

    public void SetGamepadIndex(int index)
    {
        gamepad_index = index;
    }

    public void SetGamepadId(int id)
    {
        gamepad_id = id;
    }

    private string gamepad_name = "";
    private int gamepad_index = 0;
    private int gamepad_id = 0;
}
