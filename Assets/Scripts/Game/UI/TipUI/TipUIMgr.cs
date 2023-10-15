using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipUIMgr : MonoBehaviour
{
    public SkillNodeTipUIMgr skillNodeTipUIMgr;
    public SkillButtonTipUIMgr skillButtonTipUIMgr;

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("ShowUITip", ShowUITipEvent);
        EventCenter.Instance.AddEventListener("HideUITip", HideUITipEvent);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("ShowUITip", ShowUITipEvent);
        EventCenter.Instance.RemoveEventListener("HideUITip", HideUITipEvent);
    }
    private void ShowUITipEvent(object arg0)
    {
        UITipInfo uiTipInfo = (UITipInfo)arg0;
        switch (uiTipInfo.uiTipType)
        {
            case UITipType.SkillNode:
                skillNodeTipUIMgr.ShowTip(uiTipInfo.ID, uiTipInfo.mousePos);
                skillButtonTipUIMgr.HideTip();
                break;
            case UITipType.SkillButton:
                skillNodeTipUIMgr.HideTip();
                skillButtonTipUIMgr.ShowTip(uiTipInfo.ID, uiTipInfo.characterID, uiTipInfo.mousePos);
                break;
        }
    }

    private void HideUITipEvent(object arg0)
    {
        skillNodeTipUIMgr.HideTip();
        skillButtonTipUIMgr.HideTip();
    }
}