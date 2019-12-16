using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Maze : MonoBehaviour
{
    public GameObject[] mazeBoxList;
    public GameObject applePrefab, bushSquarePrefab;
    
    //public List<Transform> mazeBoxTransList = new List<Transform>();
   
    void Start()
    {
        mazeBoxList = GameObject.FindGameObjectsWithTag("MazeBox").OrderBy(sort=>sort.transform.GetSiblingIndex()).ToArray();
        
        for (int i = 0; i < mazeBoxList.Length; i++)
        {
              TextMesh boxText = mazeBoxList[i].GetComponentInChildren<TextMesh>();
              boxText.text = (i + 1).ToString();
            
        }

        //sinh ra táo
        int mazeIDHaveApple = 5;
        while (mazeIDHaveApple < 54)
        {
            Vector3 apllePos = mazeBoxList[mazeIDHaveApple].transform.position;
            apllePos.y = 0.5f;
            Instantiate(applePrefab, apllePos, Quaternion.identity);
            mazeIDHaveApple += 6;
        }

        //sinh ra ô cỏ tặng thêm
        int[] mazeIDHaveBushSquare = { 21, 27, 33, 45, 51 };
        foreach (int id in mazeIDHaveBushSquare)
        {
            Vector3 bushSquarePos = mazeBoxList[id].transform.position;
            bushSquarePos.y = 0.6f;
            Instantiate(bushSquarePrefab, bushSquarePos, Quaternion.Euler(90,0,0));
        }
       
    }
   
}
