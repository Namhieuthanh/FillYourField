using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushTrigger : MonoBehaviour
{
    public bool canPlace = false;
    bool trigger = true;
    public List<Transform> collidderField = new List<Transform>();
   
    void Start()
    {
       
    }
    // Start is called before the first frame update
    void OnTriggerStay(Collider other)
    {
        //Debug.Log(trigger);
        if (other.CompareTag("PlacedBushSquare"))
            trigger = false;
        if (trigger == true) {
            if (other.CompareTag("Field")) 
            {
                canPlace = true;
                if (collidderField.Contains(other.transform) == false)
                    collidderField.Add(other.transform);
            } 
        }
    }
    void OnTriggerExit(Collider other)
    {
        //Debug.Log(trigger);
        if (other.CompareTag("PlacedBushSquare"))
            trigger = true;
        if (other.CompareTag("Field"))
        {
            canPlace = false;
            if (collidderField.Contains(other.transform))
                collidderField.Remove(other.transform);
        }
    }
}
