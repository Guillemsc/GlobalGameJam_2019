using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public void SetPlayerIndex(int index)
    {
        player_index = index;
    }

    public void SetPlayerId(int id)
    {
        player_id = id;
    }

    public int GetPlayerIndex()
    {
        return player_index;
    }

    public int GetPlayerId()
    {
        return player_id;
    }

    private int player_index = 0;
    private int player_id = 0;
}
