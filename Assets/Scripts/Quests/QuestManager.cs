using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    [Tooltip("When each quest starts and how long it lasts in seconds")]
    public Vector2[] quest_start_duration = new Vector2[0];

    public List<Quest> quests = new List<Quest>();

    private Timer timer = new Timer();
    private Timer quest_timer = new Timer();

    private int quest_index = 0;
    private int active_quest = 0;
    private int quests_played = 0;

    QuestManager() 
    {
        InitInstance(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.Suscribe(GameEventType.EVENT_MATCH_START, OnEvent);
    }

    // Update is called once per frame
    void Update()
    {
        if (quests.Count > 0 && quest_index == quests_played)
        {
            if (timer.ReadTime() > quest_start_duration[quest_index].x && quests[active_quest].gameObject.activeSelf == false)
            {
                active_quest = Random.Range(0, quests.Count - 1);

                quests[active_quest].SetActive(true);
                quest_timer.Start();
            }

            if (quest_timer.ReadTime() > quest_start_duration[quest_index].y)
            {
                quest_timer.Reset();
                quests_played++;

                quests[active_quest].SetActive(false);

                if(quest_index<quests.Count-1)
                    quest_index++;
            }
        }
    }

    public Quest GetActiveQuest() {
        return quests[active_quest];
    }

    public int GetQuestTime() 
    {
        return Mathf.CeilToInt(quest_start_duration[quest_index].y - quest_timer.ReadTime());
    }

    public int GetNextQuestRemainingTime() {
        return Mathf.CeilToInt(quest_start_duration[quest_index].x - quest_timer.ReadTime());
    }

    public void OnEvent(GameEvent ev) 
    {
        switch (ev.Type()) {
            case GameEventType.EVENT_MATCH_START:
                timer.Start();
                break;
            default:
                break;
        }
    }
}
