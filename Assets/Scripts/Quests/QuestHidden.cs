using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHidden : Quest {

    private void Start() {

    }

    void Update() {
    }

    public override void OnDisableQuest() {
        Debug.Log("Hidden Quest End");
    }

    public override void OnEnableQuest() {
        Debug.Log("Hidden Quest Start");
    }
}