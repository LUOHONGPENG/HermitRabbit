using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlling the flow of the battle
/// </summary>
public partial class BattleMgr : Singleton<BattleMgr>
{
    public int numTurn;
    public BattlePhase battleTurnPhase;

    public Stack<int> stackCharacter;
    public Stack<int> stackPlant;
    public Stack<int> stackFoe;

    private LevelMgr parent;

    #region Basic Function

    public void Init(LevelMgr parent)
    {
        this.parent = parent;
    }

    public void StartNewBattle(LevelMgr parent)
    {
        this.parent = parent;

        numTurn = 1;
        battleTurnPhase = BattlePhase.Character;
        StartTurnPhase();
    }

    public void StartTurnPhase()
    {
        switch (battleTurnPhase)
        {
            case BattlePhase.Character:
                StartCharacterPhase();
                break;
            case BattlePhase.Plant:
                StartPlantPhase();
                break;
            case BattlePhase.Foe:
                StartFoePhase();
                break;
        }
        Debug.Log("Start Turn " + numTurn + " " + battleTurnPhase.ToString());
        EventCenter.Instance.EventTrigger("RefreshTileInfo", null);
    }

    public void EndTurnPhase()
    {
        switch (battleTurnPhase)
        {
            case BattlePhase.Character:
                battleTurnPhase = BattlePhase.Plant;
                break;
            case BattlePhase.Plant:
                battleTurnPhase = BattlePhase.Foe;
                break;
            case BattlePhase.Foe:
                battleTurnPhase = BattlePhase.Character;
                numTurn++;
                break;
        }
        StartTurnPhase();
    }

    private void StartCharacterPhase()
    {
        PublicTool.EventChangeInteract(InteractState.Normal);
    }

    private void StartPlantPhase()
    {
        PublicTool.EventChangeInteract(InteractState.WaitAction);
    }

    private void StartFoePhase()
    {
        PublicTool.EventChangeInteract(InteractState.WaitAction);
    }
    #endregion


}
