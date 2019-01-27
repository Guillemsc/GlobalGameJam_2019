using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSelectionUI : Singleton<ControllerSelectionUI>
{
    ControllerSelectionUI()
    {
        InitInstance(this);
    }

    private void Awake()
    {
        InitQueueEvent();
    }

    private void Update()
    {
        if (selecting_gamepads)
        {
            UpdateControllersInput();

            CheckReady();
        }
    }

    private void InitQueueEvent()
    {
        queue_context = QueueEventManager.Instance.CreateContext();
    }

    public void SetSelectingGamepads(bool set)
    {
        selecting_gamepads = set;

        gameObject.SetActive(set);
    }

    private void UpdateControllersInput()
    {
        int players_count = PlayersManager.Instance.GetPlayersCount();

        for(int i = 0; i < players_count; ++i)
        {
            Player curr_player = PlayersManager.Instance.GetPlayerByIndex(i);

            bool already_placed = false;

            if(curr_player == red_player)
            {
                already_placed = true;
            }
            else if (curr_player == yellow_player)
            {
                already_placed = true;
            }
            else if (curr_player == blue_player)
            {
                already_placed = true;
            }

            if(already_placed)
            {
                if(curr_player.GetKeyB(GamepadGetButtonType.DOWN))
                {
                    GameObject go_to_move = null;
                    GameObject place_to_move = null;

                    switch(i)
                    {
                        case 0:
                            {
                                go_to_move = controller_1;
                                place_to_move = controller_1_pos;
                                break;
                            }
                        case 1:
                            {
                                go_to_move = controller_2;
                                place_to_move = controller_2_pos;
                                break;
                            }
                        case 2:
                            {
                                go_to_move = controller_3;
                                place_to_move = controller_3_pos;
                                break;
                            }
                    }

                    queue_context.PushEventForced(new
                    QueueEventPosition(go_to_move,
                    go_to_move.gameObject.transform.position, place_to_move.gameObject.transform.position,
                    0.2f, EasingFunctionsType.EXPO_IN_OUT));

                    if (curr_player == red_player)
                    {
                        red_player = null;
                        red_ready = false;

                        red_go_ready.SetActive(false);
                    }
                    else if (curr_player == yellow_player)
                    {
                        yellow_player = null;
                        yellow_ready = false;

                        yellow_go_ready.SetActive(false);
                    }
                    else if (curr_player == blue_player)
                    {
                        blue_player = null;
                        blue_ready = false;

                        blue_go_ready.SetActive(false);
                    }
                }
                else if(curr_player.GetKeyStart(GamepadGetButtonType.DOWN))
                {
                    if (curr_player == red_player)
                    {
                        red_ready = true;

                        red_go_ready.SetActive(true);
                    }
                    else if (curr_player == yellow_player)
                    {
                        yellow_ready = true;

                        yellow_go_ready.SetActive(true);
                    }
                    else if (curr_player == blue_player)
                    {
                        blue_ready = true;

                        blue_go_ready.SetActive(true);
                    }
                }
            }
            else
            {
                GameObject go_to_move = null;
                switch (i)
                {
                    case 0:
                        {
                            go_to_move = controller_1;
                            break;
                        }
                    case 1:
                        {
                            go_to_move = controller_2;
                            break;
                        }
                    case 2:
                        {
                            go_to_move = controller_3;
                            break;
                        }
                }

                if (curr_player.DPadHorizontal() < 0)
                {
                    if (red_player == null)
                    {
                        red_player = curr_player;

                        queue_context.PushEventForced(new
                        QueueEventPosition(go_to_move,
                        go_to_move.transform.position, red_pos.gameObject.transform.position,
                        0.2f, EasingFunctionsType.EXPO_IN_OUT));
                    }
                }

                if (curr_player.DPadVertical() > 0)
                {
                    if (yellow_player == null)
                    {
                        yellow_player = curr_player;

                        queue_context.PushEventForced(new
                        QueueEventPosition(go_to_move,
                        go_to_move.transform.position, yellow_pos.gameObject.transform.position,
                        0.2f, EasingFunctionsType.EXPO_IN_OUT));
                    }
                }

                if (curr_player.DPadHorizontal() > 0)
                {
                    if (blue_player == null)
                    {
                        blue_player = curr_player;

                        queue_context.PushEventForced(new
                        QueueEventPosition(go_to_move,
                        go_to_move.transform.position, blue_pos.gameObject.transform.position,
                        0.2f, EasingFunctionsType.EXPO_IN_OUT));
                    }
                }
            }
        }
    }

    private void CheckReady()
    {
        bool ready = false;
        bool cheating = false;

        if(red_ready && blue_ready && yellow_ready)
        {
            ready = true;
        }
        else if(Input.GetKey("a"))
        {
            ready = true;
            cheating = true;
        }

        if(ready)
        {
            selecting_gamepads = false;

            queue_context.PushEventForced(new QueueEventFade(this.gameObject, 1, 0, 0.5f, EasingFunctionsType.EXPO_OUT));

            queue_context.PushEventForced(new QueueEventSetActive(this.gameObject, false));

            if (cheating)
            {
                int players_count = PlayersManager.Instance.GetPlayersCount();

                for (int i = 0; i < players_count; ++i)
                {
                    Player curr_player = PlayersManager.Instance.GetPlayerByIndex(i);

                    if (i == 0)
                    {
                        curr_player.SetPlayerColour(PlayerColour.RED);
                    }
                    else if (i == 1)
                    {
                        curr_player.SetPlayerColour(PlayerColour.BLUE);
                    }
                    else if(i == 2)
                    {
                        curr_player.SetPlayerColour(PlayerColour.YELLOW);
                    }
                }
            }
            else
            {
                if (red_player != null)
                    red_player.SetPlayerColour(PlayerColour.RED);

                if (blue_player != null)
                    blue_player.SetPlayerColour(PlayerColour.BLUE);

                if (yellow_player != null)
                    yellow_player.SetPlayerColour(PlayerColour.YELLOW);
            }

            EventMapLoad ev = new EventMapLoad();
            EventManager.Instance.SendEvent(ev);
        }
    }

    private void OnController1Stoped(QueueEvent ev)
    {
        controller_1_moving = false;
    }

    private void OnController2Stoped(QueueEvent ev)
    {
        controller_2_moving = false;
    }

    private void OnController3Stoped(QueueEvent ev)
    {
        controller_3_moving = false;
    }

    private QueueEventContext queue_context = null;

    private bool selecting_gamepads = false;

    [SerializeField]
    private GameObject red_pos = null;

    [SerializeField]
    private GameObject red_go_ready = null;

    [SerializeField]
    private GameObject yellow_pos = null;

    [SerializeField]
    private GameObject yellow_go_ready = null;

    [SerializeField]
    private GameObject blue_pos = null;

    [SerializeField]
    private GameObject blue_go_ready = null;

    [SerializeField]
    private GameObject controller_1 = null;
    private bool controller_1_moving = false;

    [SerializeField]
    private GameObject controller_1_pos = null;

    [SerializeField]
    private GameObject controller_2 = null;
    private bool controller_2_moving = false;

    [SerializeField]
    private GameObject controller_2_pos = null;

    [SerializeField]
    private GameObject controller_3 = null;
    private bool controller_3_moving = false;

    [SerializeField]
    private GameObject controller_3_pos = null;

    private Player red_player = null;
    private Player yellow_player = null;
    private Player blue_player = null;

    private bool red_ready = false;
    private bool yellow_ready = false;
    private bool blue_ready = false;
}
