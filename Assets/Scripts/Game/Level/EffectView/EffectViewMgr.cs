using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectViewMgr : MonoBehaviour
{
    public Transform tfEffect;

    private bool isInit = false;

    public void Init()
    {
        isInit = true;
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("EffectViewGenerate", EffectViewGenerateEvent);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("EffectViewGenerate", EffectViewGenerateEvent);
    }

    private void EffectViewGenerateEvent(object arg0)
    {
        EffectViewInfo info = (EffectViewInfo)arg0;

        Vector3 tilePos = PublicTool.ConvertPosFromID(info.tarPos);
        tilePos = new Vector3(tilePos.x, GameGlobal.commonUnitPosY, tilePos.z);

        string strUrl = "PixelEffect/";

        switch (info.type)
        {
            case EffectViewType.FireBall:
                strUrl += "efPixelFireBall";
                break;
        }

        GameObject objEffect = Resources.Load(strUrl) as GameObject;
        objEffect = GameObject.Instantiate(objEffect, tfEffect);
        objEffect.transform.position = tilePos;
        EffectPixelCommonItem itemEffect = objEffect.GetComponent<EffectPixelCommonItem>();
        itemEffect.Init();
    }


}
