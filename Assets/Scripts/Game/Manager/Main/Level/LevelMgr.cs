using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMgr : MonoBehaviour
{
    public MapMgr mapMgr;
    public UnitMgr unitMgr;
    public BattleMgr battleMgr;

    private LevelData levelData;
    private bool isInit = false;

    #region Basic & Bind
    public void Init()
    {
        levelData = new LevelData();
        //If New Game
        levelData.NewGameData();

        mapMgr.Init(this);
        unitMgr.Init(this);
        battleMgr = BattleMgr.Instance;

        isInit = true;
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("StartBattle", StartBattleEvent);
        EventCenter.Instance.AddEventListener("EndTurn", EndTurnEvent);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("StartBattle", StartBattleEvent);
        EventCenter.Instance.RemoveEventListener("EndTurn", EndTurnEvent);
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
    #endregion

    public LevelData GetLevelData()
    {
        return levelData;
    }
}
