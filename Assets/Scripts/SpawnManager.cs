using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public GameObject PlayerPrefab;


    SpawnManager()
    {
        InitInstance(this);
    }

    public void SpawnPlayer(Vector2  spawn_position) //, Player player)
    {
        Instantiate(PlayerPrefab, spawn_position, Quaternion.identity);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instance.SpawnPlayer(new Vector2(1, 1));
        }
    }
}
