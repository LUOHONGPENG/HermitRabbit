using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LevelMgr
{
    private void OnEnable()
    {
        //About Input
        EventCenter.Instance.AddEventListener("ChangeInteract", ChangeInteractEvent);
        EventCenter.Instance.AddEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.AddEventListener("InputMoveAction", InputMoveActionEvent);
        EventCenter.Instance.AddEventListener("InputSkillAction", InputSkillActionEvent);

        //About Battle
        EventCenter.Instance.AddEventListener("StartBattle", StartBattleEvent);
        EventCenter.Instance.AddEventListener("EndTurn", EndTurnEvent);
        EventCenter.Instance.AddEventListener("TestGenerateFoe", TestGenerateFoeEvent);

        //About Map
        EventCenter.Instance.AddEventListener("RefreshOccupancy", RefreshOccupancyEvent);
        EventCenter.Instance.AddEventListener("SetHoverTile", SetHoverTileEvent);
    }



    private void OnDisable()
    {        
        //About Input
        EventCenter.Instance.RemoveEventListener("ChangeInteract", ChangeInteractEvent);
        EventCenter.Instance.RemoveEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.RemoveEventListener("InputMoveAction", InputMoveActionEvent);
        EventCenter.Instance.RemoveEventListener("InputSkillAction", InputSkillActionEvent);

        //About Battle
        EventCenter.Instance.RemoveEventListener("StartBattle", StartBattleEvent);
        EventCenter.Instance.RemoveEventListener("EndTurn", EndTurnEvent);
        EventCenter.Instance.RemoveEventListener("TestGenerateFoe", TestGenerateFoeEvent);

        //About Map
        EventCenter.Instance.RemoveEventListener("RefreshOccupancy", RefreshOccupancyEvent);
        EventCenter.Instance.RemoveEventListener("SetHoverTile", SetHoverTileEvent);
    }



    #region EventDeal_Input
    private void ChangeInteractEvent(object arg0)
    {
        InteractInfo info = (InteractInfo)arg0;
        InputMgr.Instance.SetInteractState(info.state);
        switch (info.state)
        {
            case InteractState.Skill:
                gameData.SetCurBattleSkillID(info.data_0);
                gameData.RefreshSkillTileInfo();
                break;
        }
    }

    private void InputChooseCharacterEvent(object arg0)
    {
        ChangeCurUnit(new UnitInfo(BattleUnitType.Character, (int)arg0));
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

        SkillMapInfo skillMapInfo = gameData.GetCurSkillMapInfo();
        BattleUnitData skillMaster = gameData.GetCurUnitData();
        if (!skillMaster.listValidSkill.Contains(targetPos))
        {
            Debug.Log("No target");
            return;
        }

        if (skillMaster.curSP <= 0)
        {
            Debug.Log("SP not enough");
            return;
        }

        //Spell Skill
        skillMaster.curSP--;
        List<Vector2Int> listPos = new List<Vector2Int>();
        switch (skillMapInfo.regionType)
        {
            case SkillRegionType.Circle:
                listPos = PublicTool.GetTargetCircleRange(targetPos, skillMapInfo.radius);
                break;
        }

        foreach (var pos in listPos)
        {
            if (gameData.dicTempMapUnit.ContainsKey(pos))
            {
                if (gameData.dicTempMapUnit[pos].type == BattleUnitType.Foe && skillMapInfo.isTargetFoe)
                {
                    BattleFoeData foeData = (BattleFoeData)gameData.GetDataFromUnitInfo(gameData.dicTempMapUnit[pos]);
                    foeData.GetHurt(100);
                    if (foeData.isDead)
                    {
                        unitViewMgr.RemoveFoeView(foeData.keyID);
                    }
                }
            }
        }
    }
    #endregion

    #region EventDeal_Battle
    private void StartBattleEvent(object arg0)
    {
        PublicTool.GetGameData().gamePhase = GamePhase.Battle;
        battleMgr.StartNewBattle(this);
    }

    private void EndTurnEvent(object arg0)
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

    private void SetHoverTileEvent(object arg0)
    {
        if (gameData != null)
        {
            gameData.hoverTileID = (Vector2Int)arg0;
        }
    }
    #endregion
}
