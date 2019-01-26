using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGuns : Quest
{

    private void Start() {
        
    }

    void Update() 
    {
    }

    public override void OnDisableQuest() {
        Debug.Log("Guns Quest End");
    }

    public override void OnEnableQuest() {
        Debug.Log("Guns Quest Start");
    }
}