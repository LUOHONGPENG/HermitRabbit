using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMgr : Singleton<BattleMgr>
{
    public int numTurn;
    public BattleTurnPhase battleTurnPhase;

    public Stack<BattleCharacterView> stackCharacter;
    public Stack<BattlePlantView> stackPlant;
    public Stack<BattleFoeView> stackFoe;

    private LevelMgr parent;

    public void StartNewBattle(LevelMgr parent)
    {
        numTurn = 1;
        battleTurnPhase = BattleTurnPhase.Character;


    }

    public void StartTurnPhase()
    {
        switch (battleTurnPhase)
        {
            case BattleTurnPhase.Character:

                break;
        }
    }
}
