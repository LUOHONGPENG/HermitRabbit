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
    }

    public void StartTurnPhase()
    {
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

        PublicTool.EventRefreshCharacterUI();
    }

    private void StartCharacterPhase()
    {
        EventCenter.Instance.EventTrigger("CharacterPhaseStart", null);
        PublicTool.EventChangeInteract(InteractState.Normal);
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
    #endregion


}
