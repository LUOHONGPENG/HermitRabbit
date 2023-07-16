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

        //About Battle
        EventCenter.Instance.AddEventListener("StartBattle", StartBattleEvent);
        EventCenter.Instance.AddEventListener("EndTurn", EndTurnEvent);

        //About Map
        EventCenter.Instance.AddEventListener("RefreshTileInfo", RefreshTileInfoEvent);
        EventCenter.Instance.AddEventListener("SetHoverTile", SetHoverTileEvent);
    }



    private void OnDisable()
    {        
        //About Input
        EventCenter.Instance.RemoveEventListener("ChangeInteract", ChangeInteractEvent);
        EventCenter.Instance.RemoveEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.RemoveEventListener("InputMoveAction", InputMoveActionEvent);

        //About Battle
        EventCenter.Instance.RemoveEventListener("StartBattle", StartBattleEvent);
        EventCenter.Instance.RemoveEventListener("EndTurn", EndTurnEvent);
        
        //About Map
        EventCenter.Instance.RemoveEventListener("RefreshTileInfo", RefreshTileInfoEvent);
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
    #endregion

    #region EventDeal_Map



    private void RefreshTileInfoEvent(object arg0)
    {
        gameData.RefreshTileInfo();
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
