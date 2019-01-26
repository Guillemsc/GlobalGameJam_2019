using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GamepadGetButtonType
{
    WHILE,
    DOWN,
    UP,
}

public enum PlayerColour
{
    RED,
    BLUE,
    YELLOW,
}

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

    public void SetPlayerColour(PlayerColour col)
    {
        colour = col;
    }

    public PlayerColour GetPlayerColour()
    {
        return colour;
    }

    public void SetAssignedGamepad(Gamepad gp)
    {
        assigned_gamepad = gp;
    }

    public Gamepad GetAssignedGamepad()
    {
        return assigned_gamepad;
    }

    public void SetPlayerInstance(PlayerStats instance)
    {
        player_instance = instance;
    }

    public PlayerStats GetPlayerInstance()
    {
        return player_instance;
    }

    public bool HasGamepad()
    {
        return assigned_gamepad != null;
    }

    public bool GetKeyA(GamepadGetButtonType type = GamepadGetButtonType.WHILE)
    {
        bool ret = false;

        if(HasGamepad())
        {
            string button = "A_P" + (player_index + 1).ToString();

            ret = GetButtonInputByType(button, type);
        }

        return ret;
    }

    public bool GetKeyB(GamepadGetButtonType type = GamepadGetButtonType.WHILE)
    {
        bool ret = false;

        if (HasGamepad())
        {
            string button = "B_P" + (player_index + 1).ToString();

            ret = GetButtonInputByType(button, type);
        }

        return ret;
    }

    public bool GetKeyX(GamepadGetButtonType type = GamepadGetButtonType.WHILE)
    {
        bool ret = false;

        if (HasGamepad())
        {
            string button = "X_P" + (player_index + 1).ToString();

            ret = GetButtonInputByType(button, type);
        }

        return ret;
    }

    public bool GetKeyY(GamepadGetButtonType type = GamepadGetButtonType.WHILE)
    {
        bool ret = false;

        if (HasGamepad())
        {
            string button = "Y_P" + (player_index + 1).ToString();

            ret = GetButtonInputByType(button, type);
        }

        return ret;
    }

    public bool GetKeyStart(GamepadGetButtonType type = GamepadGetButtonType.WHILE)
    {
        bool ret = false;

        if (HasGamepad())
        {
            string button = "START_P" + (player_index + 1).ToString();

            ret = GetButtonInputByType(button, type);
        }

        return ret;
    }

    public bool GetKeyBack(GamepadGetButtonType type = GamepadGetButtonType.WHILE)
    {
        bool ret = false;

        if (HasGamepad())
        {
            string button = "BACK_P" + (player_index + 1).ToString();

            ret = GetButtonInputByType(button, type);
        }

        return ret;
    }

    public bool GetKeyRB(GamepadGetButtonType type = GamepadGetButtonType.WHILE)
    {
        bool ret = false;

        if (HasGamepad())
        {
            string button = "RB_P" + (player_index + 1).ToString();

            ret = GetButtonInputByType(button, type);
        }

        return ret;
    }

    public bool GetKeyLB(GamepadGetButtonType type = GamepadGetButtonType.WHILE)
    {
        bool ret = false;

        if (HasGamepad())
        {
            string button = "LB_P" + (player_index + 1).ToString();

            ret = GetButtonInputByType(button, type);
        }

        return ret;
    }

    public float GetKeyRT()
    {
        float ret = 0.0f;

        if (HasGamepad())
        {
            string joystick = "RT_P" + (player_index + 1).ToString();

            ret = Input.GetAxis(joystick);
        }

        return ret;
    }

    public float GetKeyLT()
    {
        float ret = 0.0f;

        if (HasGamepad())
        {
            string joystick = "LT_P" + (player_index + 1).ToString();

            ret = Input.GetAxis(joystick);
        }

        return ret;
    }

    public float LeftJoystickHorizontal()
    {
        float ret = 0.0f;

        if (HasGamepad())
        {
            string joystick = "LEFT_JOY_HORI_P" + (player_index + 1).ToString();

            ret = Input.GetAxis(joystick);
        }

        return ret;
    }

    public float LeftJoystickVertical()
    {
        float ret = 0.0f;

        if (HasGamepad())
        {
            string joystick = "LEFT_JOY_VERT_P" + (player_index + 1).ToString();

            ret = Input.GetAxis(joystick);
        }

        return ret;
    }

    public float RightJoystickHorizontal()
    {
        float ret = 0.0f;

        if (HasGamepad())
        {
            string joystick = "RIGHT_JOY_HORI_P" + (player_index + 1).ToString();

            ret = Input.GetAxis(joystick);
        }

        return ret;
    }

    public float RightJoystickVertical()
    {
        float ret = 0.0f;

        if (HasGamepad())
        {
            string joystick = "RIGHT_JOY_VERT_P" + (player_index + 1).ToString();

            ret = Input.GetAxis(joystick);
        }

        return ret;
    }

    public float DPadHorizontal()
    {
        float ret = 0.0f;

        if (HasGamepad())
        {
            string joystick = "DPAD_HORI_P" + (player_index + 1).ToString();

            ret = Input.GetAxis(joystick);
        }

        return ret;
    }

    public float DPadVertical()
    {
        float ret = 0.0f;

        if (HasGamepad())
        {
            string joystick = "DPAD_VERT_P" + (player_index + 1).ToString();

            ret = Input.GetAxis(joystick);
        }

        return ret;
    }

    private bool GetButtonInputByType(string button_name, GamepadGetButtonType type)
    {
        bool ret = false;

        switch (type)
        {
            case GamepadGetButtonType.WHILE:
                ret = Input.GetButton(button_name);
                break;
            case GamepadGetButtonType.DOWN:
                ret = Input.GetButtonDown(button_name);
                break;
            case GamepadGetButtonType.UP:
                ret = Input.GetButtonUp(button_name);
                break;
        }

        return ret;
    }

    private int player_index = 0;
    private Gamepad assigned_gamepad = null;
    private PlayerStats player_instance = null;
    private PlayerColour colour = new PlayerColour();
}
