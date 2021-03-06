﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public Sprite[] B_house;
    public Sprite[] R_house;
    public Sprite[] Y_house;

    public SpriteRenderer back;
    public SpriteRenderer area;

    private void Awake()
    {
        InitEvents();
    }

    private void Start()
    {
        EventSetScore ev = new EventSetScore(player_instance, points);
        EventManager.Instance.SendEvent(ev);
    }

    private void Update()
    {
        UpdateCheckItemsInside();

        PrintDebug();
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
                    EventItemGrabbed r_ev = (EventItemGrabbed)ev;

                    TryRemoveItem(r_ev.item);

                    break;
                }

            case GameEventType.EVENT_ITEM_DROPPED:
                {
                    EventItemDropped r_ev = (EventItemDropped)ev;

                    TryAddItem(r_ev.item);

                    break;
                }
        }
    }

    private void PrintDebug()
    {
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + 
            new Vector3(HouseManager.Instance.GetHouseItemRadious(), 0, 0));
    }

    public void SetPlayerInstance(PlayerStats pl)
    {
        player_instance = pl;
        switch (pl.GetPlayer().GetPlayerColour())
        {
            case PlayerColour.RED:
                GetComponent<SpriteRenderer>().sprite = R_house[0];
                back.sprite = R_house[1];
                area.sprite = R_house[2];
                break;
            case PlayerColour.BLUE:
                GetComponent<SpriteRenderer>().sprite = B_house[0];
                back.sprite = B_house[1];
                area.sprite = B_house[2];
                break;
            case PlayerColour.YELLOW:
                GetComponent<SpriteRenderer>().sprite = Y_house[0];
                back.sprite = Y_house[1];
                area.sprite = Y_house[2];
                break;
        }
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
            float dist = Vector2.Distance(it.gameObject.transform.position, gameObject.transform.position);

            dist = Mathf.Abs(dist);

            if (dist <= HouseManager.Instance.GetHouseItemRadious())
            {
                ret = true;
            }
        }

        return ret;
    }

    private void UpdateCheckItemsInside()
    {
        for(int i = 0; i < items_around.Count; ++i)
        {
            Item curr_item = items_around[i];

            if(TryRemoveItem(curr_item))
            {
                break;
            }
        }
    }

    private void TryAddItem(Item it)
    {
        if(it != null)
        {
            if (!it.GetInHouse())
            {
                if (ItemIsInsideRadious(it))
                {
                    AddItem(it);
                }
            }
        }
    }

    private bool TryRemoveItem(Item it)
    {
        bool ret = false;

        if(it != null)
        {
            if(it.GetHouse() == this)
            {
                if(it.GetIsGrabbed() || !ItemIsInsideRadious(it))
                {
                    RemoveItem(it);

                    ret = true;
                }
            }
        }

        return ret;
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
            it.SetHouse(this);
            it.transform.parent = gameObject.transform;

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
                it.SetHouse(null);
                it.transform.parent = null;

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

        EventSetScore ev = new EventSetScore(player_instance, points);
        EventManager.Instance.SendEvent(ev);
    }

    private PlayerStats player_instance = null;

    private int points = 0;

    private List<Item> items_around = new List<Item>();
}
