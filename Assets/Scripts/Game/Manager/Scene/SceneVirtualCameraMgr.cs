using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneVirtualCameraMgr : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera_Normal;
    public CinemachineVirtualCamera virtualCamera_SkillPerform;

    public void Init()
    {
        ChangeCamera(CameraType.NormalCamera,CameraPosType.None);
    }


    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("ChangeCamera", ChangeCameraEvent);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("ChangeCamera", ChangeCameraEvent);
    }

    private void ChangeCameraEvent(object arg0)
    {
        ChangeCameraInfo info = (ChangeCameraInfo)arg0;
        ChangeCamera(info.cameraType,info.posType);
    }

    public void ChangeCamera(CameraType cameraType,CameraPosType posType)
    {
        virtualCamera_Normal.Priority = 0;
        virtualCamera_SkillPerform.Priority = 0;
        switch (cameraType)
        {
            case CameraType.NormalCamera:
                virtualCamera_Normal.Priority = 10;
                break;
            case CameraType.SkillPerformCamera:
                virtualCamera_SkillPerform.Priority = 10;

                switch (posType)
                {
                    case CameraPosType.CharacterLeftHigh:
                    case CameraPosType.CharacterLeftLow:
                    case CameraPosType.MiddleLeft:
                        virtualCamera_SkillPerform.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 3.5f, 0);
                        break;
                    case CameraPosType.FocusSubject:
                    case CameraPosType.FocusTarget:
                    case CameraPosType.FocusTargetExtra:
                        virtualCamera_SkillPerform.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 3.5f, -4);
                        break;
                }
                break;
        }
    }
}
