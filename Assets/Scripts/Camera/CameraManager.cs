using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public float player_radius = 1.5f;
    public Cinemachine.CinemachineTargetGroup target_group;

    void Start() {
        EventManager.Instance.Suscribe(GameEventType.EVENT_PLAYER_SPAWNED, OnEvent);
        EventManager.Instance.Suscribe(GameEventType.EVENT_PLAYER_DESPAWNED, OnEvent);
    }

    void OnEvent(GameEvent ev) 
    {
        switch (ev.Type()) {
            case GameEventType.EVENT_PLAYER_SPAWNED:
                EventPlayerSpawned spawn = (EventPlayerSpawned)ev;
                target_group.AddTarget(spawn.player.transform, player_radius);
                break;
            case GameEventType.EVENT_PLAYER_DESPAWNED:
                EventPlayerDeSpawned despawn = (EventPlayerDeSpawned)ev;
                target_group.RemoveTarget(despawn.player.transform);
                break;
            default:
                Debug.LogError("CameraManager: Invalid call to OnEvent");
                break;
        }
    }

    public void AddToTargetGroup(Transform trans) 
    {
        target_group.AddTarget(trans, player_radius);
    }

    public void RemonveFromTargetGroup(Transform trans) 
    {
        target_group.RemoveTarget(trans);
    }
}
