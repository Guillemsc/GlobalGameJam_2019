using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public void SetPlayerInstance(PlayerStats pl)
    {
        player_instance = pl;
    }

    public PlayerStats GetPlayerInstance()
    {
        return player_instance;
    }

    public int GetPoints()
    {
        return points;
    }

    private PlayerStats player_instance = null;

    private int points = 0;
}
