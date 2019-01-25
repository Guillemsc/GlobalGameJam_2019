using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private void Awake()
    {
        
    }

    public void SetPlayer(Player pl)
    {
        player = pl;
    }

    Player player = null;
}
