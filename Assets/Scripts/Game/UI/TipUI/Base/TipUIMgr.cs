using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TipUIMgr : MonoBehaviour
{
    [Header("ButtonTip")]
    public SkillNodeTipUIMgr skillNodeTipUIMgr;
    public SkillButtonTipUIMgr skillButtonTipUIMgr;
    public PlantPreviewTipUIMgr plantPreviewTipUIMgr;
    public BuffIconTipUIMgr buffIconTipUIMgr;

    [Header("TileInfoTip")]
    public TileInfoTipUIMgr tileInfoTipUIMgr;

    [Header("UnitInfoTip")]
    public CharacterInfoTipUIMgr characterInfoTipUIMgr;
    public FoeInfoTipUIMgr foeInfoTipUIMgr;
    public PlantInfoTipUIMgr plantInfoTipUIMgr;

    private bool isInit = false;
    private GameData gameData;

    private bool shouldHideTile = false;

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
                plantPreviewTipUIMgr.HideTip();
                buffIconTipUIMgr.HideTip();
                shouldHideTile = true;
                break;
            case UITipType.SkillButton:
                skillNodeTipUIMgr.HideTip();
                skillButtonTipUIMgr.ShowTip(uiTipInfo.ID, uiTipInfo.mousePos);
                plantPreviewTipUIMgr.HideTip();
                buffIconTipUIMgr.HideTip();
                shouldHideTile = true;
                break;
            case UITipType.PlantPreview:
                skillNodeTipUIMgr.HideTip();
                skillButtonTipUIMgr.HideTip();
                plantPreviewTipUIMgr.ShowTip(uiTipInfo.ID,uiTipInfo.mousePos);
                buffIconTipUIMgr.HideTip();
                shouldHideTile = true;
                break;
            case UITipType.BuffIcon:
                skillNodeTipUIMgr.HideTip();
                skillButtonTipUIMgr.HideTip();
                plantPreviewTipUIMgr.HideTip();
                buffIconTipUIMgr.ShowTip(uiTipInfo.ID, uiTipInfo.mousePos);
                shouldHideTile = true;
                break;
            case UITipType.TileIcon:
                skillNodeTipUIMgr.HideTip();
                skillButtonTipUIMgr.HideTip();
                plantPreviewTipUIMgr.HideTip();
                buffIconTipUIMgr.HideTip();
                tileInfoTipUIMgr.UpdateBasicInfo((MapTileType)uiTipInfo.ID);
                shouldHideTile = false;
                break;
        }
    }

    private void HideUITipEvent(object arg0)
    {
        skillNodeTipUIMgr.HideTip();
        skillButtonTipUIMgr.HideTip();
        plantPreviewTipUIMgr.HideTip();
        buffIconTipUIMgr.HideTip();
        shouldHideTile = true;
    }


    private void Update()
    {
        if (isInit)
        {
            //DisplayTileTip
            if (shouldHideTile)
            {
                if (gameData.CheckWhetherHoverTileValid() && InputMgr.Instance.interactState != InteractState.WaitAction)
                {
                    tileInfoTipUIMgr.UpdateBasicInfo(gameData.GetMapTileDataFromHoverTile());
                }
                else
                {
                    tileInfoTipUIMgr.HideTip();
                }
            }
            else
            {
                
            }


            if(InputMgr.Instance.interactState != InteractState.WaitAction)
            {
                //DisplayUnitTip
                if (gameData.GetUnitInfoFromHoverTileID().type == BattleUnitType.Character)
                {
                    characterInfoTipUIMgr.UpdateBasicInfo(gameData.GetUnitInfoFromHoverTileID());
                    foeInfoTipUIMgr.HideTip();
                    plantInfoTipUIMgr.HideTip();
                }
                else if (gameData.GetUnitInfoFromHoverTileID().type == BattleUnitType.Foe)
                {
                    characterInfoTipUIMgr.HideTip();
                    foeInfoTipUIMgr.UpdateBasicInfo(gameData.GetUnitInfoFromHoverTileID());
                    plantInfoTipUIMgr.HideTip();
                }
                else if (gameData.GetUnitInfoFromHoverTileID().type == BattleUnitType.Plant)
                {
                    characterInfoTipUIMgr.HideTip();
                    foeInfoTipUIMgr.HideTip();
                    plantInfoTipUIMgr.UpdateBasicInfo(gameData.GetUnitInfoFromHoverTileID());
                }
                else
                {
                    characterInfoTipUIMgr.HideTip();
                    foeInfoTipUIMgr.HideTip();
                    plantInfoTipUIMgr.HideTip();
                }
            }
            else
            {
                characterInfoTipUIMgr.HideTip();
                foeInfoTipUIMgr.HideTip();
                plantInfoTipUIMgr.HideTip();
            }

        }
    }


}
