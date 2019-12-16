using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Field : MonoBehaviour
{
    public List<List<GameObject>> fieldSquareList = new List<List<GameObject>>(); //[col][row]
    public List<List<bool>> placedFieldSquareList = new List<List<bool>>();
    public bool receivedGift = false;
   
    void Start()
    {
        List<GameObject> getFieldSquare = new List<GameObject>();
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.CompareTag("Field"))
                getFieldSquare.Add(child);
        }
        
        getFieldSquare.OrderBy(sort => sort.transform.GetSiblingIndex()).ToArray();

        for (int col = 0; col < 9; col++)
        {
            List<GameObject> fieldRow = new List<GameObject>();
            for (int row = 0; row < 9; row++)
            {
                fieldRow.Add(getFieldSquare[9 * col + row]);
            }
            fieldSquareList.Add(fieldRow);
        }

        for (int col = 0; col < 9; col++)
        {
            List<bool> checkFieldRow = new List<bool>(); //kiem tra placedField 
            for (int row = 0; row < 9; row++)
            {
                checkFieldRow.Add(false);
            }
            placedFieldSquareList.Add(checkFieldRow);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetPlacedField();
        if ((checkGiftCondition() && receivedGift == false))
        {
            receivedGift = true;
        }
      
    }

    public void GetPlacedField()
    {
        for (int col = 0; col < 9; col++)
        {
            for (int row = 0; row < 9; row++)
            {
                if (fieldSquareList[col][row].CompareTag("PlacedField"))
                {
                    placedFieldSquareList[col][row] = true;
                }
            }
        }
    }

    public int GetPlacedFieldNumber()
    {
        int res = 0;
        for (int col = 0; col < 9; col++)
        {
            for (int row = 0; row < 9; row++)
            {
                if (fieldSquareList[col][row].CompareTag("PlacedField"))
                {
                    res++;
                }
            }
        }
        return res;
    }

    public bool checkGiftCondition() //kiem tra 7x7
    {
        for (int col = 0; col < 3; col++)
        {
            for (int row = 0; row < 3; row++)
            {
                if (check7x7(col, row) == true)
                    return true;
            }
        }
        return false;
    }

    bool check7x7(int col, int row)
    {
        for (int j = 0; j < 7; j++)
        {
            for (int i = 0; i < 7; i++)
            {
                if (placedFieldSquareList[col+j][row+i] == false)
                {
                    return false;
                }
            }
        }

        return true;
    }

    
}
