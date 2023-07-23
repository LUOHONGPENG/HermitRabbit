using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LevelMgr
{
    private void OnEnable()
    {
        //About Input
        EventCenter.Instance.AddEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.AddEventListener("InputMoveAction", InputMoveActionEvent);
        EventCenter.Instance.AddEventListener("InputSkillAction", InputSkillActionEvent);
        EventCenter.Instance.AddEventListener("InputSetHoverTile", InputSetHoverTileEvent);


        //About Map
        EventCenter.Instance.AddEventListener("RefreshOccupancy", RefreshOccupancyEvent);
        EventCenter.Instance.AddEventListener("RefreshSkillRange", RefreshSkillRangeEvent);

        //About Test
        EventCenter.Instance.AddEventListener("TestButton", TestButtonEvent);
    }

    private void OnDisable()
    {        
        //About Input
        EventCenter.Instance.RemoveEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.RemoveEventListener("InputMoveAction", InputMoveActionEvent);
        EventCenter.Instance.RemoveEventListener("InputSkillAction", InputSkillActionEvent);
        EventCenter.Instance.RemoveEventListener("InputSetHoverTile", InputSetHoverTileEvent);

        //About Refresh
        EventCenter.Instance.RemoveEventListener("RefreshOccupancy", RefreshOccupancyEvent);
        EventCenter.Instance.RemoveEventListener("RefreshSkillRange", RefreshSkillRangeEvent);

        //About Test
        EventCenter.Instance.AddEventListener("TestButton", TestButtonEvent);
    }



    #region EventDeal_Input
    private void InputChooseCharacterEvent(object arg0)
    {
        ChangeCurUnit(new UnitInfo(BattleUnitType.Character, (int)arg0));
        PublicTool.EventChangeInteract(InteractState.CharacterMove);
    }

    private void ChangeCurUnit(UnitInfo unitInfo)
    {
        //Set Data
        gameData.SetCurUnitInfo(unitInfo);
        //BoardCast to view
        mapViewMgr.RefreshCurUnit();
    }

    private void InputMoveActionEvent(object arg0)
    {
        Vector2Int targetPos = (Vector2Int)arg0;
        battleMgr.MoveActionRequest(targetPos);

    }

    private void InputSkillActionEvent(object arg0)
    {
        Vector2Int targetPos = (Vector2Int)arg0;
        battleMgr.SkillActionRequest(targetPos);
    }
    #endregion

    #region EventDeal_Battle

    private void TestButtonEvent(object arg0)
    {
        string info = (string)arg0;
        switch (info)
        {
            case "StartBattle":
                PublicTool.GetGameData().gamePhase = GamePhase.Battle;
                battleMgr.StartNewBattle(this);
                break;
            case "EndTurn":
                battleMgr.EndTurnPhase();
                break;
            case "GenerateFoe":
                BattleFoeData newFoeData = gameData.GenerateFoeData(1001);
                unitViewMgr.GenerateFoeView(newFoeData);
                break;
        }
    }


    #endregion

    #region EventDeal_Map
    private void RefreshOccupancyEvent(object arg0)
    {
        gameData.RefreshOccupancyInfo();
    }

    private void RefreshSkillRangeEvent(object arg0)
    {
        gameData.RefreshSkillTileInfo();
    }

    private void InputSetHoverTileEvent(object arg0)
    {
        if (gameData != null)
        {
            gameData.hoverTileID = (Vector2Int)arg0;
        }
    }
    #endregion
}
