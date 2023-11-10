using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        if (Mathf.Abs(pos.x) > GameGlobal.cameraLimit)
        {
            pos.x = GameGlobal.cameraLimit * pos.x / Mathf.Abs(pos.x);
        }

        if (Mathf.Abs(pos.z) > GameGlobal.cameraLimit)
        {
            pos.z = GameGlobal.cameraLimit * pos.z / Mathf.Abs(pos.z);
        }

        transform.DOMove(new Vector3(pos.x, transform.position.y, pos.z),0.5f);
    }

    private void FixedUpdate()
    {
        InteractState state = InputMgr.Instance.GetInteractState();

        switch (state)
        {
            case InteractState.BattleNormal:
            case InteractState.CharacterMove:
            case InteractState.CharacterSkill:
            case InteractState.PeaceNormal:
            case InteractState.PeacePlant:
            case InteractState.PeaceMap:
                if (!GameMgr.Instance.GetWhetherPageOn())
                {
                    FixedGoMoveCamera();
                    FixedGoRotateCamera();
                }
                break;
        }
        FixedGoStrictCamera();
    }

    private void FixedGoMoveCamera()
    {
        Vector2 moveInput = InputMgr.Instance.camMoveVector;
        Vector3 moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
        float moveSpeed = 5f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void FixedGoStrictCamera()
    {
        float limit = GameGlobal.cameraLimit;

        if (transform.position.x < -limit)
        {
            transform.position = new Vector3(-limit, transform.position.y, transform.position.z);

        }
        else if (transform.position.x > limit)
        {
            transform.position = new Vector3(limit, transform.position.y, transform.position.z);
        }

        if (transform.position.z < -limit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -limit);

        }
        else if (transform.position.z > limit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, limit);
        }
    }

    private void FixedGoRotateCamera()
    {
        float rotateInput = InputMgr.Instance.camRotateValue;
        float rotateSpeed = 5f;
        transform.eulerAngles += new Vector3(0, rotateInput * rotateSpeed, 0);
    }
}
