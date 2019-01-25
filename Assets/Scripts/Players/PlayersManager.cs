using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : Singleton<PlayersManager>
{
    PlayersManager()
    {
        InitInstance(this);
    }

    public Player AddPlayer()
    {
        Player ret = new Player();

        ret.SetPlayerId(max_player_id++);

        players.Add(ret);

        UpdatePlayersIndex();

        EventPlayerAdded ev = new EventPlayerAdded(ret);
        EventManager.Instance.SendEvent(ev);

        return ret;
    }

    public void RemovePlayer(Player pl)
    {
        players.Remove(pl);

        UpdatePlayersIndex();

        EventPlayerRemoved ev = new EventPlayerRemoved(pl);
        EventManager.Instance.SendEvent(ev);
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

    public Player GetPlayerById(int id)
    {
        Player ret = null;

        for (int i = 0; i < players.Count; ++i)
        {
            if(players[i].GetPlayerId() == id)
            {
                ret = players[i];
                break;
            }
        }

        return ret;
    }

    private void UpdatePlayersIndex()
    {
        for(int i = 0; i < players.Count; ++i)
            players[i].SetPlayerIndex(i);
    }

    private List<Player> players = new List<Player>();
    private int max_player_id = 0;
}
