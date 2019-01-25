using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField]
    private GameObject PlayerPrefab;
    [SerializeField]
    private GameObject HousePrefab;

    [SerializeField]
    private Vector2[] HousePositions;

    SpawnManager()
    {
        InitInstance(this);
    }

    public void SpawnPlayer()
    {
        int num_players = PlayersManager.Instance.GetPlayersCount();
        for (int i = 0; i < num_players; ++i)
        {
            Player player = PlayersManager.Instance.GetPlayerByIndex(i);
            Vector2 spawnposition = HousePositions[i];

            Instantiate(HousePrefab, spawnposition, Quaternion.identity);
            Instantiate(PlayerPrefab, spawnposition, Quaternion.identity);

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) SpawnPlayer();
        if (Input.GetKeyDown(KeyCode.S)) PlayersManager.Instance.AddPlayer();
        Debug.Log(PlayersManager.Instance.GetPlayersCount());
    }
}
