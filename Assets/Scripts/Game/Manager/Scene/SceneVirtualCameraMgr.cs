using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneVirtualCameraMgr : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera_Normal;
    public CinemachineVirtualCamera virtualCamera_SkillPerform;

    public void Init()
    {

    }


    public void ChangeCamera(CameraType cameraType)
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
                break;
        }
    }
}
