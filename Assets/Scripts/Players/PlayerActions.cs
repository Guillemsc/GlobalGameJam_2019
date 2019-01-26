using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private void Awake()
    {
        InitPlayer();
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
            }
        }
    }

    private void RotationInput()
    {
        Player player = stats.GetPlayer();

        input = new Vector2(0, 0);
        input_magnitude = 0.0f;

        if (player.HasGamepad())
        {
            input.x = player.LeftJoystickHorizontal();
            input.y = -player.LeftJoystickVertical();

            input_angle = Utils.AngleFromTwoPoints(new Vector2(0, 0), input);

            input_magnitude = input.sqrMagnitude;
        }
    }

    private void UpdateItemRotation()
    {
        if (input_magnitude > stats.GetJoystickDeadVal())
        {
            item_parent.transform.eulerAngles = new Vector3(0, 0, input_angle);
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
