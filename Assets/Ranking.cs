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

        EventManager.Instance.Suscribe(GameEventType.EVENT_MATCH_FINISH, OnEvent);
        EventManager.Instance.Suscribe(GameEventType.EVENT_MAP_LOAD, OnEvent);
        transform.parent.gameObject.SetActive(false);
        
    }

    public void OnEvent(GameEvent ev) 
    {
        switch (ev.Type()) {
            case GameEventType.EVENT_NULL:
                break;
            case GameEventType.EVENT_PLAYER_ADDED:
                break;
            case GameEventType.EVENT_PLAYER_REMOVED:
                break;
            case GameEventType.EVENT_PLAYER_SPAWNED:
                break;
            case GameEventType.EVENT_PLAYER_DESPAWNED:
                break;
            case GameEventType.EVENT_GAMEPADS_CHANGED:
                break;
            case GameEventType.EVENT_GAMEPAD_ADDED:
                break;
            case GameEventType.EVENT_GAMEPAD_REMOVED:
                break;
            case GameEventType.EVENT_SET_SCORE:
                break;
            case GameEventType.EVENT_SUBSTRACT_SCORE:
                break;
            case GameEventType.EVENT_START_QUEST:
                break;
            case GameEventType.EVENT_END_QUEST:
                break;
            case GameEventType.EVENT_HOUSES_SPAWNED:
                break;
            case GameEventType.EVENT_MAP_LOAD:
                transform.parent.gameObject.SetActive(false);
                break;
            case GameEventType.EVENT_MAP_UNLOAD:
                break;
            case GameEventType.EVENT_MATCH_START:
                break;
            case GameEventType.EVENT_MATCH_FINISH: 
                {
                    transform.parent.gameObject.SetActive(true);

                    List<House> house_ranking = HouseManager.Instance.GetHousesOrderedByPoints();

                    for (int i = 0; i < house_ranking.Count; ++i) {
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

                    break;
                }
            case GameEventType.EVENT_ITEM_GRABBED:
                break;
            case GameEventType.EVENT_ITEM_DROPPED:
                break;
            case GameEventType.EVENT_ITEM_ENTERS_HOUSE:
                break;
            case GameEventType.EVENT_ITEM_LEAVES_HOUSE:
                break;
            case GameEventType.EVENT_BULLET_HITS_PLAYER:
                break;
            default:
                break;
        }
    }

}
