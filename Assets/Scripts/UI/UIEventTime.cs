using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIEventTime : MonoBehaviour
{
    TMPro.TextMeshProUGUI time = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTime(int time) {
        this.time.text = time.ToString("D2");
    }
}
