using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //danh sách biến public (game controller)
    public GameObject[] players = new GameObject[2];
    public GameObject bushListObj; //object: danh sách các bụi cỏ 
    public bool turn; //true = lượt player 1, false = lượt player 2

    //danh sách biến chức năng đặt cỏ vào field
    GameObject currentObject, clone;
    bool isPlacing = false;
    int selectedBushID = -1;
    List<GameObject> bushSquareList = new List<GameObject>(); //danh sách các ô cỏ trong bụi

    void Start()
    {
        turn = true;
    }
    void Update()
    {
        SendGift();
        ChangeTurn(); //cos thể sẽ xoá 
        if (isPlacing == false)
        {
            ChooseBushAndClone();
        }
        else //isPlacing ==  true
        {
            MoveToMouse();
            RotateBush();
            bool temp = CheckValid();
            if (Input.GetMouseButtonDown(0))
            {
                PlaceBush();
            }
            if (Input.GetKeyDown(KeyCode.Space))
                CancelPlacing();

        }
    }

    void ChooseBushAndClone()
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
            {
                Debug.Log("not ok");
                //mau do*/
                return false;
            }
        }

        List<Transform> touchedFieldSquare = bushSquareList[0].GetComponent<BushTrigger>().collidderField;
        GameObject touchField = touchedFieldSquare[0].gameObject.transform.parent.gameObject;
        if ((turn) && (players[0].GetComponent<Player>().playerField.name == touchField.name))
        {
            Debug.Log("1 ok");
            //mau xanh
            return true;
        }
        if ((!turn) && (players[1].GetComponent<Player>().playerField.name == touchField.name))
        {
            Debug.Log("2 ok");
            //mau xanh
            return true;
        }

        Debug.Log("not ok");
        //mau do*/
        return false;
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

            int movingStep = int.Parse(clone.transform.Find("Info").Find("Move").gameObject.GetComponent<TextMesh>().text);
            MakeCharacterMove(movingStep);
            

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

    void SendGift()
    {
        if (players[0].GetComponent<Player>().playerField.GetComponent<Field>().receivedGift)
            players[0].GetComponent<Player>().haveGift = true;
        if (players[1].GetComponent<Player>().playerField.GetComponent<Field>().receivedGift)
            players[1].GetComponent<Player>().haveGift = true;
    }

    void SkipTurn()
    {
        turn = !turn;
    }

    void ChangeTurn()
    {
        //CHUA VIET DOAN NAY
        /*neu bam button thi SkipTurn()*/

        int player0CurMazePos = players[0].GetComponent<Player>().playerChar.GetComponent<Character>().currentMazeBox;
        int player1CurMazePos = players[1].GetComponent<Player>().playerChar.GetComponent<Character>().currentMazeBox;
       
        if ((turn) && (player0CurMazePos > player1CurMazePos))
        {
            turn = !turn;
        }
        else if ((!turn) && (player1CurMazePos > player0CurMazePos))
        {
            turn = !turn;
        }
    }

    void MakeCharacterMove(int step)
    {
        if (turn)
        {
            players[0].GetComponent<Player>().playerChar.GetComponent<Character>().steps = step;
        }
        if (!turn)
        {
            players[1].GetComponent<Player>().playerChar.GetComponent<Character>().steps = step;
        }
    }
}
