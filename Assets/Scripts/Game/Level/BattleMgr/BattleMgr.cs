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

    private Coroutine routineFoe = null;

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
        EventCenter.Instance.EventTrigger("EffectSkillName", "Interview Start");
        ResetNewTurnBefore(); 
        PublicTool.RecalculateOccupancy();
        ResetCharacterExp();
        RefreshWaterRange();
        battleTurnPhase = BattlePhase.CharacterPhase;
        StartTurnPhase();
        isBattleEnd = false;
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

    private void ResetNewTurnBefore()
    {
        List<BattleCharacterData> listCharacter = gameData.listCharacter;
        for(int i = 0; i < listCharacter.Count; i++)
        {
            listCharacter[i].ResetNewTurnBefore();
        }

        List<BattlePlantData> listPlant = gameData.listPlant;
        for (int i = 0; i < listPlant.Count; i++)
        {
            listPlant[i].ResetNewTurnBefore();
        }

        List<BattleFoeData> listFoe = gameData.listFoe;
        for (int i = 0; i < listFoe.Count; i++)
        {
            listFoe[i].ResetNewTurnBefore();
        }

        PublicTool.EventRefreshCharacterUI();
    }

    private void ResetNewTurnAfter()
    {
        List<BattleCharacterData> listCharacter = gameData.listCharacter;
        for (int i = 0; i < listCharacter.Count; i++)
        {
            listCharacter[i].ResetNewTurnAfter();
        }

        List<BattlePlantData> listPlant = gameData.listPlant;
        for (int i = 0; i < listPlant.Count; i++)
        {
            listPlant[i].ResetNewTurnAfter();
        }

        List<BattleFoeData> listFoe = gameData.listFoe;
        for (int i = 0; i < listFoe.Count; i++)
        {
            listFoe[i].ResetNewTurnAfter();
        }

        PublicTool.EventRefreshCharacterUI();
    }



    private void StartCharacterPhase()
    {
        PublicTool.EventChangeInteract(InteractState.WaitAction);
        StartCoroutine(IE_WholeStartCharacterTurn());

    }

    private void StartFoePhase()
    {
        PublicTool.EventChangeInteract(InteractState.WaitAction);

        routineFoe = StartCoroutine(IE_WholeFoeTurn());
    }




    private void BattleOverWin()
    {
        if (gameData.numDay < 7)
        {
            //Reward
            List<Vector2Int> listVictoryExp = new List<Vector2Int>();
            listVictoryExp.Add(new Vector2Int(1001, GetCharacterExp(1001)));
            listVictoryExp.Add(new Vector2Int(1002, GetCharacterExp(1002)));

            //Invoke Victory Page
            EventCenter.Instance.EventTrigger("NormalVictoryStart", listVictoryExp);

            //End Battle Flow
            isBattleEnd = true;
            gameData.numDay++;
            gameData.gamePhase = GamePhase.Peace;
            PublicTool.EventChangeInteract(InteractState.PeaceNormal);
            EventCenter.Instance.EventTrigger("BattleEnd", null);
        }
        else
        {
            isBattleEnd = true;
            gameData.gamePhase = GamePhase.Peace;
            PublicTool.EventChangeInteract(InteractState.PeaceNormal);
            EventCenter.Instance.EventTrigger("GoodEndStart", null);
        }

    }

    private void BattleOverLose()
    {
        isBattleEnd = true;
        PublicTool.EventChangeInteract(InteractState.PeaceNormal);
        EventCenter.Instance.EventTrigger("BattleLose", null);

    }

    public void StopForQuit()
    {
        if (routineFoe != null)
        {
            StopCoroutine(routineFoe);
        }
        StopAllCoroutines();

    }


    #endregion


}
