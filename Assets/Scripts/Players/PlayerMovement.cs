using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private void Awake()
    {
        InitPlayer();
    }

    private void InitPlayer()
    {
        stats = gameObject.GetComponent<PlayerStats>();
    }

    private void PlayerInput()
    {
        Player player = stats.GetPlayer();

        if (player.HasGamepad())
        {

        }
    }

    private PlayerStats stats = null;

    private Vector2Int input = Vector2Int.zero;
}
