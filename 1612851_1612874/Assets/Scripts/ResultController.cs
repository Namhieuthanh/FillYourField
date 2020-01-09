using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour
{
    public GameObject resultScreen, player1WinImg, player2WinImg;
    public GameObject[] players;
    public Text[] goldsInfo = new Text[2];
    public Text[] unplacedFieldInfo = new Text[2];
    public Text[] rewardInfo = new Text[2];
    public Text[] scoresInfo = new Text[2];
    // Start is called before the first frame update
    void Start()
    {
        resultScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("co up");
        CheckWin();
        ShowStat();
    }

    void CheckWin()
    {
        if ((players[0].GetComponent<Player>().playerChar.GetComponent<Character>().stop == true) && (players[1].GetComponent<Player>().playerChar.GetComponent<Character>().stop == true))
        {
           
            if (players[0].GetComponent<Player>().score > players[1].GetComponent<Player>().score)
            {
                player1WinImg.SetActive(true);
                player2WinImg.SetActive(false);
            }
            else
            {
                player2WinImg.SetActive(true);
                player1WinImg.SetActive(false);
            }
        }
    }

    void ShowStat()
    {
        for (int i = 0; i < 2; i++)
        {
            goldsInfo[i].text = players[i].GetComponent<Player>().gold.ToString();
            unplacedFieldInfo[i].text = players[i].GetComponent<Player>().unplacedFieldNumber.ToString();
            if (players[i].GetComponent<Player>().haveGift == true)
            {
                rewardInfo[i].text = "Yes";
            }
            else
            {
                rewardInfo[i].text = "No";
            }
            scoresInfo[i].text = players[i].GetComponent<Player>().score.ToString();
        }
    }
}
