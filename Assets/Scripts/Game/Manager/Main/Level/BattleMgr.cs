using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMgr : Singleton<BattleMgr>
{
    public int numTurn;
    public BattlePhase battleTurnPhase;

    public Stack<int> stackCharacter;
    public Stack<int> stackPlant;
    public Stack<int> stackFoe;

    private LevelMgr parent;


    #region Basic Function
    public void StartNewBattle(LevelMgr parent)
    {
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

    }

    private void StartPlantPhase()
    {

    }

    private void StartFoePhase()
    {

    }
    #endregion
}
