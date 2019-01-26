using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    bool force_gun = false;
    int num_items = 0;

    public float spawn_time = 10f;
    public int spawn_chance = 70;

    private Timer timer = new Timer();

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.Suscribe(GameEventType.EVENT_START_QUEST, OnEvent);
        EventManager.Instance.Suscribe(GameEventType.EVENT_END_QUEST, OnEvent);
        EventManager.Instance.Suscribe(GameEventType.EVENT_MATCH_START, OnEvent);

        num_items =  ItemManager.Instance.GetItemsPrefabs().Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.ReadTime() > spawn_time) 
        {
            timer.Start();
            int rand = Random.Range(0, 99);
            if (rand < spawn_chance)
                SpawnItem();
        }
    }

    void SpawnItem() 
    {
        Item it = null;
        if (force_gun) 
        {
            it = Instantiate<Item>(ItemManager.Instance.GetItemPrefabByItemType(ItemType.ITEM_GUN), transform.position, Quaternion.identity);
            ItemManager.Instance.AddToItemsInstances(it);

            return;
        }
        
        int item_to_spawn = Random.Range(0, num_items - 2);

        it = Instantiate<Item>(ItemManager.Instance.GetItemsPrefabs()[item_to_spawn],transform.position,Quaternion.identity);
        ItemManager.Instance.AddToItemsInstances(it);
    }

    void OnEvent(GameEvent ev) {
        switch (ev.Type()) {
            case GameEventType.EVENT_START_QUEST: 
                {
                    EventStartQuest start = (EventStartQuest)ev;

                    if (start.quest == QuestType.QT_GUNS) {
                        force_gun = true;
                    }
                    break;
                }
            case GameEventType.EVENT_END_QUEST: 
                {
                    EventStartQuest start = (EventStartQuest)ev;

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
            default:
                Debug.LogError("ItemSpawner: Invalid call to OnEvent");
                break;
        }
    }
}
