using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayer : MonoBehaviour {
    private PlayerStats player = null;

    public UIPlayerScore score;

    // Start is called before the first frame update
    void Start() {
        EventManager.Instance.Suscribe(GameEventType.EVENT_ADD_SCORE, OnEvent);
        EventManager.Instance.Suscribe(GameEventType.EVENT_SUBSTRACT_SCORE, OnEvent);
    }

    // Update is called once per frame
    void Update() {

    }

    public void SetPlayer(PlayerStats player) 
    {
        if (this.player == null)
            this.player = player;
        else
            Debug.LogError("UIPlayer: Error setting player: Already set.");
    }

    public void OnEvent(GameEvent ev) 
    {
        switch (ev.Type()) {
            case GameEventType.EVENT_ADD_SCORE: 
            { 
                EventAddScore add_score = (EventAddScore)ev;

                if (add_score.player != player)
                    break;

                score.AddScore(add_score.amount);
               
                break;
            }
            case GameEventType.EVENT_SUBSTRACT_SCORE: 
            {
                EventSubstractScore sub_score = (EventSubstractScore)ev;

                if (sub_score.player != player)
                    break;

                score.SubstractScore(sub_score.amount);
                break;
            }
            default:
                Debug.LogError("UIPlayer: Invalid call to OnEvent");
                break;
        }
    }
    
}
