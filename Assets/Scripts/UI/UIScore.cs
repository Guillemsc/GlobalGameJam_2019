using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScore : MonoBehaviour
{

    public TMPro.TextMeshProUGUI score = null;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
