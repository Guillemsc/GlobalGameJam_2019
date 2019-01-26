using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventPanel : MonoBehaviour
{
    public float panel_move_time = 1f;
    public EasingFunctionsType panel_move_type = EasingFunctionsType.BOUNCE;

    float y_pos = 0;
    RectTransform trans = null;

    public UIEventText ev_text;
    public UIEventTime ev_time;

    bool quest_active = false;

    private void Awake() 
    {
        queue_context = QueueEventManager.Instance.CreateContext();
    }

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

        if(quest_active) 
        {
            ev_time.SetTime(QuestManager.Instance.GetQuestTime());
        }
    }

    void OnEvent(GameEvent ev) {

        switch (ev.Type()) 
        {
            case GameEventType.EVENT_START_QUEST: 
                {
                    quest_active = true;

                    ev_text.SetText(QuestManager.Instance.GetActiveQuest().description);
                    Vector3 pos = gameObject.transform.position;
                    pos.y = 0f;
                    queue_context.PushEvent(new QueueEventPosition(gameObject, transform.position, pos, panel_move_time, panel_move_type));

                    break;
                }
            case GameEventType.EVENT_END_QUEST: 
                {
                    quest_active = false;

                    if (this == null)
                        break;

                    Vector3 pos = gameObject.transform.position;
                    pos.y = -50f;
                    queue_context.PushEvent(new QueueEventPosition(gameObject, transform.position, pos, panel_move_time, panel_move_type));

                    break;
                }
            default:
                Debug.LogError("UIEventPanel: Invalid call to OnEvent");
                break;
        }
    }

    private QueueEventContext queue_context = null;
}
