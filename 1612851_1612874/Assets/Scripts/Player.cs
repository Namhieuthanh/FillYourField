using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int gold, score, profit;
    public bool haveGift;
    public AudioSource coinColAudio;
    public GameObject playerChar, playerField;
    // Start is called before the first frame update
    void Start()
    {
        gold = 5;
        score = 0;
        haveGift = false;
    }

    // Update is called once per frame
    void Update()
    {
        AppleTrigger();
        CalculateScore();
    }

    public void PayGold(int g)
    {
        gold -= g;
    }

    public void ReciveGold(int g)
    {
        gold += g;
    }

    public void CalculateScore()
    {
        int emptyFieldNumber = playerField.GetComponent<Field>().GetUnplacedFieldNumber();
        score = 162 + gold - 2 * emptyFieldNumber;
        if (haveGift == true)
            score += 7;
    }

    public void StoreProfit(int pro)
    {
        profit += pro;
    }

    public void AppleTrigger()
    {
        if (playerChar.GetComponent<CharacterTrigger>().appleTrigger)
        {
            coinColAudio.Play();
            ReciveGold(profit);
            //CalculateScore();
        }
        playerChar.GetComponent<CharacterTrigger>().appleTrigger = false;
    }
}
