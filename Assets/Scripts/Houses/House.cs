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

    private PlayerStats player_instance = null;
}
