using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public GameObject[] mazeBoxList;
    //public List<Transform> mazeBoxTransList = new List<Transform>();
   
    void Start()
    {
        mazeBoxList = GameObject.FindGameObjectsWithTag("MazeBox");
        for (int i = 0; i < mazeBoxList.Length; i++)
        {
              TextMesh boxText = mazeBoxList[i].GetComponentInChildren<TextMesh>();
              boxText.text = (i + 1).ToString();
            
        }
    }
   
}
