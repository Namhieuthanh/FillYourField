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
    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        FillBoxTransList();
        for (int i = 0; i < mazeBoxTransList.Count; i++)
        {
            Vector3 currentBoxPosition = mazeBoxTransList[i].position;
            if (i > 0)
            {
                Vector3 prevBoxPosition = mazeBoxTransList[i - 1].position;
                Gizmos.DrawLine(prevBoxPosition, currentBoxPosition);
            }
        }
    }

    void FillBoxTransList()
    {
        mazeBoxTransList.Clear();
        foreach(var mazeBox in mazeBoxList)
        {
            Transform boxTrans = mazeBox.transform;
            if (boxTrans != this.transform)
            {
                mazeBoxTransList.Add(boxTrans); 
            }
        }
    }*/
}
