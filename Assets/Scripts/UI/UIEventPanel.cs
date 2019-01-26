using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventPanel : MonoBehaviour
{
    public float panel_move_speed = 30f;
    float y_pos = 0;
    RectTransform trans = null;

    public UIEventText ev_text;
    public UIEventTime ev_time;

    bool quest_active = false;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<RectTransform>();
        y_pos = -trans.rect.height;
        EventManager.Instance.Suscribe(GameEventType.EVENT_START_QUEST, OnEvent);
        EventManager.Instance.Suscribe(GameEventType.EVENT_END_QUEST, OnEvent);
    }

    // Update is called once per frame
    void Update()
    {
        if (y_pos != trans.position.y) 
        {
            Vector3 pos = trans.position;
            if(y_pos > trans.position.y) 
            {
                pos.y -= panel_move_speed * Time.deltaTime;
            }
            else if (y_pos < trans.position.y) {
                pos.y += panel_move_speed * Time.deltaTime;
            }
            trans.position = pos;
        }

        if(quest_active) 
        {
            ev_time.SetTime(QuestManager.Instance.GetQuestTime());
        }
    }

    void OnEvent(GameEvent ev) {

        switch (ev.Type()) {
            case GameEventType.EVENT_START_QUEST:
                quest_active = true;
                y_pos = 0f;

                ev_text.SetText(QuestManager.Instance.GetActiveQuest().description);
                break;
            case GameEventType.EVENT_END_QUEST:
                quest_active = false;
                y_pos = -50f;
                break;
            default:
                Debug.LogError("UIEventPanel: Invalid call to OnEvent");
                break;
        }
    }
}
