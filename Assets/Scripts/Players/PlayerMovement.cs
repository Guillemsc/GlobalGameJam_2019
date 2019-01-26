using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private void Awake()
    {
        InitPlayer();
        InitRigidbody();
    }

    private void Update()
    {
        PlayerInput();

        MovePlayer();

        CapSpeed();
    }

    private void InitPlayer()
    {
        stats = gameObject.GetComponent<PlayerStats>();
    }

    private void InitRigidbody()
    {
        rigid_body = gameObject.GetComponent<Rigidbody2D>();
    }

    public void SetMaxSpeed(float max_speed)
    {
        player_max_speed = max_speed;
    }

    public void SetAcceleration(float acceleration)
    {
        player_acceleration = acceleration;
    }

    public void SetDeceleration(float decelration)
    {
        player_deceleration = decelration;
    }

    private void PlayerInput()
    {
        Player player = stats.GetPlayer();

        input = new Vector2(0, 0);
        input_magnitude = 0.0f;

        if (player.HasGamepad())
        {
            input.x = player.LeftJoystickHorizontal();
            input.y = -player.LeftJoystickVertical();

            movement_angle = Utils.AngleFromTwoPoints(new Vector2(0, 0), input);

            input_magnitude = input.sqrMagnitude;
        }
    }

    private void MovePlayer()
    {
        if (input.x > joystick_dead_val || input.x < -joystick_dead_val)
        {
            float dt_acceleration = player_acceleration * Time.deltaTime;

            Vector2 acc_vec = new Vector2(Mathf.Cos(movement_angle * Mathf.Deg2Rad) * dt_acceleration, 0);

            rigid_body.velocity += acc_vec;
        }
        else
        {
            float dt_deceleration = player_deceleration * Time.deltaTime;

            float deceleration_val = 0;

            if (rigid_body.velocity.x > 0)
            {
                deceleration_val -= dt_deceleration;

                if (rigid_body.velocity.x + deceleration_val < 0)
                    deceleration_val = -rigid_body.velocity.x;
            }

            else if (rigid_body.velocity.x < 0)
            {
                deceleration_val += dt_deceleration;

                if (rigid_body.velocity.x - deceleration_val > 0)
                    deceleration_val = -rigid_body.velocity.x;
            }

            rigid_body.velocity += new Vector2(deceleration_val, 0);
        }

        if (input.y > joystick_dead_val || input.y < -joystick_dead_val)
        {
            float dt_acceleration = player_acceleration * Time.deltaTime;

            Vector2 acc_vec = new Vector2(0, Mathf.Sin(movement_angle * Mathf.Deg2Rad) * dt_acceleration);

            rigid_body.velocity += acc_vec;
        }
        else
        {
            float dt_deceleration = player_deceleration * Time.deltaTime;

            float deceleration_val = 0;

            if (rigid_body.velocity.y > 0)
            {
                deceleration_val -= dt_deceleration;

                if (rigid_body.velocity.y + deceleration_val < 0)
                    deceleration_val = -rigid_body.velocity.y;
            }

            else if (rigid_body.velocity.y < 0)
            {
                deceleration_val += dt_deceleration;

                if (rigid_body.velocity.y - deceleration_val > 0)
                    deceleration_val = -rigid_body.velocity.y;
            }

            rigid_body.velocity += new Vector2(0, deceleration_val);
        }
    }

    private void CapSpeed()
    {        
        Vector2 vel_norm = rigid_body.velocity.normalized;

        float max_speed_x = vel_norm.x * player_max_speed;
        float max_speed_y = vel_norm.y * player_max_speed;

        if (rigid_body.velocity.x > max_speed_x && max_speed_x > 0)
        {
            rigid_body.velocity = new Vector2(player_max_speed * vel_norm.x, rigid_body.velocity.y);
        }
        else if (rigid_body.velocity.x < max_speed_x && max_speed_x < 0)
        {
            rigid_body.velocity = new Vector2(player_max_speed * vel_norm.x, rigid_body.velocity.y);
        }

        if (rigid_body.velocity.y > max_speed_y && max_speed_y > 0)
        {
            rigid_body.velocity = new Vector2(rigid_body.velocity.x, player_max_speed * vel_norm.y);
        }
        else if (rigid_body.velocity.y < max_speed_y && max_speed_y < 0)
        {
            rigid_body.velocity = new Vector2(rigid_body.velocity.x, player_max_speed * vel_norm.y);
        }       
    }

    [SerializeField]
    private float joystick_dead_val = 0.0f;

    [SerializeField]
    private float player_max_speed = 0.0f;

    [SerializeField]
    private float player_acceleration = 0.0f;

    [SerializeField]
    private float player_deceleration = 0.0f;

    private PlayerStats stats = null;
    private Rigidbody2D rigid_body = null;

    private Vector2 input = Vector2.zero;
    private float input_magnitude = 0.0f;
    private float movement_angle = 0.0f;
}
