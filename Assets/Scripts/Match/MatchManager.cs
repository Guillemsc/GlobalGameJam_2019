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

        InitQueueEvent();

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

        timer_before_match_text.gameObject.SetActive(false);

    }

    private void InitQueueEvent()
    {
        queue_context = QueueEventManager.Instance.CreateContext();
    }

    private void OnEvent(GameEvent ev)
    {
        switch(ev.Type())
        {
            case GameEventType.EVENT_MAP_LOAD:
                {
                    LoadMap();

                    StartWaitBeforStartingMatch();

                    game_ui.SetActive(true);

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

            // UI Animation
            queue_context.PushEvent(new QueueEventWaitTime(0.3f));

            queue_context.PushEvent(new
            QueueEventScale(timer_before_match_text.gameObject,
            new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0.1f, EasingFunctionsType.EXPO_IN), true);

            queue_context.PushEvent(new QueueEventSetActive(timer_before_match_text.gameObject, true));

            queue_context.PushEvent(new 
                QueueEventScale(timer_before_match_text.gameObject, 
                new Vector3(0, 0, 0), new Vector3(2, 2, 2), 0.3f, EasingFunctionsType.EXPO_IN));

            queue_context.PushEvent(new QueueEventWaitTime(time_before_match_starts - 0.5f));

            queue_context.PushEvent(new
                QueueEventScale(timer_before_match_text.gameObject,
                new Vector3(2, 2, 2), new Vector3(0, 0, 0), 0.2f, EasingFunctionsType.EXPO_OUT));

            queue_context.PushEvent(new QueueEventFade(panel_before_match, 1, 0, 0.2f, EasingFunctionsType.LINEAR), true);

            queue_context.PushEvent(new QueueEventSetActive(timer_before_match_text.gameObject, false));

            // --------------
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
            else
            {
                float left_time = time_before_match_starts - timer_before_match_starts.ReadTime();
                int left_time_int = Mathf.RoundToInt(left_time);

                if (left_time_int > 0)
                    timer_before_match_text.text = left_time_int.ToString();
                else
                    timer_before_match_text.text = "Start!";
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

    // UI ----------

    [SerializeField]
    private GameObject game_ui = null;

    [SerializeField]
    private TMPro.TextMeshProUGUI timer_before_match_text = null;

    [SerializeField]
    private GameObject panel_before_match = null;

    [SerializeField]
    private GameObject map = null;

    // ----------

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

    private QueueEventContext queue_context = null;
}
