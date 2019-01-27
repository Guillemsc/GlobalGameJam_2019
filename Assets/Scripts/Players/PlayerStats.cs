using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    public GameObject red;
    public GameObject yellow;
    public GameObject blue;

    public GameObject particles;

    private GameObject animator = null;


    void Start() {
        red.SetActive(false);
        yellow.SetActive(false);
        blue.SetActive(false);

        switch (player.GetPlayerColour()) {
            case PlayerColour.RED:
                animator = red;
                break;
            case PlayerColour.BLUE:
                animator = yellow;
                break;
            case PlayerColour.YELLOW:
                animator = blue;
                break;
            default:
                break;
        }
        animator.SetActive(true);
        particles.transform.SetParent(animator.transform);
    }

    public void SetPlayer(Player pl)
    {
        player = pl;

        if (player != null)
            gameObject.name = "Player: " + player.GetPlayerIndex();
    }

    public Player GetPlayer()
    {
        return player;
    }

    public float GetJoystickDeadVal()
    {
        return joystick_dead_val;
    }

    public void SetGrabbedItem(Item item)
    {
        grabbed_item = item;
    }

    public Item GetGrabbedItem()
    {
        return grabbed_item;
    }

    public bool GetHasGrabbedItem()
    {
        return grabbed_item != null;
    }

    public GameObject GetAnimator() 
    {
        return animator;
    }

    [SerializeField]
    private float joystick_dead_val = 0.0f;

    private Player player = null;

    private Item grabbed_item = null;
}
