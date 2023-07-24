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

        EventCenter.Instance.AddEventListener("BattleStart", BattleStartEvent);
        EventCenter.Instance.AddEventListener("CharacterPhaseEnd", CharacterPhaseEndEvent);

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

        EventCenter.Instance.RemoveEventListener("BattleStart", BattleStartEvent);
        EventCenter.Instance.RemoveEventListener("CharacterPhaseEnd", CharacterPhaseEndEvent);

        //About Test
        EventCenter.Instance.AddEventListener("TestButton", TestButtonEvent);
    }



    #region EventDeal_Input
    private void InputChooseCharacterEvent(object arg0)
    {
        BattleCharacterData characterData = (BattleCharacterData)PublicTool.GetGameData().GetDataFromUnitInfo(new UnitInfo(BattleUnitType.Character, (int)arg0));
        //Set Data
        gameData.SetCurUnitInfo(new UnitInfo(BattleUnitType.Character, characterData.keyID));
        //BoardCast to view
        mapViewMgr.RefreshCurUnit();
        //Event
        PublicTool.EventChangeInteract(InteractState.CharacterMove);
        PublicTool.EventCameraGoPosID(characterData.posID);
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
                EventCenter.Instance.EventTrigger("BattleStart", null);
                break;
            case "GenerateFoe":
                BattleFoeData newFoeData = gameData.GenerateFoeData(1001);
                unitViewMgr.GenerateFoeView(newFoeData);
                break;
        }
    }

    private void BattleStartEvent(object arg0)
    {
        PublicTool.GetGameData().gamePhase = GamePhase.Battle;
        battleMgr.StartNewBattle(this);
    }

    private void CharacterPhaseEndEvent(object arg0)
    {
        battleMgr.EndTurnPhase();
    }
    #endregion

    #region EventDeal_Map

    private void InputSetHoverTileEvent(object arg0)
    {
        if (gameData != null)
        {
            gameData.hoverTileID = (Vector2Int)arg0;
        }
    }
    #endregion
}
