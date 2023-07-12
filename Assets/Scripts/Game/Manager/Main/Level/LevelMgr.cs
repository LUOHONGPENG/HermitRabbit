using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMgr : MonoBehaviour
{
    [Header("Manager")]
    public MapMgr mapMgr;
    public UnitMgr unitMgr;
    public BattleMgr battleMgr;

    private LevelData levelData;
    private bool isInit = false;

    [Header("StateInfo")]
    public InteractState interactState;
    public int curActionUnitID = -1;

    #region Basic & Bind
    public void Init()
    {
        levelData = new LevelData();
        //If New Game
        levelData.NewGameData();

        mapMgr.Init(this);
        unitMgr.Init(this);
        battleMgr = BattleMgr.Instance;

        interactState = InteractState.Normal;

        isInit = true;
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("StartBattle", StartBattleEvent);
        EventCenter.Instance.AddEventListener("EndTurn", EndTurnEvent);
        EventCenter.Instance.AddEventListener("ShowBattleOption", ShowBattlePageEvent);
        EventCenter.Instance.AddEventListener("ChangeInteract", ChangeInteractEvent);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("StartBattle", StartBattleEvent);
        EventCenter.Instance.RemoveEventListener("EndTurn", EndTurnEvent);
        EventCenter.Instance.RemoveEventListener("ShowBattleOption", ShowBattlePageEvent);
        EventCenter.Instance.RemoveEventListener("ChangeInteract", ChangeInteractEvent);

    }



    private void FixedUpdate()
    {
        mapMgr.TimeGo();
    }
    #endregion

    #region EventDeal
    private void StartBattleEvent(object arg0)
    {
        battleMgr.StartNewBattle(this);
    }

    private void EndTurnEvent(object arg0)
    {
        battleMgr.EndTurnPhase();
    }

    private void ShowBattlePageEvent(object arg0)
    {
        curActionUnitID = (int)arg0;
    }

    private void ChangeInteractEvent(object arg0)
    {
        ChangeInteractStruct info = (ChangeInteractStruct)arg0;
        switch (info.state)
        {
            case InteractState.Move:
                interactState = InteractState.Move;
                break;
        }

    }
    #endregion

    #region GetInfo

    public LevelData GetLevelData()
    {
        return levelData;
    }

    public BattleUnitData GetCurrentUnit(BattleUnitType type)
    {
        LevelData levelData = GetLevelData();
        switch (type)
        {
            case BattleUnitType.Character:
                if (levelData.dicCharacter.ContainsKey(curActionUnitID))
                {
                    return levelData.dicCharacter[curActionUnitID];
                }
                break;
        }
        return null;
    }
    #endregion
}
