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

    List<Item> GetItemsPrefabs()
    {
        return items_prefabs;
    }

    [SerializeField]
    List<Item> items_prefabs = null;
}
