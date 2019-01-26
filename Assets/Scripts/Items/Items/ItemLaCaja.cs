using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLaCaja: Item {

    PlayerStats last_player = null;

    public int points_gain = 50;

    private void Awake() {

    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public override void OnPlayerGrab(PlayerStats player) 
    {
        destroyed = false;
        sr.color = Color.white;

        if(last_player != player) 
        {
            points_to_give += points_gain;
            last_player = player;
        }
    }
}
