using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private void Awake()
    {
        InitPlayer();
        InitRigidbody();
        InitEvents();
    }

    private void Start()
    {
        SetMovementEnabled(false);
        anim = stats.GetAnimator().GetComponent<Animator2D>();
    }

    private void Update()
    {
        if (movement_enabled)
        {
            PlayerInput();

            MovePlayer();

            if (rigid_body.velocity.magnitude != 0)
                particles_object.SetActive(true);
            else
                particles_object.SetActive(false);

        }

        CapSpeed();

        Flip();

        //Animations
        if(rigid_body.velocity.magnitude != 0f && !running) 
        {
            anim.PlayAnimation("Run",anim_speed*0.75f);
            running = true;
        }
        else if(rigid_body.velocity.magnitude == 0f && running)
        {
            anim.PlayAnimation("Idle", anim_speed);
            running = false;
        }
    }

    private void InitPlayer()
    {
        stats = gameObject.GetComponent<PlayerStats>();
    }

    private void InitRigidbody()
    {
        rigid_body = gameObject.GetComponent<Rigidbody2D>();
    }

    private void InitEvents()
    {
        EventManager.Instance.Suscribe(GameEventType.EVENT_START_QUEST, OnEvent);
        EventManager.Instance.Suscribe(GameEventType.EVENT_END_QUEST, OnEvent);
        EventManager.Instance.Suscribe(GameEventType.EVENT_MATCH_START, OnEvent);
    }

    public void SetMovementEnabled(bool set)
    {
        movement_enabled = set;
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
        if (input_magnitude > stats.GetJoystickDeadVal())
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

        if (input_magnitude > stats.GetJoystickDeadVal())
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

        float player_speed = player_max_speed + speed_delta;

        float max_speed_x = vel_norm.x * player_speed;
        float max_speed_y = vel_norm.y * player_speed;

        if (rigid_body.velocity.x > max_speed_x && max_speed_x > 0)
        {
            rigid_body.velocity = new Vector2(player_speed * vel_norm.x, rigid_body.velocity.y);
        }
        else if (rigid_body.velocity.x < max_speed_x && max_speed_x < 0)
        {
            rigid_body.velocity = new Vector2(player_speed * vel_norm.x, rigid_body.velocity.y);
        }

        if (rigid_body.velocity.y > max_speed_y && max_speed_y > 0)
        {
            rigid_body.velocity = new Vector2(rigid_body.velocity.x, player_speed * vel_norm.y);
        }
        else if (rigid_body.velocity.y < max_speed_y && max_speed_y < 0)
        {
            rigid_body.velocity = new Vector2(rigid_body.velocity.x, player_speed * vel_norm.y);
        }       
    }

    void Flip() 
    {
        if(rigid_body.velocity.x < -0.1f) 
        {
            Vector3 scale = stats.GetAnimator().transform.localScale;
            scale.x = 1;
            stats.GetAnimator().transform.localScale = scale;
            
        }
        else if (rigid_body.velocity.x > 0.1f) 
        {
            Vector3 scale = stats.GetAnimator().transform.localScale;
            scale.x = -1;
            stats.GetAnimator().transform.localScale = scale;
        }
    }

    void OnEvent(GameEvent ev) 
    {
        switch (ev.Type()) {

            case GameEventType.EVENT_START_QUEST: 
                {
                    EventStartQuest quest = (EventStartQuest)ev;

                    if(quest.quest == QuestType.QT_SPEEDS) 
                    {
                        HouseManager.Instance.GetHousesOrderedByPoints();
                        if (HouseManager.Instance.GetHousesOrderedByPoints()[0].GetPlayerInstance().gameObject == gameObject)
                            speed_delta = player_max_speed * -0.5f;
                        else if(HouseManager.Instance.GetHousesOrderedByPoints()[2].GetPlayerInstance().gameObject == gameObject)
                            speed_delta = player_max_speed * 0.5f;
                    }
                    break;
                }
            case GameEventType.EVENT_END_QUEST:
                speed_delta = 0f;
                break;

            case GameEventType.EVENT_MATCH_START:
                {
                    SetMovementEnabled(true);
                    break;
                }

            default:
                break;
        }
    }

    [SerializeField]
    private GameObject particles_object;

    [SerializeField]
    private float player_max_speed = 0.0f;

    [SerializeField]
    private float player_acceleration = 0.0f;

    [SerializeField]
    private float player_deceleration = 0.0f;

    private PlayerStats stats = null;
    private Rigidbody2D rigid_body = null;
    private Animator2D anim = null;

    private Vector2 input = Vector2.zero;
    private float input_magnitude = 0.0f;
    private float movement_angle = 0.0f;

    private float speed_delta = 0;
    private bool movement_enabled = true;
    private bool running = true;

    public float anim_speed = 1f;

}
