using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayer : MonoBehaviour {
    private PlayerStats player = null;

    public UIPlayerScore score;

    // Start is called before the first frame update
    void Start() {
        EventManager.Instance.Suscribe(GameEventType.EVENT_SET_SCORE, OnEvent);
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
            case GameEventType.EVENT_SET_SCORE: 
            { 
                EventSetScore add_score = (EventSetScore)ev;

                if (add_score.player != player)
                    break;

                score.SetScore(add_score.amount);
               
                break;
            }
            default:
                Debug.LogError("UIPlayer: Invalid call to OnEvent");
                break;
        }
    }
    
}
