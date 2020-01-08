using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject mazeCam, fieldCam, miniMapCam, minimapImage, minimapBorder;
    // Start is called before the first frame update
    void Start()
    {
        mazeCam.SetActive(true);
        fieldCam.SetActive(false);
        miniMapCam.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
            SwitchCam();
    }
    public void SwitchCam()
    {
        if (mazeCam.activeInHierarchy == true)
        {
            mazeCam.SetActive(false);
            fieldCam.SetActive(true);
            miniMapCam.SetActive(false);
            minimapImage.SetActive(false);
            minimapBorder.SetActive(false);
        }
        else
        {
            mazeCam.SetActive(true);
            fieldCam.SetActive(false);
            miniMapCam.SetActive(true);
            minimapImage.SetActive(true);
            minimapBorder.SetActive(true);
        }
    }
}
