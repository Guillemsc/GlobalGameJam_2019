using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPlayGameButton : MonoBehaviour
{
    [SerializeField]
    GameObject controller_menu;


    public void GoToControllerMenu()
    {
        GetComponent<AudioSource>().Play();
        controller_menu.SetActive(true);
        gameObject.SetActive(false);
    }

    

}
