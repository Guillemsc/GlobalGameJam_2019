using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public void SetPlayerIndex(int index)
    {
        player_index = index;
    }

    public int GetPlayerIndex()
    {
        return player_index;
    }

    public void SetAssignedGamepad(Gamepad gp)
    {
        assigned_gamepad = gp;
    }

    public Gamepad GetAssignedGamepad()
    {
        return assigned_gamepad;
    }

    public bool LeftStickUp()
    {
        bool ret = false;

        if(assigned_gamepad != null)
        {

        }

        return ret;
    }

    private int player_index = 0;
    private Gamepad assigned_gamepad = null;
}
