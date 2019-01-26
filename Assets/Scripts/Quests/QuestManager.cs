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

    // Start is called before the first frame update
    void Start()
    {
        timer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (quests.Count > 0)
        {
            if (timer.ReadTime() > quest_start_duration[quest_index][0] && quests[active_quest].isActiveAndEnabled == false)
            {
                active_quest = Random.Range(0, quests.Count - 1);

                quests[active_quest].SetActive(true);
                quest_timer.Start();
            }

            if (quest_timer.ReadTime() > quest_start_duration[quest_index][1])
            {
                quest_timer.Reset();

                quests[active_quest].SetActive(false);

                quest_index++;
            }
        }
    }

    public Quest GetActiveQuest() {
        return quests[active_quest];
    }

    public int GetQuestTime() 
    {
        return Mathf.CeilToInt(quest_timer.ReadTime());
    }

    public int GetNextQuestRemainingTime() {
        return Mathf.CeilToInt(quest_start_duration[quest_index][0] - quest_timer.ReadTime());
    }
}
