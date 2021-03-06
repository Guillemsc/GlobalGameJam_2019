﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    bool force_gun = false;
    int num_items = 0;

    public float spawn_time = 10f;
    public int spawn_chance = 70;

    private Timer timer = new Timer();

    Item spawned_item = null;

    public bool LaCaja_spawn = false;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.Suscribe(GameEventType.EVENT_START_QUEST, OnEvent);
        EventManager.Instance.Suscribe(GameEventType.EVENT_END_QUEST, OnEvent);
        EventManager.Instance.Suscribe(GameEventType.EVENT_MATCH_START, OnEvent);
        EventManager.Instance.Suscribe(GameEventType.EVENT_ITEM_GRABBED, OnEvent);

        num_items =  ItemManager.Instance.GetItemsPrefabs().Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.ReadTime() > spawn_time) 
        {
            timer.Reset();
            timer.Start();
            if (spawned_item == null) 
            {
                int rand = Random.Range(0, 100);
                if (rand < spawn_chance)
                    SpawnItem();
            }
        }
    }

    void SpawnItem() 
    {
        if (force_gun) 
        {
            spawned_item = Instantiate(ItemManager.Instance.GetItemPrefabByItemType(ItemType.ITEM_GUN), transform.position, Quaternion.identity);
            ItemManager.Instance.AddToItemsInstances(spawned_item);

            return;
        }
        
        int item_to_spawn = Random.Range(0, num_items - 1);

        spawned_item = Instantiate<Item>(ItemManager.Instance.GetItemsPrefabs()[item_to_spawn],transform.position,Quaternion.identity);
        ItemManager.Instance.AddToItemsInstances(spawned_item);
    }

    void SpawnLACAJA() {
        spawned_item = Instantiate(ItemManager.Instance.GetItemPrefabByItemType(ItemType.ITEM_LACAJA), transform.position, Quaternion.identity);
        ItemManager.Instance.AddToItemsInstances(spawned_item);
    }

    void OnEvent(GameEvent ev) {
        switch (ev.Type()) {
            case GameEventType.EVENT_START_QUEST: 
                {
                    EventStartQuest start = (EventStartQuest)ev;

                    if (start.quest == QuestType.QT_GUNS) {
                        force_gun = true;
                    }
                    if(start.quest == QuestType.QT_LACAJA && LaCaja_spawn) {
                        if(spawned_item != null) {
                            ItemManager.Instance.RemoveFromitemsInstances(spawned_item);
                            GameObject.Destroy(spawned_item);
                        }
                        SpawnLACAJA();
                    }
                    break;
                }
            case GameEventType.EVENT_END_QUEST: 
                {
                    EventEndQuest start = (EventEndQuest)ev;

                    if (start.quest == QuestType.QT_GUNS) {
                        force_gun = false;
                    }
                    break;
                }
            case GameEventType.EVENT_MATCH_START: 
                {
                    SpawnItem();
                    timer.Start();

                    break;
                }
            case GameEventType.EVENT_ITEM_GRABBED: {

                    EventItemGrabbed grab = (EventItemGrabbed)ev;
                    if (grab.item == spawned_item)
                        spawned_item = null;

                    break;
                }
            default:
                Debug.LogError("ItemSpawner: Invalid call to OnEvent");
                break;
        }
    }
}
