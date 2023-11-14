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

    private CameraType lastCameraType = CameraType.NormalCamera;

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("NormalCameraGoTo", NormalCameraGoToEvent);
        EventCenter.Instance.AddEventListener("ChangeCamera", ChangeCameraEvent);
    }

    public void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("NormalCameraGoTo", NormalCameraGoToEvent);
        EventCenter.Instance.RemoveEventListener("ChangeCamera", ChangeCameraEvent);
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

    #region NormalCamera

    private void NormalCameraGoToEvent(object arg0)
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

    #endregion

    #region SkillPerformCamera
    private void ChangeCameraEvent(object arg0)
    {
        ChangeCameraInfo info = (ChangeCameraInfo)arg0;
        if(info.cameraType == CameraType.SkillPerformCamera)
        {
            if(lastCameraType == CameraType.SkillPerformCamera)
            {
                SetSkillPerformCamera(info,true);
            }
            else
            {
                SetSkillPerformCamera(info, false);
            }
        }
        lastCameraType = info.cameraType;
    }

    public void SetSkillPerformCamera(ChangeCameraInfo info,bool isFromSkill)
    {
        Vector3 tempPosSubject = PublicTool.ConvertPosFromID(info.posSubject);
        Vector3 tempPosTarget = PublicTool.ConvertPosFromID(info.posTarget);
        Vector3 tempPosTargetExtra = PublicTool.ConvertPosFromID(info.posExtraTarget);
        Vector3 tempPosMiddle = (tempPosSubject + tempPosTarget) / 2;
        Vector3 direction = tempPosTarget - tempPosSubject;
        float distance = direction.magnitude;

        //Define
        Vector3 posFollow = tempPosSubject;
        Vector3 posLookAt = tempPosTarget;
        Quaternion rotate = Quaternion.Euler(Vector3.zero);
        Vector3 tempDelta = Vector3.zero;
        bool needLookAt = false;

        //FollowLookAt
        switch (info.posType)
        {
            case CameraPosType.CharacterLeftLow:
            case CameraPosType.CharacterLeftHigh:
            case CameraPosType.MiddleLeft:
                needLookAt = true;
                //posLookAt = tempPosMiddle;
                break;
            case CameraPosType.FocusSubject:
                posFollow = GetNormalCameraLimitPos(tempPosSubject);
                posLookAt = GetNormalCameraLimitPos(tempPosSubject);
                rotate = tfNormalCamera.rotation;
                break;
            case CameraPosType.FocusTarget:
                posFollow = GetNormalCameraLimitPos(tempPosTarget);
                posLookAt = GetNormalCameraLimitPos(tempPosTarget);
                rotate = tfNormalCamera.rotation;
                break;
            case CameraPosType.FocusTargetExtra:
                posFollow = GetNormalCameraLimitPos(tempPosTargetExtra);
                posLookAt = GetNormalCameraLimitPos(tempPosTargetExtra);
                rotate = tfNormalCamera.rotation;
                break;
        }


        //Delta
        switch (info.posType)
        {
            case CameraPosType.CharacterLeftLow:
                tempDelta = new Vector3(-2f, -1f, -0.3f * distance);
                break;
            case CameraPosType.CharacterLeftHigh:
                tempDelta = new Vector3(-2f, 1f, -0.2f * distance);
                break;
            case CameraPosType.MiddleLeft:
                tempDelta = new Vector3(-3f, -1, 0.5f * distance);
                break;
            case CameraPosType.FocusSubject:
            case CameraPosType.FocusTarget:
            case CameraPosType.FocusTargetExtra:
                break;
        }

        if (isFromSkill)
        {
            tfSkillPerformFollow.DOMove(posFollow, 0.5f);
            tfSkillPerformLookAt.DOMove(posLookAt, 0.5f);
            if(tfSkillPerformFollow.rotation != rotate)
            {
                tfSkillPerformFollow.DORotateQuaternion(rotate, 0.5f);
            }
            if (needLookAt)
            {
                tfSkillPerformFollow.LookAt(posLookAt);
            }
            tfSkillPerformDelta.DOLocalMove(tempDelta, 0.5f);
        }
        else
        {
            tfSkillPerformFollow.position = posFollow;
            tfSkillPerformLookAt.position = posLookAt;
            tfSkillPerformFollow.rotation = rotate;
            if (needLookAt)
            {
                tfSkillPerformFollow.LookAt(posLookAt);
            }
            tfSkillPerformDelta.localPosition = tempDelta;
        }

        
    }

    public Vector3 GetNormalCameraLimitPos(Vector3 pos)
    {
        if (Mathf.Abs(pos.x) > GameGlobal.cameraLimit)
        {
            pos.x = GameGlobal.cameraLimit * pos.x / Mathf.Abs(pos.x);
        }

        if (Mathf.Abs(pos.z) > GameGlobal.cameraLimit)
        {
            pos.z = GameGlobal.cameraLimit * pos.z / Mathf.Abs(pos.z);
        }

        return pos;
    }

    #endregion
}
