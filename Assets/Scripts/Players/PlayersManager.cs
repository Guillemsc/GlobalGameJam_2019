using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : Singleton<PlayersManager>
{
    PlayersManager()
    {
        InitInstance(this);
    }

    private void Awake()
    {
        EventManager.Instance.Suscribe(GameEventType.EVENT_GAMEPAD_ADDED, OnEvent);
        EventManager.Instance.Suscribe(GameEventType.EVENT_GAMEPAD_REMOVED, OnEvent);

        EventManager.Instance.Suscribe(GameEventType.EVENT_HOUSES_SPAWNED, OnEvent);

        AddPlayer();
        AddPlayer();
        AddPlayer();
    }

    private void OnEvent(GameEvent ev)
    {
        switch(ev.Type())
        {
            case GameEventType.EVENT_GAMEPAD_ADDED:
                {
                    EventGamepadAdded c_ev = (EventGamepadAdded)ev;

                    Player pl = GetPlayerByIndex(c_ev.gamepad.GetGamepadIndex());

                    if(pl != null)
                        pl.SetAssignedGamepad(c_ev.gamepad);
                    
                    break;
                }
            case GameEventType.EVENT_GAMEPAD_REMOVED:
                {
                    EventGamepadRemoved c_ev = (EventGamepadRemoved)ev;

                    Player pl = GetPlayerByIndex(c_ev.gamepad.GetGamepadIndex());

                    if (pl != null)
                        pl.SetAssignedGamepad(null);

                    break;
                }

            case GameEventType.EVENT_HOUSES_SPAWNED:
                {
                    EventHousesSpawned c_ev = (EventHousesSpawned)ev;

                    SpawnPlayersOnHousePositions(c_ev.houses);

                    break;
                }
        }
    }

    private Player AddPlayer()
    {
        Player player = new Player();

        player.SetPlayerIndex(players.Count);

        players.Add(player);

        EventPlayerAdded ev = new EventPlayerAdded(player);
        EventManager.Instance.SendEvent(ev);

        return player;
    }

    private void RemovePlayers()
    {
        players.Clear();
    }

    public int GetPlayersCount()
    {
        return players.Count;
    }

    public Player GetPlayerByIndex(int index)
    {
        Player ret = null;

        if (players.Count > index)
        {
            ret = players[index];
        }

        return ret;
    }

    public List<PlayerStats> GetAllPlayerInstances()
    {
        List<PlayerStats> ret = new List<PlayerStats>();

        for(int i = 0; i < players.Count; ++i)
        {
            Player curr_player = players[i];

            PlayerStats stats = curr_player.GetPlayerInstance();

            if (stats != null)
                ret.Add(stats);
        }

        return ret;
    }

    private void SpawnPlayersOnHousePositions(List<House> houses)
    {
        for(int i = 0; i < houses.Count; ++i)
        {
            House curr_house = houses[i];

            if(players.Count > i)
            {
                PlayerStats player_instance = SpawnPlayerInstance(players[i], curr_house.gameObject.transform.position);

                curr_house.SetPlayerInstance(player_instance);
            }
        }
    }

    public PlayerStats SpawnPlayerInstance(Player assigned, Vector2 pos)
    {
        PlayerStats ret = null;

        if (assigned != null)
        {
            DestroyPlayerInstance(assigned);

            GameObject new_player = Instantiate(player_prefab, pos, Quaternion.identity);

            if (new_player != null)
            {
                ret = new_player.GetComponent<PlayerStats>();

                ret.SetPlayer(assigned);
                assigned.SetPlayerInstance(ret);

                EventPlayerSpawned ev = new EventPlayerSpawned(ret);
                EventManager.Instance.SendEvent(ev);
            }
        }

        return ret;
    }

    public void DestroyPlayersInstances()
    {
        for(int i = 0; i < players.Count; ++i)
        {
            Player curr_player = players[i];

            DestroyPlayerInstance(curr_player);
        }
    }

    public void DestroyPlayerInstance(Player player)
    {
        PlayerStats instance = player.GetPlayerInstance();

        if (instance != null)
        {
            EventPlayerDeSpawned ev = new EventPlayerDeSpawned(instance);
            EventManager.Instance.SendEvent(ev);

            Destroy(instance.gameObject);
        }
    }

    [SerializeField]
    private GameObject player_prefab = null;

    private List<Player> players = new List<Player>();
}
