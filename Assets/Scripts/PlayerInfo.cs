using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public Color color = Color.white;
    void Awake()
    {
        int randomNum = Random.Range(0,7);
        switch(randomNum)
        {
            case 0:
            color = Color.black; 
            break;
            case 1:
            color = Color.blue;
            break;
            case 2:
            color = Color.cyan;
            break;
            case 3:
            color = Color.gray;
            break;
            case 4:
            color = Color.green;
            break;
            case 5:
            color = Color.magenta;
            break;
            case 6:
            color = Color.red;
            break;
            case 7:
            color = Color.yellow;
            break;
            default:
            color = Color.white;
            break;
        }
    }
    void Start()
    {

    }

}
