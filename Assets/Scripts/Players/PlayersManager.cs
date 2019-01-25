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

        ret.SetPlayerIndex(players.Count);

        players.Add(ret);

        EventPlayerAdded ev = new EventPlayerAdded(ret);
        EventManager.Instance.SendEvent(ev);

        return ret;
    }

    public void RemovePlayer(Player pl)
    {
        EventPlayerRemoved ev = new EventPlayerRemoved(pl);
        EventManager.Instance.SendEvent(ev);

        players.Remove(pl);
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

    List<Player> players = new List<Player>();
}
