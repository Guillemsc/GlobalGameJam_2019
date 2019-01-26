using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : Singleton<MatchManager>
{
    MatchManager()
    {
        InitInstance(this);
    }

    private void Awake()
    {
        EventManager.Instance.Suscribe(GameEventType.EVENT_MAP_LOAD, OnEvent);
    }

    private void Update()
    {
        UpdateWaitBeforeStartMatch();

        UpdateMatch();
    }

    private void OnEvent(GameEvent ev)
    {
        switch(ev.Type())
        {
            case GameEventType.EVENT_MAP_LOAD:
                {
                    StartWaitBeforStartingMatch();

                    break;
                }
        }
    }

    public bool GetWaitingToStartMatch()
    {
        return wating_to_start_match;
    }

    public float GetWaitingToStartMatchTime()
    {
        return timer_before_match_starts.ReadTime();
    }

    public bool GetMatchStarted()
    {
        return match_started;
    }

    public float GetMatchTime()
    {
        return timer_match.ReadTime();
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
    private GameObject map = null;

    [SerializeField]
    private float time_before_match_starts = 0.0f;

    [SerializeField]
    private float time_match = 0.0f;

    private Timer timer_match = new Timer();
    private bool match_started = false;

    private Timer timer_before_match_starts = new Timer();
    private bool wating_to_start_match = false;
}
