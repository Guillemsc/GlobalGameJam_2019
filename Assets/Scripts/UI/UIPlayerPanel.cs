using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<UIPlayer>().SetPlayer(PlayersManager.Instance.GetPlayerByColour(PlayerColour.RED).GetPlayerInstance());
        transform.GetChild(1).GetComponent<UIPlayer>().SetPlayer(PlayersManager.Instance.GetPlayerByColour(PlayerColour.YELLOW).GetPlayerInstance());
        transform.GetChild(2).GetComponent<UIPlayer>().SetPlayer(PlayersManager.Instance.GetPlayerByColour(PlayerColour.BLUE).GetPlayerInstance());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
