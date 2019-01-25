using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public void SetPlayerIndex(int index)
    {
        player_index = index;
    }

    int GetPlayerIndex()
    {
        return player_index;
    }

    private int player_index = 0;
}
