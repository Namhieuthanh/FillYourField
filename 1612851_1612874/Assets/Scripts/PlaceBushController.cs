using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBushController : MonoBehaviour
{
    GameObject currentObject;
    bool isPlacing = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPlacing = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if ((hit.transform != null) && (hit.transform.gameObject.CompareTag("BushSquare")))
                {
                    currentObject = (hit.transform.parent).gameObject;
                    currentObject.transform.eulerAngles = new Vector3(0, 90, 0);
                    Debug.Log(currentObject);
                }
            }
            else
            {
                if (isPlacing == false)
                    isPlacing = false;
            }
        }
        if (isPlacing)
            MoveToMouse();

    }
    void MoveToMouse()
    {
        /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            currentObject.transform.position = hit.point;
            
            Debug.Log("after" + currentObject);
        }*/
    }


}
