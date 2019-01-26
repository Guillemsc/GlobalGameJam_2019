using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSpeed : Quest {

    private void Start() {

    }

    void Update() {
    }

    public override void OnDisableQuest() {
        Debug.Log("Speed Quest End");
    }

    public override void OnEnableQuest() {
        Debug.Log("Speed Quest Start");
    }
}