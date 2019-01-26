using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTest : Quest
{
    Timer timer = new Timer();

    private void Start() {
        timer.Start();
    }

    void Update() 
    {
        if(timer.ReadTime() % 5 == 0) {
            Debug.Log("Test Quest Active");
        }
    }

    public override void OnDisableQuest() {
        Debug.Log("Test Quest End");
    }

    public override void OnEnableQuest() {
        Debug.Log("Test Quest Start");
    }
}