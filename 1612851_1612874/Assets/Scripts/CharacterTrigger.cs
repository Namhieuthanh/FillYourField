using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public bool appleTrigger, squareTrigger;
    public GameObject squareGift;
    void Start()
    {
        appleTrigger = false;
        squareTrigger = false;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Apple"))
        {
            appleTrigger = true;
        }
        else if (other.CompareTag("BushSquare"))
        {
            squareTrigger = true;
            squareGift = other.gameObject;

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Apple"))
        {
            appleTrigger = false;
        }
        else if (other.CompareTag("BushSquare"))
        {
            squareTrigger = false;
        }
    }
}
