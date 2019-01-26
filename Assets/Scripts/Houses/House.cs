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
            items_around.Add(it);
    }

    private void RemoveItem(Item it)
    {
        items_around.Remove(it);
    }

    private void CalculateHousePoints()
    {
        points = 0;

        for (int i = 0; i < items_around.Count; ++i)
        {

        }
    }

    private PlayerStats player_instance = null;
    private CircleCollider2D circle_collider = null;

    private int points = 0;

    private List<Item> items_around = new List<Item>();
}
