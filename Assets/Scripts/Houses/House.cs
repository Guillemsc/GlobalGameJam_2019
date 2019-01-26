using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    private void Awake()
    {
        InitCollider();
    }

    private void InitCollider()
    {
        circle_collider = gameObject.GetComponent<CircleCollider2D>();

        circle_collider.radius = HouseManager.Instance.GetHouseItemRadious();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item it = collision.gameObject.GetComponent<Item>();

        if(it != null)
        {
            AddItem(it);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Item it = collision.gameObject.GetComponent<Item>();

        if (it != null)
        {
            RemoveItem(it);
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
    private CircleCollider2D circle_collider = null;

    private int points = 0;

    private List<Item> items_around = new List<Item>();
}
