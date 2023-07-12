using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMgr : Singleton<BattleMgr>
{
    public int numTurn;
    public BattleTurnPhase battleTurnPhase;

    public Stack<int> stackCharacter;
    public Stack<int> stackPlant;
    public Stack<int> stackFoe;

    private LevelMgr parent;


    #region Basic Function
    public void StartNewBattle(LevelMgr parent)
    {
        numTurn = 1;
        battleTurnPhase = BattleTurnPhase.Character;
        StartTurnPhase();
    }

    public void StartTurnPhase()
    {
        switch (battleTurnPhase)
        {
            case BattleTurnPhase.Character:
                StartCharacterPhase();
                break;
            case BattleTurnPhase.Plant:
                StartPlantPhase();
                break;
            case BattleTurnPhase.Foe:
                StartFoePhase();
                break;
        }
        Debug.Log("Start Turn " + numTurn + " " + battleTurnPhase.ToString());
    }

    public void EndTurnPhase()
    {
        switch (battleTurnPhase)
        {
            case BattleTurnPhase.Character:
                battleTurnPhase = BattleTurnPhase.Plant;
                break;
            case BattleTurnPhase.Plant:
                battleTurnPhase = BattleTurnPhase.Foe;
                break;
            case BattleTurnPhase.Foe:
                battleTurnPhase = BattleTurnPhase.Character;
                numTurn++;
                break;
        }
        StartTurnPhase();
    }

    private void StartCharacterPhase()
    {

    }

    private void StartPlantPhase()
    {

    }

    private void StartFoePhase()
    {

    }
    #endregion
}
