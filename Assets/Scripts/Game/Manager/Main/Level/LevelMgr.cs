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
    public LevelPhase levelPhase = LevelPhase.Peace;

    #region Basic & Bind
    public void Init()
    {
        levelData = new LevelData();
        //If New Game
        levelData.NewGameData();

        mapMgr.Init(this);
        unitMgr.Init(this);
        battleMgr = BattleMgr.Instance;
        battleMgr.Init(this);

        isInit = true;
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("StartBattle", StartBattleEvent);
        EventCenter.Instance.AddEventListener("EndTurn", EndTurnEvent);
        EventCenter.Instance.AddEventListener("ChangeInteract", ChangeInteractEvent);
        EventCenter.Instance.AddEventListener("RefreshPosInfo", RefreshPosInfoEvent);

    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("StartBattle", StartBattleEvent);
        EventCenter.Instance.RemoveEventListener("EndTurn", EndTurnEvent);
        EventCenter.Instance.RemoveEventListener("ChangeInteract", ChangeInteractEvent);
        EventCenter.Instance.RemoveEventListener("RefreshPosInfo", RefreshPosInfoEvent);
    }

    private void FixedUpdate()
    {
        if (!isInit)
        {
            return;
        }
        mapMgr.TimeGo();
    }
    #endregion

    #region EventDeal
    private void StartBattleEvent(object arg0)
    {
        levelPhase = LevelPhase.Battle;
        battleMgr.StartNewBattle(this);
    }

    private void EndTurnEvent(object arg0)
    {
        battleMgr.EndTurnPhase();
    }

    private void ChangeInteractEvent(object arg0)
    {
        InteractState state = (InteractState)arg0;
        InputMgr.Instance.SetInteractState(state);

    }

    private void RefreshPosInfoEvent(object arg0)
    {
        GetLevelData().RefreshTempPos();
    }
    #endregion

    #region GetInfo

    public LevelData GetLevelData()
    {
        return levelData;
    }
    #endregion
}
