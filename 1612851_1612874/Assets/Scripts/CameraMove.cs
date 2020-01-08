using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Vector3 gameCamPos, farCamPos;
    // Start is called before the first frame update
    void Start()
    {
        gameCamPos = transform.position;
        farCamPos = gameCamPos;
        farCamPos.y = gameCamPos.y + 30;
        farCamPos.z = gameCamPos.z - 14;
        transform.position = farCamPos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, gameCamPos, Time.deltaTime * 7);
    }
}
