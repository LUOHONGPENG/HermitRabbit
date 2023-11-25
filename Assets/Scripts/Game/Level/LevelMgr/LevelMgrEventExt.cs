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
        EventCenter.Instance.AddEventListener("UnitUIRefresh", UnitUIRefreshEvent);
        EventCenter.Instance.AddEventListener("BattleFoeDead", BattleFoeDeadEvent);

        //PeacePlant
        EventCenter.Instance.AddEventListener("PeacePlantStart", PeacePlantStartEvent);
        EventCenter.Instance.AddEventListener("PeacePlantEnd", PeacePlantEndEvent);
        EventCenter.Instance.AddEventListener("InputModifyPlant", InputModifyPlantEvent);

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
        EventCenter.Instance.RemoveEventListener("UnitUIRefresh", UnitUIRefreshEvent);
        EventCenter.Instance.RemoveEventListener("BattleFoeDead", BattleFoeDeadEvent);


        //PeacePlant
        EventCenter.Instance.RemoveEventListener("PeacePlantStart", PeacePlantStartEvent);
        EventCenter.Instance.RemoveEventListener("PeacePlantEnd", PeacePlantEndEvent);
        EventCenter.Instance.RemoveEventListener("InputModifyPlant", InputModifyPlantEvent);

        //PeaceMap
        EventCenter.Instance.RemoveEventListener("PeaceMapStart", PeaceMapClipStartEvent);
        EventCenter.Instance.RemoveEventListener("PeaceMapEnd", PeaceMapClipEndEvent);
        EventCenter.Instance.RemoveEventListener("InputSetMapClip", InputSetMapClipEvent);


        //About Test
        EventCenter.Instance.RemoveEventListener("TestButton", TestButtonEvent);
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
        PublicTool.EventNormalCameraGoPosID(characterData.posID);
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

    private void InputChangeSkillEvent(object arg0)
    {
        battleMgr.BattleSkillReset();
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
            case "BackToMenu":
                GameMgr.Instance.LoadScene(SceneName.Menu);
                break;
        }
    }

    private void BattleStartEvent(object arg0)
    {
        //Generate Foe
        if (ExcelDataMgr.Instance.dayExcelData.dicDayFoe.ContainsKey(gameData.numDay))
        {
            List<Vector3Int> listFoeInfo = ExcelDataMgr.Instance.dayExcelData.dicDayFoe[gameData.numDay];

            int rowMax = GameGlobal.mapMaxNumY - 1;

            for (int i = 0; i < listFoeInfo.Count; i++)
            {
                //X=typeID Y=num Z=exp
                Vector3Int foeInfo = listFoeInfo[i];
                FoeExcelItem foeItem = PublicTool.GetFoeExcelItem(foeInfo.x);
                for (int j = 0; j < foeInfo.y; j++)
                {
                    BattleFoeData newFoeData = gameData.GenerateFoeData(foeItem.id);
                    //Set Position
                    switch (foeItem.generateType)
                    {
                        case FoeGenerateType.FixedPos:
                            newFoeData.posID = new Vector2Int(foeItem.pos0, foeItem.pos1);
                            break;
                        case FoeGenerateType.RowRange:
                            List<Vector2Int> listPos = PublicTool.GetEmptyPosFromRowRange(rowMax-foeItem.pos0 ,rowMax);
                            if (listPos.Count > 0)
                            {
                                int ran = UnityEngine.Random.Range(0, listPos.Count);
                                newFoeData.posID = listPos[ran];
                            }
                            break;
                    }
                    //Set eXP
                    newFoeData.exp = foeInfo.z;

                    unitViewMgr.GenerateFoeView(newFoeData);
                }
            }
            battleMgr.StartNewBattle(this);
        }

        //Tutorial
        if(gameData.numDay == 2)
        {
            PublicTool.StartConditionalTutorial(TutorialGroup.Skill);
        }


    }

    private void BattleEndEvent(object arg0)
    {
        //CharacterReset
        for(int i = 0; i < gameData.listCharacter.Count; i++)
        {
            BattleCharacterData characterData = gameData.listCharacter[i];
            characterData.ResetBattleEnd();
            if (unitViewMgr.GetCharacterView(characterData.keyID) != null)
            {
                unitViewMgr.GetCharacterView(characterData.keyID).MoveToPos();
            }
        }
        //PlantReset
        for(int i = 0; i < gameData.listPlant.Count; i++)
        {
            BattlePlantData plantData = gameData.listPlant[i];
            plantData.ResetBattleEnd();
        }
        //TileReset
        gameData.RefreshMapTileStatusData();
        mapViewMgr.RefreshTileView();

        PublicTool.RecalculateOccupancy();
        EventCenter.Instance.EventTrigger("UnitUIRefresh", null);
        PublicTool.EventReadyAni(-1);
    }

    private void CharacterPhaseEndEvent(object arg0)
    {
        battleMgr.EndTurnPhase();
    }

    private void UnitUIRefreshEvent(object arg0)
    {
        unitViewMgr.RefreshUnitUI();
    }

    private void BattleFoeDeadEvent(object arg0)
    {
        battleMgr.AddCharacterExp((int)arg0);
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

    private void InputModifyPlantEvent(object arg0)
    {
        Vector2Int posPlant = (Vector2Int)arg0;
        peaceMgr.ModifyPlant(posPlant);
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
