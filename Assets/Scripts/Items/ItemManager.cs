using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    ItemManager()
    {
        InitInstance(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.Suscribe(GameEventType.EVENT_START_QUEST, OnEvent);
        EventManager.Instance.Suscribe(GameEventType.EVENT_END_QUEST, OnEvent);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateItems();
    }

    public List<Item> GetItemsPrefabs()
    {
        return items_prefabs;
    }

    public Item GetItemPrefabByItemType(ItemType type)
    {
        Item ret = null;

        for(int i = 0; i < items_prefabs.Count; ++i)
        {
            if(items_prefabs[i].Type() == type)
            {
                ret = items_prefabs[i];
                break;
            }
        }

        return ret;
    }

    public int GetItemsCount()
    {
        return items_prefabs.Count;
    }

    public void AddToItemsInstances(Item it)
    {
        if(it != null)
        {
            item_instances.Add(it);
            it.BaseStart();
            if (hidden_items)
                it.SetHidden();
        }
    }

    public void RemoveFromitemsInstances(Item it)
    {
        if (it != null)
        {
            item_instances.Remove(it);
        }
    }

    public void PlayerTryGrabItem(PlayerStats player_ins)
    {
        if (player_ins != null)
        {
            if (!player_ins.GetHasGrabbedItem())
            {
                Item closest_item = null;
                float closest_item_distance = float.MaxValue;

                for (int i = 0; i < item_instances.Count; ++i)
                {
                    Item curr_item = item_instances[i];

                    if (!curr_item.GetIsGrabbed())
                    {
                        float dist = Vector2.Distance(curr_item.gameObject.transform.position, player_ins.gameObject.transform.position);

                        dist = Mathf.Abs(dist);

                        if (dist <= min_item_distance_to_grab)
                        {
                            if (dist < closest_item_distance)
                            {
                                closest_item = curr_item;
                                closest_item_distance = dist;
                            }
                        }
                    }
                }

                if(closest_item != null)
                {
                    StartGrabbingItem(player_ins, closest_item);
                }
            }
        }
    }

    public void StartGrabbingItem(PlayerStats ins, Item it)
    {
        if(ins != null && it != null)
        {
            if(!it.GetIsGrabbed() && !ins.GetHasGrabbedItem())
            {
                ins.SetGrabbedItem(it);
                it.SetGrabbedBy(ins);

                EventItemGrabbed ev = new EventItemGrabbed(it, ins);
                EventManager.Instance.SendEvent(ev);

                it.OnPlayerGrabBase(ins);
                it.OnPlayerGrab(ins);
            }
        }
    }

    public void StopGrabbingItem(PlayerStats ins)
    {
        if(ins != null)
        {
            Item grabbed_item = ins.GetGrabbedItem();

            if (grabbed_item != null)
            {
                grabbed_item.OnPlayerThrows();

                grabbed_item.transform.parent = null;

                grabbed_item.SetGrabbedBy(null);
            }

            ins.SetGrabbedItem(null);

            EventItemDropped ev = new EventItemDropped(grabbed_item, ins);
            EventManager.Instance.SendEvent(ev);
        }
    }

    private void UpdateItems()
    {
        for(int i = 0; i < item_instances.Count; ++i)
        {
            Item curr_item = item_instances[i];

            if(curr_item.GetIsGrabbed())
                curr_item.OnPlayerGrabbed();
        }
    }

    void HiddeItems() {
        foreach(Item i in item_instances) {
            if(!i.GetIsGrabbed())
                i.SetHidden();
                }
    }

    void ShowItems() {
        foreach (Item i in item_instances) {
            i.SetBaseSprite();
        }
    }

    public void OnEvent(GameEvent ev) {
        switch (ev.Type()) {
            case GameEventType.EVENT_START_QUEST: {
                    EventStartQuest start = (EventStartQuest)ev;
                    
                    if(start.quest == QuestType.QT_HIDDEN) {
                        hidden_items = true;
                        HiddeItems();
                    }
                break;
                }
            case GameEventType.EVENT_END_QUEST: { 

                EventEndQuest end = (EventEndQuest)ev;

                if (end.quest == QuestType.QT_HIDDEN) {
                    hidden_items = false;
                    ShowItems();
                }
                break;
                }
            default:
                break;
        }
    }

    [SerializeField]
    private float min_item_distance_to_grab = 0.0f;

    [SerializeField]
    private List<Item> items_prefabs = null;

    private List<Item> item_instances = new List<Item>();

    private bool hidden_items = false;
}
