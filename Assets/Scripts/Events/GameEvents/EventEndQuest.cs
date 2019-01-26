using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEndQuest : GameEvent {
    public EventEndQuest(QuestType quest) : base(GameEventType.EVENT_END_QUEST) {
        this.quest = quest;
    }
    QuestType quest = QuestType.QT_NULL;
}
