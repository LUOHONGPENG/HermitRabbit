using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMgr : MonoBehaviour
{
    [Header("NormalCamera")]
    public Transform tfNormalCamera;
    [Header("SkillPerformCamera")]
    public Transform tfSkillPerformLookAt;
    public Transform tfSkillPerformFollow;
    public Transform tfSkillPerformDelta;

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("NormalCameraGoTo", CameraGoToEvent);
    }

    public void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("NormalCameraGoTo", CameraGoToEvent);
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

        tfNormalCamera.DOMove(new Vector3(pos.x, tfNormalCamera.position.y, pos.z),0.5f);
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
        Vector3 moveDir = tfNormalCamera.forward * moveInput.y + tfNormalCamera.right * moveInput.x;
        float moveSpeed = 5f;
        tfNormalCamera.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void FixedGoStrictCamera()
    {
        float limit = GameGlobal.cameraLimit;

        if (tfNormalCamera.position.x < -limit)
        {
            tfNormalCamera.position = new Vector3(-limit, tfNormalCamera.position.y, tfNormalCamera.position.z);

        }
        else if (tfNormalCamera.position.x > limit)
        {
            tfNormalCamera.position = new Vector3(limit, tfNormalCamera.position.y, tfNormalCamera.position.z);
        }

        if (tfNormalCamera.position.z < -limit)
        {
            tfNormalCamera.position = new Vector3(tfNormalCamera.position.x, tfNormalCamera.position.y, -limit);
        }
        else if (tfNormalCamera.position.z > limit)
        {
            tfNormalCamera.position = new Vector3(tfNormalCamera.position.x, tfNormalCamera.position.y, limit);
        }
    }

    private void FixedGoRotateCamera()
    {
        float rotateInput = InputMgr.Instance.camRotateValue;
        float rotateSpeed = 5f;
        tfNormalCamera.eulerAngles += new Vector3(0, rotateInput * rotateSpeed, 0);
    }
}
