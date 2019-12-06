using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var targetPosition = GetComponent<Character>().MoveTarget();
        MoveForwardTarget(targetPosition);
        Debug.Log("tar" + targetPosition);
    }

    void MoveForwardTarget(Vector3 targetPos)
    {
        CharacterController characterController = GetComponent<CharacterController>();
        if ((targetPos - transform.position).magnitude > .1f)
        {
            characterController.Move((targetPos - transform.position).normalized * Time.deltaTime * 5);
        }
    }
}
