using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBushController : MonoBehaviour
{
    GameObject currentObject, clone;
    bool isPlacing = false;
    int selectedBushID = -1;
    public GameObject bushListObj; //object: danh sách các bụi cỏ 
    List<GameObject> bushSquareList = new List<GameObject>(); //danh sách các ô cỏ trong bụi
    public GameObject[] fields = new GameObject[2];
    
    void Update()
    {
        if (fields[0].GetComponent<Field>().receivedGift)
            Debug.Log("Chau hoa cho anh 1");
        if (fields[1].GetComponent<Field>().receivedGift)
            Debug.Log("Chua hoa cho anh 2");
        if (isPlacing == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && (hit.transform != null) && (hit.transform.gameObject.CompareTag("BushSquare")))
                {
                    currentObject = (hit.transform.parent).gameObject;
                    List<GameObject> bushListClone = bushListObj.GetComponent<Bush>().bushList; //LIST: danh sách các  bụi cỏ
                    for (int i = 0; i < 3; i++)
                    {
                        if (currentObject.name == bushListClone[i].name)
                        {
                            selectedBushID = i;

                            Vector3 clonePos = hit.point;
                            clonePos.y = 0.51f;
                            clone = GameObject.Instantiate(currentObject, clonePos, Quaternion.identity) as GameObject;

                            foreach (Transform child in clone.transform)
                            {
                                if (child.CompareTag("BushSquare") == false)
                                {
                                    child.gameObject.SetActive(false);
                                }
                                else //child: BushSquare
                                {

                                    bushSquareList.Add(child.gameObject);
                                }
                            }
                            isPlacing = true;
                        }
                    }
                }
            }
        }
        else //isPlacing ==  true
        {
            MoveToMouse();
            RotateBush();
            /*if (CheckValid())
                Debug.Log("ok");
                //mau xanh
            else
                Debug.Log("not ok");
                //mau do*/

            if (Input.GetMouseButtonDown(0))
            {
                PlaceBush();
            }
            if (Input.GetKeyDown(KeyCode.Space))
                CancelPlacing();
            
        }
    }
    void MoveToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = hit.point;
            hitPoint.y = 0.51f;
            clone.transform.position = hitPoint;
        }
    }

    void RotateBush()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            clone.transform.Rotate(new Vector3(0, -90, 0));
        if ((Input.GetKeyDown(KeyCode.RightArrow)) || (Input.GetMouseButtonDown(1)))
        {
            clone.transform.Rotate(new Vector3(0, 90, 0));
        }
    }

    void CancelPlacing()
    {
        Destroy(clone);
        bushSquareList.Clear();
        isPlacing = false;
    }

    bool CheckValid()
    {
        foreach (var bushSquare in bushSquareList)
        {
            if (bushSquare.GetComponent<BushTrigger>().canPlace == false)
                return false;
        }
        return true;
    }

    void PlaceBush()
    {
        if (CheckValid() == true)
        {
            List<Transform> touchedField = bushSquareList[0].GetComponent<BushTrigger>().collidderField;
            var minDistance = Vector3.Distance(clone.transform.position, touchedField[0].position);
            int minID = 0;
            for (int i = 1; i < touchedField.Count; i++)
            {
                if (minDistance > Vector3.Distance(clone.transform.position, touchedField[i].position))
                {
                    minDistance = Vector3.Distance(clone.transform.position, touchedField[i].position);
                    minID = i;
                }
            }
            Vector3 newPos = touchedField[minID].position;
            newPos.y = 0.55f;
            clone.transform.position = newPos;
            isPlacing = false;
            foreach (var bushSquare in bushSquareList)
            {
                bushSquare.tag = "PlacedBushSquare";
                List<Transform> placedFieldSquare = bushSquare.GetComponent<BushTrigger>().collidderField;
                placedFieldSquare[0].gameObject.tag = "PlacedField";
            }
            bushSquareList.Clear();

            bushListObj.GetComponent<Bush>().RemoveBushOnOption(selectedBushID);
        }
    }


}
