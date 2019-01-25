using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayerScore : MonoBehaviour
{
    private int score = 0;
    private int target_score = 0;
    private TMPro.TextMeshProUGUI text;
    private bool text_updated = false;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() 
    {
        if (score < target_score) 
        {
            score++;
            text_updated = true;
        }
        else if (score > target_score) 
        {
            score--;
            text_updated = true;
        }

        if(text_updated) 
        {
            text.text = score.ToString("D5");
            text_updated = false;
        }
    }

    public void AddScore(int amount) 
    {
        target_score += amount;
    }

    public void SubstractScore(int amount) 
    {
        target_score -= amount;
    }
}
