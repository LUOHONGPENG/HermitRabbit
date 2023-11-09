using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TipUIMgr : MonoBehaviour
{
    public SkillNodeTipUIMgr skillNodeTipUIMgr;
    public SkillButtonTipUIMgr skillButtonTipUIMgr;
    public FoeInfoTipUIMgr foeInfoTipUIMgr;

    private bool isInit = false;
    private GameData gameData;

    public void Init()
    {
        gameData = PublicTool.GetGameData();
        isInit = true;
    }


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
                skillButtonTipUIMgr.ShowTip(uiTipInfo.ID, uiTipInfo.unitID, uiTipInfo.mousePos);
                break;
        }
    }

    private void HideUITipEvent(object arg0)
    {
        skillNodeTipUIMgr.HideTip();
        skillButtonTipUIMgr.HideTip();
    }


    private void Update()
    {
        if (isInit)
        {
            //DisplayFoeTip
            if(gameData.GetUnitInfoFromHoverTileID().type == BattleUnitType.Foe)
            {
                foeInfoTipUIMgr.ShowTip(gameData.GetUnitInfoFromHoverTileID().keyID);
            }
            else
            {
                foeInfoTipUIMgr.HideTip();
            }
        }
    }


}
