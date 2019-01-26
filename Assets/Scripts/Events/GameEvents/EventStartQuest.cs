using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStartQuest : GameEvent {
    public EventStartQuest(QuestType quest) : base(GameEventType.EVENT_START_QUEST) 
    {
        this.quest = quest;
    }
    public QuestType quest = QuestType.QT_NULL;
}
