using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject mazeCam, fieldCam, miniMapCam, minimapImage;
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
        }
        else
        {
            mazeCam.SetActive(true);
            fieldCam.SetActive(false);
            miniMapCam.SetActive(true);
            minimapImage.SetActive(true);
        }
    }
}
