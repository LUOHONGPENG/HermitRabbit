using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : MonoBehaviour
{
    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("CameraGoTo", CameraGoToEvent);
    }

    public void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("CameraGoTo", CameraGoToEvent);
    }

    private void CameraGoToEvent(object arg0)
    {
        Vector3 pos = (Vector3)arg0;

        Debug.Log(pos);
        transform.position = new Vector3(pos.x, transform.position.y, pos.z);
    }

    private void FixedUpdate()
    {
        FixedGoMoveCamera();
        FixedGoRotateCamera();
    }

    private void FixedGoMoveCamera()
    {
        Vector2 moveInput = InputMgr.Instance.camMoveVector;
        Vector3 moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
        float moveSpeed = 5f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void FixedGoRotateCamera()
    {
        float rotateInput = InputMgr.Instance.camRotateValue;
        float rotateSpeed = 5f;
        transform.eulerAngles += new Vector3(0, rotateInput * rotateSpeed, 0);
    }
}
