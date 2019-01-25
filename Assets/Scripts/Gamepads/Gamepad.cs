using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamepad
{
    public void SetGamepadName(string name)
    {
        gamepad_name = name;
    }

    public string GetGamepadName()
    {
        return gamepad_name;
    }

    public void SetGamepadIndex(int index)
    {
        gamepad_index = index;
    }

    public int GetGamepadIndex()
    {
        return gamepad_index;
    }

    private string gamepad_name = "";
    private int gamepad_index = 0;
}
