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
        player = gameObject.GetComponent<Player>();
    }

    Player player = null;
}
