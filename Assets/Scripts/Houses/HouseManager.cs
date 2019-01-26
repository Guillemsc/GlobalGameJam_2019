using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : Singleton<HouseManager>
{
    HouseManager()
    {
        InitInstance(this);
    }

    private void Awake()
    {
        InitEvents();
        InitQueueEvents();
    }

    private void InitEvents()
    {
        EventManager.Instance.Suscribe(GameEventType.EVENT_MAP_LOAD, OnEvent);
        EventManager.Instance.Suscribe(GameEventType.EVENT_MAP_UNLOAD, OnEvent);

        EventManager.Instance.Suscribe(GameEventType.EVENT_MATCH_START, OnEvent);
        EventManager.Instance.Suscribe(GameEventType.EVENT_MATCH_FINISH, OnEvent);
    }

    private void InitQueueEvents()
    {
        queue_context = QueueEventManager.Instance.CreateContext();
    }

    private void OnEvent(GameEvent ev)
    {
        switch(ev.Type())
        {
            case GameEventType.EVENT_MAP_LOAD:
                {
                    SpawnHouses();

                    break;
                }
            case GameEventType.EVENT_MAP_UNLOAD:
                {
                    DestroyHousesInstances();

                    break;
                }

            case GameEventType.EVENT_MATCH_START:
                {
                    StartHousesMovement();

                    break;
                }

            case GameEventType.EVENT_MATCH_FINISH:
                {
                    StopHousesMovement();

                    break;
                }
        }
    }

    private void SpawnHouses()
    {
        DestroyHousesInstances();

        for (int i = 0; i < house_spawn_positions.Length; ++i)
        {
            if(house_spawn_positions[i] != null)
                SpawnHouseInstance(house_spawn_positions[i].transform.position);
        }

        EventHousesSpawned ev = new EventHousesSpawned(houses);
        EventManager.Instance.SendEvent(ev);
    }

    private void StartHousesMovement()
    {
        float time = MatchManager.Instance.GetTotalMatchTime();

        for(int i = 0; i < houses.Count; ++i)
        {
            House curr_house = houses[i];

            queue_context.PushEvent(new
                QueueEventPosition(curr_house.gameObject, curr_house.transform.position,
                MatchManager.Instance.GetMapCenter(), time, EasingFunctionsType.LINEAR), true);
        }
    }

    private void StopHousesMovement()
    {
        queue_context.ClearEvents();
    }

    private void SpawnHouseInstance(Vector2 pos)
    {        
        GameObject new_house = Instantiate(house_prefab, pos, Quaternion.identity);

        if (new_house != null)
        {
            House stats = new_house.GetComponent<House>();

            houses.Add(stats);
        }
    }

    private void DestroyHousesInstances()
    {
        for(int i = 0; i < houses.Count; ++i)
        {
            Destroy(houses[i].gameObject);
        }

        houses.Clear();
    }

    [SerializeField]
    private GameObject house_prefab = null;

    [SerializeField]
    private GameObject[] house_spawn_positions = null;

    private List<House> houses = new List<House>();

    private QueueEventContext queue_context = null;
}
