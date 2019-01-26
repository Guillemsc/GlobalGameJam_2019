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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Item> GetItemsPrefabs()
    {
        return items_prefabs;
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
        }
    }

    public void RemoveFromitemsInstances(Item it)
    {
        if (it != null)
        {
            item_instances.Remove(it);
        }
    }

    public void PlayerTryGrabItem(PlayerStats player_instance)
    {

    }

    [SerializeField]
    private float min_item_distance_to_grab = 0.0f;

    [SerializeField]
    private List<Item> items_prefabs = null;

    private List<Item> item_instances = null;
}
