using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Maze thisMaze;
    //public int steps;
    public int currentMazeBox;
    public int movingBox;
    bool isMoving = false;
    public bool stop = false;
   
    // Start is called before the first frame update
    void Start()
    {
        currentMazeBox = 0;
        movingBox = 0;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MoveTarget());
    }

    public void SetCurrentBox(int steps)
    {
        if (currentMazeBox + steps < thisMaze.mazeBoxList.Length)
        {
            currentMazeBox += steps;
            if (currentMazeBox == thisMaze.mazeBoxList.Length - 1)
                stop = true;
        }
        else
        {
            currentMazeBox = thisMaze.mazeBoxList.Length - 1;
            stop = true;
        }
        
    }
    IEnumerator MoveTarget()
    {
        if (isMoving)
            yield break;
        isMoving = true;
       
        while ((movingBox < currentMazeBox))
        {       
            movingBox++;
        
            if (((movingBox >= 8 - 1) && (movingBox <= 14 - 1)) || ((movingBox >= 32 - 1) && (movingBox <= 36 - 1)) || ((movingBox >= 48 - 1) && (movingBox <= 50 - 1)))
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
            }
            else if (((movingBox >= 15 - 1) && (movingBox <= 20 - 1)) || ((movingBox >= 37 - 1) && (movingBox <= 40 - 1)) || ((movingBox >= 51 - 1) && (movingBox <= 54 - 1)))
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (((movingBox >= 21 - 1) && (movingBox <= 26 - 1)) || ((movingBox >= 41 - 1) && (movingBox <= 44 - 1)))
            {
                transform.eulerAngles = new Vector3(0, 270, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            Vector3 currentPosition = transform.position;
            Vector3 targetPos = thisMaze.mazeBoxList[movingBox].transform.position;
            targetPos.y = currentPosition.y;
           
            while ((targetPos - transform.position).magnitude > .1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, 5 * Time.deltaTime);             
                yield return null;
            }
            yield return new WaitForSeconds(.1f);
        }

        isMoving = false;
    }

}
