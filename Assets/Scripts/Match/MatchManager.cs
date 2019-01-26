using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : Singleton<MatchManager>
{
    MatchManager()
    {
        InitInstance(this);
    }

    private void Awake()
    {
        EventManager.Instance.Suscribe(GameEventType.EVENT_MAP_LOAD, OnEvent);
        EventManager.Instance.Suscribe(GameEventType.EVENT_MAP_UNLOAD, OnEvent);

        UnloadMap();

        InitUI();
    }

    private void Update()
    {
        UpdateWaitBeforeStartMatch();

        UpdateMatch();
    }

    private void InitUI()
    {
        game_ui.SetActive(false);

        Button but = temp_game_ui.GetComponentInChildren<Button>();
        but.onClick.AddListener(TempOnButtonLoadMap);
    }

    private void TempOnButtonLoadMap()
    {
        EventMapLoad ev = new EventMapLoad();
        EventManager.Instance.SendEvent(ev);
    }

    private void OnEvent(GameEvent ev)
    {
        switch(ev.Type())
        {
            case GameEventType.EVENT_MAP_LOAD:
                {
                    LoadMap();

                    StartWaitBeforStartingMatch();

                    temp_game_ui.SetActive(false);

                    break;
                }

            case GameEventType.EVENT_MAP_UNLOAD:
                {
                    UnloadMap();

                    break;
                }
        }
    }

    public Vector3 GetMapCenter()
    {
        return map_center;
    }

    public bool GetWaitingToStartMatch()
    {
        return wating_to_start_match;
    }

    public float GetWaitingToStartMatchTotalTime()
    {
        return time_before_match_starts;
    }

    public float GetWaitingToStartMatchCurrTime()
    {
        return timer_before_match_starts.ReadTime();
    }

    public bool GetMatchStarted()
    {
        return match_started;
    }

    public float GetTotalMatchTime()
    {
        return time_match;
    }

    public float GetCurrMatchTime()
    {
        return timer_match.ReadTime();
    }

    private void LoadMap()
    {
        map.SetActive(true);
    }

    private void UnloadMap()
    {
        map.SetActive(false);
    }

    private void StartWaitBeforStartingMatch()
    {
        if(!wating_to_start_match)
        {
            wating_to_start_match = true;

            timer_before_match_starts.Start();
        }
    }

    private void UpdateWaitBeforeStartMatch()
    {
        if(wating_to_start_match)
        {
            if(timer_before_match_starts.ReadTime() > time_before_match_starts)
            {
                wating_to_start_match = false;
                timer_before_match_starts.Reset();

                StartMatch();
            }
        }
    }

    private void StartMatch()
    {
        if(!wating_to_start_match && !match_started)
        {
            match_started = true;

            timer_match.Start();

            List<PlayerStats> player_instances = PlayersManager.Instance.GetAllPlayerInstances();

            EventMatchStart ev = new EventMatchStart(time_match, player_instances);
            EventManager.Instance.SendEvent(ev);
        }
    }

    private void UpdateMatch()
    {
        if(match_started)
        {
            if(timer_match.ReadTime() > time_match)
            {
                FinishMatch();
            }
        }
    }

    private void FinishMatch()
    {
        if(match_started)
        {
            match_started = false;

            List<PlayerStats> player_instances = PlayersManager.Instance.GetAllPlayerInstances();

            EventMatchFinish ev = new EventMatchFinish(null, player_instances);
            EventManager.Instance.SendEvent(ev);
        }
    }

    [SerializeField]
    private GameObject game_ui = null;

    [SerializeField]
    private GameObject temp_game_ui = null;

    [SerializeField]
    private GameObject map = null;

    [SerializeField]
    private Vector3 map_center = Vector3.zero;

    [SerializeField]
    private float time_before_match_starts = 0.0f;

    [SerializeField]
    private float time_match = 0.0f;

    private Timer timer_match = new Timer();
    private bool match_started = false;

    private Timer timer_before_match_starts = new Timer();
    private bool wating_to_start_match = false;
}
