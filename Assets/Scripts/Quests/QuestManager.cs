using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Tooltip("When each quest starts in seconds")]
    public float[] quest_time = new float[0];

    public List<Quest> quests = new List<Quest>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
