using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ranking : MonoBehaviour
{
    public TextMeshProUGUI ranking_p1;
    public TextMeshProUGUI ranking_p2;
    public TextMeshProUGUI ranking_p3;

    public TextMeshProUGUI points_p1;
    public TextMeshProUGUI points_p2;
    public TextMeshProUGUI points_p3;
    // Start is called before the first frame update
    void Start()
    {
        List<House> house_ranking = HouseManager.Instance.GetHousesOrderedByPoints();

        for(int i = 0; i< house_ranking.Count; ++i) 
        {
            switch (house_ranking[i].GetPlayerInstance().GetPlayer().GetPlayerColour()) {
                case PlayerColour.RED:
                    ranking_p1.text = (i + 1).ToString();
                    points_p1.text = house_ranking[i].GetPoints().ToString("D5");
                    break;
                case PlayerColour.BLUE:
                    ranking_p3.text = (i + 1).ToString();
                    points_p2.text = house_ranking[i].GetPoints().ToString("D5");
                    break;
                case PlayerColour.YELLOW:
                    ranking_p2.text = (i + 1).ToString();
                    points_p3.text = house_ranking[i].GetPoints().ToString("D5");
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
