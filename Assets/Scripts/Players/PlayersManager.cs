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

    private void Update()
    {

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

    public Player AddPlayer()
    {
        Player player = new Player();

        player.SetPlayerIndex(players.Count);

        players.Add(player);

        EventPlayerAdded ev = new EventPlayerAdded(player);
        EventManager.Instance.SendEvent(ev);

        return player;
    }

    public void RemovePlayers()
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

    private List<Player> players = new List<Player>();
}
