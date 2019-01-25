using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    Vector2 end_position;
    Vector2 start_position;

    float distance = 0.0f;

    private void Update()
    {
        //float new_x = EasingFunctions.Linear(end_position.x - start_position.x, //Timer Partida, start_position.x, 300)
        //float new_y = EasingFunctions.Linear(end_position.y - start_position.y, //Timer Partida, start_position.y, 300)
        //transform.position = new Vector2(new_x, new_y);
    }

    public void SetEndPosition(Vector2 _end_position)
    {
        start_position = transform.position;
        end_position = _end_position;
    }
}
