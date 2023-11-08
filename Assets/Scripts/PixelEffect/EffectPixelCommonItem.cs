using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPixelCommonItem : MonoBehaviour
{
    public CameraGridAdjustMgr cameraAdjustMgr;

    private float destoryTime = 1f;


    public void Init()
    {
        cameraAdjustMgr.Init();

        Destroy(this.gameObject, destoryTime);
    }
}
