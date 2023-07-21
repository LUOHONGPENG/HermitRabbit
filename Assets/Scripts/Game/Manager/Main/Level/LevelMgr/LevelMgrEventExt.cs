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

        //About Battle
        EventCenter.Instance.AddEventListener("TestStartBattle", TestStartBattleEvent);
        EventCenter.Instance.AddEventListener("TestEndTurn", TestEndTurnEvent);
        EventCenter.Instance.AddEventListener("TestGenerateFoe", TestGenerateFoeEvent);

        //About Map
        EventCenter.Instance.AddEventListener("RefreshOccupancy", RefreshOccupancyEvent);
        EventCenter.Instance.AddEventListener("RefreshSkillRange", RefreshSkillRangeEvent);
        EventCenter.Instance.AddEventListener("SetHoverTile", SetHoverTileEvent);
    }

    private void OnDisable()
    {        
        //About Input
        EventCenter.Instance.RemoveEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.RemoveEventListener("InputMoveAction", InputMoveActionEvent);
        EventCenter.Instance.RemoveEventListener("InputSkillAction", InputSkillActionEvent);

        //About Battle
        EventCenter.Instance.RemoveEventListener("TestStartBattle", TestStartBattleEvent);
        EventCenter.Instance.RemoveEventListener("TestEndTurn", TestEndTurnEvent);
        EventCenter.Instance.RemoveEventListener("TestGenerateFoe", TestGenerateFoeEvent);

        //About Refresh
        EventCenter.Instance.RemoveEventListener("RefreshOccupancy", RefreshOccupancyEvent);
        EventCenter.Instance.RemoveEventListener("RefreshSkillRange", RefreshSkillRangeEvent);
        
        //About Hover Tile
        EventCenter.Instance.RemoveEventListener("SetHoverTile", SetHoverTileEvent);
    }

    #region EventDeal_Input
    private void InputChooseCharacterEvent(object arg0)
    {
        ChangeCurUnit(new UnitInfo(BattleUnitType.Character, (int)arg0));
        PublicTool.EventChangeInteract(InteractState.Move);
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
        unitViewMgr.InvokeAction_SelfMove(targetPos);
    }

    private void InputSkillActionEvent(object arg0)
    {
        Vector2Int targetPos = (Vector2Int)arg0;
        battleMgr.SkillActionRequest(targetPos);
    }
    #endregion

    #region EventDeal_Battle
    private void TestStartBattleEvent(object arg0)
    {
        PublicTool.GetGameData().gamePhase = GamePhase.Battle;
        battleMgr.StartNewBattle(this);
    }

    private void TestEndTurnEvent(object arg0)
    {
        battleMgr.EndTurnPhase();
    }

    private void TestGenerateFoeEvent(object arg0)
    {
        BattleFoeData newFoeData = gameData.GenerateFoeData(1001);
        unitViewMgr.GenerateFoeView(newFoeData);
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

    private void SetHoverTileEvent(object arg0)
    {
        if (gameData != null)
        {
            gameData.hoverTileID = (Vector2Int)arg0;
        }
    }
    #endregion
}
