using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    private void Awake()
    {
        InitEvents();
    }

    private void InitEvents()
    {
        EventManager.Instance.Suscribe(GameEventType.EVENT_ITEM_GRABBED, OnEvent);
        EventManager.Instance.Suscribe(GameEventType.EVENT_ITEM_DROPPED, OnEvent);
    }

    private void OnEvent(GameEvent ev)
    {
        switch(ev.Type())
        {
            case GameEventType.EVENT_ITEM_GRABBED:
                {
                    break;
                }

            case GameEventType.EVENT_ITEM_DROPPED:
                {
                    break;
                }
        }
    }

    public void SetPlayerInstance(PlayerStats pl)
    {
        player_instance = pl;
    }

    public PlayerStats GetPlayerInstance()
    {
        return player_instance;
    }

    public int GetPoints()
    {
        return points;
    }

    private bool ItemIsInsideRadious(Item it)
    {
        bool ret = false;

        if(it != null)
        {
            float dist = Vector3.Distance(it.gameObject.transform.position, gameObject.transform.position);

            dist = Mathf.Abs(dist);

            if (dist <= HouseManager.Instance.GetHouseItemRadious())
            {
                ret = true;
            }
        }

        return ret;
    }

    private void TryAddItem(Item it)
    {
        if(it != null)
        {
            float dist = Vector3.Distance(it.gameObject.transform.position, gameObject.transform.position);

            //if (Vector3.Distance(it.gameObject.transform.position))
        }
    }

    private void AddItem(Item it)
    {
        bool exists = false;
        for(int i = 0; i < items_around.Count; ++i)
        {
            if(items_around[i] == it)
            {
                exists = true;
                break;
            }
        }

        if (!exists)
        {
            items_around.Add(it);

            EventItemEntersHouse ev = new EventItemEntersHouse(it, this);
            EventManager.Instance.SendEvent(ev);
        }

        RecalculateHousePoints();
    }

    private void RemoveItem(Item it)
    {
        if (it != null)
        {
            if(items_around.Remove(it))
            {
                EventItemLeavesHouse ev = new EventItemLeavesHouse(it, this);
                EventManager.Instance.SendEvent(ev);
            }

            RecalculateHousePoints();
        }
    }

    private void RecalculateHousePoints()
    {
        points = 0;

        for (int i = 0; i < items_around.Count; ++i)
        {
            Item curr_item = items_around[i];

            points += curr_item.GetPointsToGive();
        }
    }

    private PlayerStats player_instance = null;

    private int points = 0;

    private List<Item> items_around = new List<Item>();
}
