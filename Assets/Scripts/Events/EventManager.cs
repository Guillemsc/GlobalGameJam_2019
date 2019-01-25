using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    EventManager()
    {
        InitInstance(this);
    }

    public void SendEvent(GameEvent ev)
    {
        if (ev != null)
        {
            if (event_delegates.ContainsKey(ev.Type()))
            {
                OnEventDel del = event_delegates[ev.Type()];

                if (del != null)
                    del(ev);
            }
        }
    }

    public void Suscribe(GameEventType type, OnEventDel callback)
    {
        if (event_delegates.ContainsKey(type))
        {
             event_delegates[type] += callback;
        }
        else
        {
            OnEventDel del = null;
            del += callback;
            event_delegates.Add(type, del);
        }
    }

    public void UnSuscribe(OnEventDel del, GameEventType type)
    {
        if (event_delegates.ContainsKey(type))
        {
            event_delegates[type] -= del;
        }
    }

    public delegate void OnEventDel(GameEvent ev);

    Dictionary<GameEventType, OnEventDel> event_delegates = new Dictionary<GameEventType, OnEventDel>();
}