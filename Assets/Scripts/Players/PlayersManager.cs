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
        }
    }

    private Player AddPlayer()
    {
        Player player = new Player();

        player.SetPlayerIndex(players.Count);

        players.Add(player);

        EventPlayerAdded ev = new EventPlayerAdded(player);
        EventManager.Instance.SendEvent(ev);

        // Temp
        if(players.Count == 1)
            SpawnPlayerInstance(player, new Vector2(-3, -3));
        //else if(players.Count == 2)
        //    SpawnPlayerInstance(player, new Vector2(-6, -3));
        //else if (players.Count == 3)
        //    SpawnPlayerInstance(player, new Vector2(-9, -3));


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

    public void SpawnPlayerInstance(Player assigned, Vector2 pos)
    {
        if (assigned != null)
        {
            GameObject new_player = Instantiate(player_prefab, pos, Quaternion.identity);

            if (new_player != null)
            {
                PlayerStats stats = new_player.GetComponent<PlayerStats>();

                stats.SetPlayer(assigned);
                assigned.SetPlayerInstance(stats);

                EventPlayerSpawned ev = new EventPlayerSpawned(stats);
                EventManager.Instance.SendEvent(ev);
            }
        }
    }

    public void DestroyPlayersInstances()
    {
        for(int i = 0; i < players.Count; ++i)
        {
            Player curr_player = players[i];

            PlayerStats instance = curr_player.GetPlayerInstance();

            if (instance != null)
            {
                EventPlayerDeSpawned ev = new EventPlayerDeSpawned(instance);
                EventManager.Instance.SendEvent(ev);

                Destroy(instance.gameObject);
            }
            
            curr_player.SetPlayerInstance(null);
        }
    }

    [SerializeField]
    private GameObject player_prefab = null;

    private List<Player> players = new List<Player>();
}
