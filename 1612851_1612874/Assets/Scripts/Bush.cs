using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    public List<GameObject> bushList;
    public GameObject[] selectAnchor;
    public GameObject[] nextAnchor;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] getBush = GameObject.FindGameObjectsWithTag("Bush");
        for (int i = 0; i < getBush.Length; i++)
        {
            bushList.Add(getBush[i]);
        }
        RandomBushList();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.DownArrow)) {
            
            RemoveBushOnOption(1);
         }*/
        
        for (int i = 0; i < selectAnchor.Length; i++)
        {
            if (i < bushList.Count)
                bushList[i].transform.position = selectAnchor[i].transform.position;
        }
        for (int i = 0; i < nextAnchor.Length; i++)
        {
            if (i + selectAnchor.Length < bushList.Count)
                bushList[i + selectAnchor.Length].transform.position = nextAnchor[i].transform.position;
        }
    }
    
    void RandomBushList()
    {
        System.Random _random = new System.Random();

        GameObject tempObject;

        for (int i = 0; i < bushList.Count; i++)
        {
            int newIndicator = i + (int)(_random.NextDouble() * (bushList.Count - i));
            tempObject = bushList[newIndicator];
            bushList[newIndicator] = bushList[i];
            bushList[i] = tempObject;
        }

    }

    public void RemoveBushOnOption(int bushID){
        List<GameObject> temp = new List<GameObject>();
        
        for (int i = 0; i < bushID; i++)
        {
            temp.Add(bushList[i]);
            bushList[i].transform.position = this.transform.position; //dua bush tro ve bushList = remove khoi optionList
            bushList.Remove(bushList[i]);
        }
        bushList[0].transform.position = this.transform.position; //dua bush tro ve bushList = remove khoi optionList
        bushList.Remove(bushList[0]); //remove bushID

        bushList.AddRange(temp);
    }

 
}
