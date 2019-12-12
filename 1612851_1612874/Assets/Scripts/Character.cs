using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Maze thisMaze;
    public int steps;
    public int currentMazeBox;
    // Start is called before the first frame update
    void Start()
    {
        currentMazeBox = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 MoveTarget()
    {
        Vector3 currentPosition = transform.position;
               
        if (currentMazeBox + steps < thisMaze.mazeBoxList.Length)
        {
            currentMazeBox += steps;
        }
        else
        {
            currentMazeBox = thisMaze.mazeBoxList.Length - 1;
        }

        if (((currentMazeBox >= 8-1) && (currentMazeBox <= 14-1)) || ((currentMazeBox >= 32-1)&&(currentMazeBox <= 36-1)) || ((currentMazeBox >= 48-1) && (currentMazeBox <= 50-1)))
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else if (((currentMazeBox >= 15-1) && (currentMazeBox <= 20-1)) || ((currentMazeBox >= 37-1) && (currentMazeBox <= 40-1)) || ((currentMazeBox >= 51-1) && (currentMazeBox <= 54-1)))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (((currentMazeBox >= 21-1) && (currentMazeBox <= 26-1)) || ((currentMazeBox >= 41-1) && (currentMazeBox <= 44-1))) { 
            transform.eulerAngles = new Vector3(0, 270, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        Vector3 newPosition = thisMaze.mazeBoxList[currentMazeBox].transform.position;
        newPosition.y = currentPosition.y;
        steps = 0;
        return newPosition;
    }
}
