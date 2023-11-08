using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPixelCommonItem : MonoBehaviour
{
    public CameraGridAdjustMgr cameraAdjustMgr;


    public void Init(float destoryTime)
    {
        cameraAdjustMgr.Init();

        Destroy(this.gameObject, destoryTime);
    }
}
