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

    public Stack<int> stackCharacter;
    public Stack<int> stackFoe;

    private LevelMgr parent;
    private MapViewMgr mapViewMgr;
    private UnitViewMgr unitViewMgr;
    private GameData gameData;


    #region Basic Function

    public void Init(LevelMgr parent)
    {
        this.parent = parent;
        this.mapViewMgr = parent.mapViewMgr;
        this.unitViewMgr = parent.unitViewMgr;
        this.gameData = PublicTool.GetGameData();
    }

    public void StartNewBattle(LevelMgr parent)
    {
        this.parent = parent;

        numTurn = 1;
        battleTurnPhase = BattlePhase.CharacterPhase;
        StartTurnPhase();
    }

    public void StartTurnPhase()
    {
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
        PublicTool.EventRefreshOccupancy();
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
                break;
        }
        StartTurnPhase();
    }

    private void StartCharacterPhase()
    {
        PublicTool.EventChangeInteract(InteractState.Normal);
    }


    private void StartFoePhase()
    {
        PublicTool.EventChangeInteract(InteractState.WaitAction);
    }
    #endregion


}
