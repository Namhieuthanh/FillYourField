using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //danh sách biến public (game controller)
    public GameObject[] players = new GameObject[2];
    public GameObject bushListObj; //object: danh sách các bụi cỏ 
    public bool turn; //true = lượt player 1, false = lượt player 2
    public GameObject camController;
    public Material redMat, greenMat, greyMat;
    public Button skipButton;
    public GameObject[] playerTurnMark = new GameObject[2];
    public AudioSource bushColAudio, turnAudio;
    public GameObject defaultCanvas, resultScreen, pauseCanvas;

    //danh sách biến chức năng đặt cỏ vào field
    GameObject currentObject, clone;
    bool isPlacing = false;
    bool isGift = false;
    int selectedBushID = -1;
    List<GameObject> bushSquareList = new List<GameObject>(); //danh sách các ô cỏ trong bụi

    void Start()
    {
        turn = true;
        pauseCanvas.SetActive(false);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseCanvas.SetActive(true);
        }
        if (turn)
        {
            playerTurnMark[0].SetActive(true);
            playerTurnMark[1].SetActive(false);
        }
        else
        {
            playerTurnMark[0].SetActive(false);
            playerTurnMark[1].SetActive(true);
        }
        SendGift();
        StartCoroutine(CheckBushSquareGift());
        if (isGift == true)
            PutBushSquareGift();
        else
        {
            if (CheckChangeTurn() == true)
            {
                ChangeTurn();
            }

            ChangeColorToGrey();

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

        CheckWin();
    }

    void ChooseBushAndClone()
    {
        if (camController.GetComponent<CameraController>().fieldCam.activeInHierarchy == true)
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
                            //so sánh tiền người chơi và giá mua cỏ
                            int selectedBushPrice = int.Parse(currentObject.transform.Find("Info").Find("Price").gameObject.GetComponent<TextMesh>().text);
                            int playerGold = 0;
                            if (turn)
                            {
                                playerGold = players[0].GetComponent<Player>().gold;
                            }
                            else
                            {
                                playerGold = players[1].GetComponent<Player>().gold;
                            }

                            if (selectedBushPrice <= playerGold)
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
        //nếu không đặt bụi cỏ vào được field nào
        foreach (var bushSquare in bushSquareList)
        {
            if (bushSquare.GetComponent<BushTrigger>().canPlace == false)
            {
                ChangeColorToRed();
                return false;
            }
        }

        //xét xem có đặt đúng field của mình không
        List<Transform> touchedFieldSquare = bushSquareList[0].GetComponent<BushTrigger>().collidderField;
        GameObject touchField = touchedFieldSquare[0].gameObject.transform.parent.gameObject;
        if ((turn) && (players[0].GetComponent<Player>().playerField.name == touchField.name))
        {
            ChangeColorToGreen();

            return true;
        }
        if ((!turn) && (players[1].GetComponent<Player>().playerField.name == touchField.name))
        {
            ChangeColorToGreen();
            return true;
        }

        ChangeColorToRed();
        return false;
    }

    void PlaceBush()
    {
        if (CheckValid() == true)
        {
            //tìm ô sân gần nhất trong các ô hợp lệ để đặt cỏ vào
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
                        
            //đổi tag cỏ và khoảng sân (đánh dấu là không còn sử dụng được nữa)
            foreach (var bushSquare in bushSquareList)
            {
                bushSquare.tag = "PlacedBushSquare";
                List<Transform> placedFieldSquare = bushSquare.GetComponent<BushTrigger>().collidderField;
                placedFieldSquare[0].gameObject.tag = "PlacedField";
            }
            bushSquareList.Clear();

            //thanh toán tiền mua mảnh cỏ
            Payment(int.Parse(clone.transform.Find("Info").Find("Price").gameObject.GetComponent<TextMesh>().text));

            //tích luỹ lợi nhuận
            StoreFieldProfit(int.Parse(clone.transform.Find("Profit").Find("Fruit").gameObject.GetComponent<TextMesh>().text));

            //xoá thảm cỏ trên băng chuyền 
            bushListObj.GetComponent<Bush>().RemoveBushOnOption(selectedBushID);


            //chuyển cam
            camController.GetComponent<CameraController>().SwitchCam();

            //điều khiển nhân vật di chuyển đúng số bước ghi trên thảm cỏ
            int movingStep = int.Parse(clone.transform.Find("Info").Find("Move").gameObject.GetComponent<TextMesh>().text);
            MakeCharacterMove(movingStep);
        }
    }

    void SendGift()
    {
        if (players[0].GetComponent<Player>().playerField.GetComponent<Field>().receivedGift)
        {
            players[0].GetComponent<Player>().haveGift = true;
            players[0].GetComponent<Player>().CalculateScore();
        }
        if (players[1].GetComponent<Player>().playerField.GetComponent<Field>().receivedGift)
        {
            players[1].GetComponent<Player>().haveGift = true;
            players[1].GetComponent<Player>().CalculateScore();
        }
    }

    void MakeCharacterMove(int step)
    {
        if (turn)
        {
            players[0].GetComponent<Player>().playerChar.GetComponent<Character>().SetCurrentBox(step);
        }
        if (!turn)
        {
            players[1].GetComponent<Player>().playerChar.GetComponent<Character>().SetCurrentBox(step);
        }
    }

    void ChangeColorToRed()
    {
        foreach (var bushSquare in bushSquareList)
        {
            bushSquare.GetComponent<MeshRenderer>().material = redMat;
        }
    }

    void ChangeColorToGreen()
    {
        foreach (var bushSquare in bushSquareList)
        {
            bushSquare.GetComponent<MeshRenderer>().material = greenMat;
        }
    }

    void ChangeColorToGrey()
    {
        for (int i = 3; i < 6; i++)
        {
            List<GameObject> squareList = new List<GameObject>();
            foreach (Transform child in (bushListObj.GetComponent<Bush>().bushList)[i].transform)
            {
                if (child.CompareTag("BushSquare") == true)
                {                  
                    squareList.Add(child.gameObject);
                }
            }
            foreach(var square in squareList)
            {
                square.GetComponent<MeshRenderer>().material = greyMat;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            List<GameObject> squareList = new List<GameObject>();
            foreach (Transform child in (bushListObj.GetComponent<Bush>().bushList)[i].transform)
            {
                if (child.CompareTag("BushSquare") == true)
                {
                    squareList.Add(child.gameObject);
                }
            }

            int bushPrice = int.Parse((bushListObj.GetComponent<Bush>().bushList)[i].transform.Find("Info").Find("Price").gameObject.GetComponent<TextMesh>().text);
            int playerGold = 0;
            if (turn)
            {
                playerGold = players[0].GetComponent<Player>().gold;
            }
            else
            {
                playerGold = players[1].GetComponent<Player>().gold;
            }

            if (playerGold >= bushPrice)
            {
                foreach (var square in squareList)
                {
                    square.GetComponent<MeshRenderer>().material = greenMat;
                }
            }
            else
            {
                foreach (var square in squareList)
                {
                    square.GetComponent<MeshRenderer>().material = greyMat;
                }
            }
        }
    }

    void ChangeTurn()
    {
        turn = !turn;
        turnAudio.Play();
    }

    bool CheckChangeTurn()
    {
        int player0CurMazePos = players[0].GetComponent<Player>().playerChar.GetComponent<Character>().currentMazeBox;
        int player1CurMazePos = players[1].GetComponent<Player>().playerChar.GetComponent<Character>().currentMazeBox;

        if ((turn) && (player0CurMazePos > player1CurMazePos))
        {
            return true;
        }
        else if ((!turn) && (player1CurMazePos > player0CurMazePos))
        {
            return true;
        }

        return false;
    }

    public void OnSkipButtonClick()
    {
        if (turn)
        {
            int movingStep = players[1].GetComponent<Player>().playerChar.GetComponent<Character>().currentMazeBox - players[0].GetComponent<Player>().playerChar.GetComponent<Character>().currentMazeBox;
            MakeCharacterMove(movingStep + 1);
            players[0].GetComponent<Player>().ReciveGold(movingStep + 1);
            ChangeTurn();
        }
        else if (!turn)
        {
            int movingStep = players[0].GetComponent<Player>().playerChar.GetComponent<Character>().currentMazeBox - players[1].GetComponent<Player>().playerChar.GetComponent<Character>().currentMazeBox;
            MakeCharacterMove(movingStep + 1);
            players[1].GetComponent<Player>().ReciveGold(movingStep + 1);
            ChangeTurn();
        }
    }

    void Payment(int gold)
    {
        if (turn)
        {
            players[0].GetComponent<Player>().PayGold(gold);
            //players[0].GetComponent<Player>().CalculateScore();
        }
        else
        {
            players[1].GetComponent<Player>().PayGold(gold);
            //players[1].GetComponent<Player>().CalculateScore();
        }
    }

    void StoreFieldProfit(int pro)
    {
        if (turn)
        {
            players[0].GetComponent<Player>().StoreProfit(pro);
        }
        else
        {
            players[1].GetComponent<Player>().StoreProfit(pro);
        }
    }

    IEnumerator CheckBushSquareGift()
    {
        for (int playerID = 0; playerID < 2; playerID++)
        {
            
            if (players[playerID].GetComponent<Player>().playerChar.GetComponent<CharacterTrigger>().squareTrigger == true)
            {
                bushColAudio.Play();
                isGift = true;
                players[playerID].GetComponent<Player>().playerChar.GetComponent<CharacterTrigger>().squareTrigger = false;

                GameObject bushGift = players[playerID].GetComponent<Player>().playerChar.GetComponent<CharacterTrigger>().squareGift;
                if (Camera.main == null)
                {
                    camController.GetComponent<CameraController>().SwitchCam();
                }

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if ((isPlacing == false) && (Physics.Raycast(ray, out hit)))
                {
                    Vector3 clonePos = hit.point;
                    clonePos.y = 0.51f;
                    clone = GameObject.Instantiate(bushGift, clonePos, Quaternion.Euler(90, 0, 0)) as GameObject;
                    Destroy(bushGift);
                    bushSquareList.Add(clone);
                    isPlacing = true;

                }
                ChangeTurn();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    void PutBushSquareGift()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if ((isPlacing == true) && (Physics.Raycast(ray, out hit)))
        {
            Vector3 hitPoint = hit.point;
            hitPoint.y = 0.51f;
            clone.transform.position = hitPoint;
            bool temp = CheckValid();
            if (Input.GetMouseButtonDown(0))
            {
                if (CheckValid() == true)
                {
                    //tìm ô sân gần nhất trong các ô hợp lệ để đặt cỏ vào
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


                    //đổi tag khoảng sân (đánh dấu là không còn sử dụng được nữa)
                    clone.tag = "PlacedBushSquare";
                    List<Transform> placedFieldSquare = clone.GetComponent<BushTrigger>().collidderField;
                    placedFieldSquare[0].gameObject.tag = "PlacedField";
                    bushSquareList.Clear();
                    ChangeTurn();
                    camController.GetComponent<CameraController>().SwitchCam();
                    isGift = false;
                }

            }
        }

    }

    void CheckWin()
    {
        if ((players[0].GetComponent<Player>().playerChar.GetComponent<Character>().stop == true) && (players[1].GetComponent<Player>().playerChar.GetComponent<Character>().stop == true))
        {
            defaultCanvas.SetActive(false);
            resultScreen.SetActive(true);
        }
    }

}
