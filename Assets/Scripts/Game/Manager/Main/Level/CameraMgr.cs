using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : MonoBehaviour
{
    private void Update()
    {
        Vector2 moveInput = InputMgr.Instance.moveCamVector;

        Vector3 moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;

        //Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y);

        float moveSpeed = 5f;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
