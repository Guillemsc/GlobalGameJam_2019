using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private void Awake()
    {
        InitPlayer();

        InitEvents();
    }

    private void Update()
    {
        UpdateGrabInput();

        RotationInput();

        UpdateItemRotation();
    }

    private void InitPlayer()
    {
        stats = gameObject.GetComponent<PlayerStats>();
    }

    private void InitEvents()
    {
        EventManager.Instance.Suscribe(GameEventType.EVENT_ITEM_GRABBED, OnEvent);
        EventManager.Instance.Suscribe(GameEventType.EVENT_ITEM_DROPPED, OnEvent);
    }

    private void OnEvent(GameEvent ev)
    {
        switch (ev.Type())
        {
            case GameEventType.EVENT_ITEM_GRABBED:
                {
                    EventItemGrabbed r_ev = (EventItemGrabbed)ev;

                    if (r_ev.player == stats)
                    {
                        r_ev.item.gameObject.transform.parent = item_parent.transform;
                        r_ev.item.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                        r_ev.item.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
                    }

                    break;
                }
            case GameEventType.EVENT_ITEM_DROPPED:
                {
                    EventItemDropped r_ev = (EventItemDropped)ev;

                    if (r_ev.player == stats)
                    {

                    }

                    break;
                }
            default:
                break;
        }
    }

    private void UpdateGrabInput()
    {
        Player pl = stats.GetPlayer();

        if (pl != null)
        {
            if (pl.HasGamepad())
            {
                if (pl.GetKeyA())
                {
                    ItemManager.Instance.PlayerTryGrabItem(stats);
                }
                else if(pl.GetKeyB())
                {
                    ItemManager.Instance.StopGrabbingItem(stats);
                }
            }
        }
    }

    private void RotationInput()
    {
        Player player = stats.GetPlayer();

        if (player != null)
        {
            input = new Vector2(0, 0);
            input_magnitude = 0.0f;

            if (player.HasGamepad())
            {
                input.x = player.RightJoystickHorizontal();
                input.y = -player.RightJoystickVertical();

                input_angle = Utils.AngleFromTwoPoints(new Vector2(0, 0), input);

                input_magnitude = input.sqrMagnitude;
            }
        }
    }

    private void UpdateItemRotation()
    {
        if (input_magnitude > stats.GetJoystickDeadVal())
        {
            item_pivot.transform.localEulerAngles = new Vector3(0, 0, input_angle);
        }
    }

    [SerializeField]
    private GameObject item_pivot = null;

    [SerializeField]
    private GameObject item_parent = null;

    private PlayerStats stats = null;

    private Vector2 input = Vector2.zero;
    private float input_magnitude = 0.0f;
    private float input_angle = 0.0f;
}
