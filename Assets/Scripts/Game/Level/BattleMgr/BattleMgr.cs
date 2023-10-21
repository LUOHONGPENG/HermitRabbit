using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlling the flow of the battle
/// </summary>
public partial class BattleMgr : MonoSingleton<BattleMgr>
{
    public int numTurn;
    public BattlePhase battleTurnPhase;

    private MapViewMgr mapViewMgr;
    private UnitViewMgr unitViewMgr;
    private GameData gameData;

    private bool isBattleEnd = true;

    #region Basic Function

    public void Init(LevelMgr levelMgr)
    {
        this.mapViewMgr = levelMgr.mapViewMgr;
        this.unitViewMgr = levelMgr.unitViewMgr;
        this.gameData = PublicTool.GetGameData();
    }

    public void StartNewBattle(LevelMgr parent)
    {
        numTurn = 1;
        ResetNewTurn();
        battleTurnPhase = BattlePhase.CharacterPhase;
        StartTurnPhase();
        isBattleEnd = false;
    }

    public void StartTurnPhase()
    {
        GeneratePlantTriggerDic();
        PublicTool.RecalculateOccupancy();
        switch (battleTurnPhase)
        {
            case BattlePhase.CharacterPhase:
                StartCharacterPhase();
                break;
            case BattlePhase.FoePhase:
                StartFoePhase();
                break;
        }
        Debug.Log("Start Turn " + numTurn + " " + battleTurnPhase.ToString());
    }

    public void EndTurnPhase()
    {
        switch (battleTurnPhase)
        {
            case BattlePhase.CharacterPhase:
                battleTurnPhase = BattlePhase.FoePhase;
                break;
            case BattlePhase.FoePhase:
                battleTurnPhase = BattlePhase.CharacterPhase;
                numTurn++;
                ResetNewTurn();
                break;
        }
        StartTurnPhase();
    }

    private void ResetNewTurn()
    {
        List<BattleCharacterData> listCharacter = gameData.listCharacter;
        for(int i = 0; i < listCharacter.Count; i++)
        {
            listCharacter[i].ResetNewTurn();
        }

        List<BattlePlantData> listPlant = gameData.listPlant;
        for (int i = 0; i < listPlant.Count; i++)
        {
            listPlant[i].ResetNewTurn();
        }

        List<BattleFoeData> listFoe = gameData.listFoe;
        for (int i = 0; i < listFoe.Count; i++)
        {
            listFoe[i].ResetNewTurn();
        }

        PublicTool.EventRefreshCharacterUI();
    }

    private void StartCharacterPhase()
    {
        EventCenter.Instance.EventTrigger("CharacterPhaseStart", null);
        PublicTool.EventChangeInteract(InteractState.BattleNormal);
        //Auto Click
        List<BattleCharacterData> listCharacter = gameData.listCharacter;
        for(int i = 0; i < listCharacter.Count; i++)
        {
            if (!listCharacter[i].isDead)
            {
                EventCenter.Instance.EventTrigger("InputChooseCharacter", listCharacter[i].keyID);
            }
        }
    }

    private void StartFoePhase()
    {
        PublicTool.EventChangeInteract(InteractState.WaitAction);
        ScanFoeStack();
        StartCoroutine(IE_ExecuteFoeTurn());
    }

    private void BattleOverWin()
    {
        //Reward
        gameData.AddCharacterExp(1001, 50);
        gameData.AddCharacterExp(1002, 50);
        //Invoke Victory Page
        EventCenter.Instance.EventTrigger("NormalVictoryStart", null);

        //End Battle Flow
        isBattleEnd = true;
        PublicTool.GetGameData().gamePhase = GamePhase.Peace;
        PublicTool.EventChangeInteract(InteractState.PeaceNormal);
        EventCenter.Instance.EventTrigger("BattleEnd", null);
    }

    private void BattleOverLose()
    {
        isBattleEnd = true;
        PublicTool.EventChangeInteract(InteractState.PeaceNormal);
        EventCenter.Instance.EventTrigger("BattleLose", null);

    }

    #endregion


}
