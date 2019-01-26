using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIEventText : MonoBehaviour
{
    TMPro.TextMeshProUGUI text = null;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text) 
    {
        this.text.text = text;
    }
}
