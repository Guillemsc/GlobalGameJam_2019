using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++) 
        {
            GameObject child = transform.GetChild(i).gameObject;
            //child.GetComponent<UIPlayer>().SetPlayer(PlayersManager.Instance.GetPlayerByIndex(i)); TODO
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
