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

            if (house_end_positions.Length > i)
            {
                GameObject end_pos = house_end_positions[i];

                if (end_pos != null)
                {
                    end_pos.transform.position = new Vector3(end_pos.transform.position.x, end_pos.transform.position.y,
                        -5);

                    GameObject line_renderer = Instantiate(line_renderer_prefab, Vector3.zero, Quaternion.identity);
                    line_renderer.gameObject.transform.parent = curr_house.gameObject.transform;
                    LineRenderer lr = line_renderer.GetComponent<LineRenderer>();
                    lr.SetPosition(0, curr_house.gameObject.transform.position);
                    lr.SetPosition(1, end_pos.transform.position);

                    queue_context.PushEvent(new
                        QueueEventPosition(curr_house.gameObject, curr_house.transform.position,
                        end_pos.transform.position, time, EasingFunctionsType.LINEAR), true);
                }
            }
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

    public List<House> GetHousesOrderedByPoints()
    {
        List<House> ret = new List<House>();

        List<House> to_check = new List<House>(houses);

        while(to_check.Count > 0)
        {
            int max_points = 0;
            House house_max_points = null;

            for(int i = 0; i < to_check.Count; ++i)
            {
                House curr_house = to_check[i];

                if(curr_house.GetPoints() > max_points)
                {
                    house_max_points = curr_house;
                    max_points = curr_house.GetPoints();
                }
            }

            if(house_max_points != null)
            {
                to_check.Remove(house_max_points);
                ret.Add(house_max_points);
            }
        }

        return ret;
    }

    public float GetHouseItemRadious()
    {
        return house_item_radious;
    }

    [SerializeField]
    private GameObject house_prefab = null;

    [SerializeField]
    private GameObject line_renderer_prefab = null;

    [SerializeField]
    private GameObject[] house_spawn_positions = null;

    [SerializeField]
    private GameObject[] house_end_positions = null;

    [SerializeField]
    private float house_item_radious = 0.0f;

    private List<House> houses = new List<House>();

    private QueueEventContext queue_context = null;
}
