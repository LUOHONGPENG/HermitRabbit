using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LevelMgr
{
    private void OnEnable()
    {
        //Input
        EventCenter.Instance.AddEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.AddEventListener("InputMoveAction", InputMoveActionEvent);
        EventCenter.Instance.AddEventListener("InputSkillAction", InputSkillActionEvent);
        EventCenter.Instance.AddEventListener("InputChangeSkill", InputChangeSkillEvent);
        EventCenter.Instance.AddEventListener("InputSetHoverTile", InputSetHoverTileEvent);

        //Battle
        EventCenter.Instance.AddEventListener("BattleStart", BattleStartEvent);
        EventCenter.Instance.AddEventListener("BattleEnd", BattleEndEvent);
        EventCenter.Instance.AddEventListener("CharacterPhaseEnd", CharacterPhaseEndEvent);

        //PeacePlant
        EventCenter.Instance.AddEventListener("PeacePlantStart", PeacePlantStartEvent);
        EventCenter.Instance.AddEventListener("PeacePlantEnd", PeacePlantEndEvent);
        EventCenter.Instance.AddEventListener("InputAddPlant", InputAddPlantEvent);

        //PeaceMap
        EventCenter.Instance.AddEventListener("PeaceMapStart", PeaceMapClipStartEvent);
        EventCenter.Instance.AddEventListener("PeaceMapEnd", PeaceMapClipEndEvent);
        EventCenter.Instance.AddEventListener("InputSetMapClip", InputSetMapClipEvent);

        //About Test
        EventCenter.Instance.AddEventListener("TestButton", TestButtonEvent);
    }

    private void OnDisable()
    {        
        //Input
        EventCenter.Instance.RemoveEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.RemoveEventListener("InputMoveAction", InputMoveActionEvent);
        EventCenter.Instance.RemoveEventListener("InputSkillAction", InputSkillActionEvent);
        EventCenter.Instance.RemoveEventListener("InputChangeSkill", InputChangeSkillEvent);
        EventCenter.Instance.RemoveEventListener("InputSetHoverTile", InputSetHoverTileEvent);

        //Battle
        EventCenter.Instance.RemoveEventListener("BattleStart", BattleStartEvent);
        EventCenter.Instance.RemoveEventListener("BattleEnd", BattleEndEvent);
        EventCenter.Instance.RemoveEventListener("CharacterPhaseEnd", CharacterPhaseEndEvent);

        //PeacePlant
        EventCenter.Instance.RemoveEventListener("PeacePlantStart", PeacePlantStartEvent);
        EventCenter.Instance.RemoveEventListener("PeacePlantEnd", PeacePlantEndEvent);
        EventCenter.Instance.RemoveEventListener("InputAddPlant", InputAddPlantEvent);

        //PeaceMap
        EventCenter.Instance.RemoveEventListener("PeaceMapStart", PeaceMapClipStartEvent);
        EventCenter.Instance.RemoveEventListener("PeaceMapEnd", PeaceMapClipEndEvent);
        EventCenter.Instance.RemoveEventListener("InputSetMapClip", InputSetMapClipEvent);


        //About Test
        EventCenter.Instance.RemoveEventListener("TestButton", TestButtonEvent);
    }

    private void InputChangeSkillEvent(object arg0)
    {
        battleMgr.BattleSkillReset();
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

    private void InputSetHoverTileEvent(object arg0)
    {
        if (gameData != null)
        {
            gameData.hoverTileID = (Vector2Int)arg0;
        }
    }
    #endregion

    #region EventDeal_Battle

    private void TestButtonEvent(object arg0)
    {
        string info = (string)arg0;
        switch (info)
        {
            case "AddExp":
                gameData.AddCharacterExp(1001, 50);
                gameData.AddCharacterExp(1002, 50);
                break;
            case "GenerateFoe":
                BattleFoeData newFoeData = gameData.GenerateFoeData(1001);
                unitViewMgr.GenerateFoeView(newFoeData);
                break;
            case "RandomMap":
                for(int i = 0; i < mapViewMgr.listMapTile.Count; i++)
                {
                    mapViewMgr.listMapTile[i].TestRandomSetTileType();
                }
                break;
        }
    }

    private void BattleStartEvent(object arg0)
    {
        battleMgr.StartNewBattle(this);
    }

    private void BattleEndEvent(object arg0)
    {

    }

    private void CharacterPhaseEndEvent(object arg0)
    {
        battleMgr.EndTurnPhase();
    }
    #endregion

    #region EventDeal_PeacePlant
    private void PeacePlantStartEvent(object arg0)
    {
        peaceMgr.StartPlantMode();
    }

    private void PeacePlantEndEvent(object arg0)
    {
        peaceMgr.EndPlantMode();
    }

    private void InputAddPlantEvent(object arg0)
    {
        Vector2Int posPlant = (Vector2Int)arg0;
        peaceMgr.AddPlant(posPlant);
    }
    #endregion

    #region EventDeal_PeaceMap

    private void PeaceMapClipStartEvent(object arg0)
    {
        peaceMgr.StartMapClipMode();
    }
    private void PeaceMapClipEndEvent(object arg0)
    {
        peaceMgr.EndMapClipMode();
    }

    private void InputSetMapClipEvent(object arg0)
    {
        Vector2Int posMapClip = (Vector2Int)arg0;
        peaceMgr.SetMapClip(posMapClip);
    }

    #endregion
}
