using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerConnected : MonoBehaviour
{
    RectTransform[] controller = new RectTransform[3];
    Player[] players = new Player[3];
    bool controller0_connected = false;


    // Start is called before the first frame update
    void Start()
    {
        GameObject[] controllers = GameObject.FindGameObjectsWithTag("Controller");
        int i = 0;
        foreach (GameObject c in controllers)
        {
            players[i] = PlayersManager.Instance.GetPlayerByIndex(i);
            controller[i] = c.GetComponent<RectTransform>();
            i++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))  controller[0].position = new Vector2(0, controller[0].position.y + 125);
        //if (Input.GetKeyDown(KeyCode.W))  controller[0].position = new Vector2(0, controller[0].position.y - 125);

    }
}
