using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public void SetPlayer(Player pl)
    {
        player = pl;

        if (player != null)
            gameObject.name = "Player: " + player.GetPlayerIndex();
    }

    public Player GetPlayer()
    {
        return player;
    }

    public float GetJoystickDeadVal()
    {
        return joystick_dead_val;
    }

    public void SetGrabbedItem(Item item)
    {
        grabbed_item = item;
    }

    public Item GetGrabbedItem()
    {
        return grabbed_item;
    }

    public bool GetHasGrabbedItem()
    {
        return grabbed_item != null;
    }

    [SerializeField]
    private float joystick_dead_val = 0.0f;

    private Player player = null;

    private Item grabbed_item = null;
}
