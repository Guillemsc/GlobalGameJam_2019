using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private void SetPlayer(Player pl)
    {
        player = pl;

        if (player != null)
            gameObject.name = "Player: " + player.GetPlayerIndex();
    }

    public Player GetPlayer()
    {
        return player;
    }

    private Player player = null;
}
