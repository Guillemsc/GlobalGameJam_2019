using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuUI : Singleton<MainMenuUI>
{
    MainMenuUI()
    {
        InitInstance(this);
    }

    private void Awake()
    {
        InitQueueEvent();

        ControllerSelectionUI.Instance.SetSelectingGamepads(false);
    }

    private void Start()
    {
        FadeIn();
    }

    private void InitQueueEvent()
    {
        queue_context = QueueEventManager.Instance.CreateContext();
    }

    public void GoToControllerMenu()
    {
        FadeOutToMainMenu();
    }

    public void GoToCredits()
    {

    }

    private void FadeIn()
    {
        queue_context.PushEvent(new QueueEventSetActive(play_button, false));

        queue_context.PushEvent(new
        QueueEventScale(play_button,
        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0.1f, EasingFunctionsType.EXPO_IN), true);

        queue_context.PushEvent(new QueueEventSetActive(credits_button, false), true);

        queue_context.PushEvent(new
        QueueEventScale(credits_button,
        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0.1f, EasingFunctionsType.EXPO_IN), true);

        queue_context.PushEvent(new QueueEventWaitTime(0.2f));

        queue_context.PushEvent(new QueueEventSetActive(play_button, true));
        queue_context.PushEvent(new QueueEventSetActive(credits_button, true), true);

        queue_context.PushEvent(new
            QueueEventScale(play_button,
            new Vector3(0, 0, 0), new Vector3(2, 2, 2), 0.7f, EasingFunctionsType.BOUNCE));

        queue_context.PushEvent(new
            QueueEventScale(credits_button,
             new Vector3(0, 0, 0), new Vector3(2, 2, 2), 1.7f, EasingFunctionsType.BOUNCE), true);
    }

    private void FadeOutToMainMenu()
    {
        queue_context.PushEvent(new
        QueueEventScale(play_button,
        new Vector3(2, 2, 2), new Vector3(0, 0, 0), 0.6f, EasingFunctionsType.QUAD_OUT));

        queue_context.PushEvent(new
        QueueEventScale(credits_button,
        new Vector3(2, 2, 2), new Vector3(0, 0, 0), 0.3f, EasingFunctionsType.QUAD_OUT), true);

        queue_context.PushEvent(new QueueEventSetActive(gameObject, false));
        queue_context.LastPushedEventOnFinish(OnSelectingGamepadsLoad);
    }

    private void OnSelectingGamepadsLoad(QueueEvent ev)
    {
        ControllerSelectionUI.Instance.SetSelectingGamepads(true);
    }

    [SerializeField]
    private GameObject controller_menu = null;

    [SerializeField]
    private GameObject play_button = null;

    [SerializeField]
    private GameObject credits_button = null;

    private QueueEventContext queue_context = null;
}
