using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int gold, score;
    public bool haveGift;
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

    }

    void PayGold(int g)
    {
        gold -= g;
    }

    void ReciveGold(int g)
    {
        gold += g;
    }

    void CalculateScore()
    {
        int emptyFieldNumber = playerField.AddComponent<Field>().GetEmptyFieldNumber();
        score = gold - 2 * emptyFieldNumber;
        if (haveGift == true)
            score += 7;
    }
    
}
