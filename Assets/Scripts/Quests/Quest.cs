﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType {
    QT_LACAJA,

    QT_GUNS,
    QT_SPEEDS,
    QT_HIDDEN,

    QT_NULL
}

public abstract class Quest : MonoBehaviour
{
    [Tooltip("Text displayed on event panel")]
    public string description = "Event Text";

    public QuestType quest_type = QuestType.QT_NULL;

    // Start is called before the first frame update
    void Start()
    {
        SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive(bool state) 
    {
        gameObject.SetActive(state);
    }

    void OnEnable() 
    {
        OnEnableQuest();
        EventStartQuest ev = new EventStartQuest(quest_type);
        EventManager.Instance.SendEvent(ev);
    }

    void OnDisable() 
    {
        OnDisableQuest();

        EventEndQuest ev = new EventEndQuest(quest_type);

        if(EventManager.Instance != null)
            EventManager.Instance.SendEvent(ev);
    }

    public abstract void OnEnableQuest();

    public abstract void OnDisableQuest();
}
