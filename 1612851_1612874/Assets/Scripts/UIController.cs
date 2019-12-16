using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject[] players = new GameObject[2];
    public GameObject[] playersInfo = new GameObject[2];
    public Text[] goldsInfo = new Text[2];
    public Text[] profitsInfo = new Text[2];
    public Text[] scoresInfo = new Text[2];

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowInfo();
    }

    void ShowInfo()
    {
        for (int i = 0; i < 2; i++)
        { 
            goldsInfo[i].text = "Gold: " + players[i].GetComponent<Player>().gold.ToString();
            profitsInfo[i].text = "Profit: " + players[i].GetComponent<Player>().profit.ToString();
            scoresInfo[i].text = "Score: " + players[i].GetComponent<Player>().score.ToString();
        }
    }
}
