using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LevelMgr : MonoBehaviour
{
    [Header("Manager")]
    public MapViewMgr mapViewMgr;
    public UnitViewMgr unitViewMgr;
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

        mapViewMgr.Init();
        unitViewMgr.Init();
        battleMgr = BattleMgr.Instance;
        battleMgr.Init(this);

        isInit = true;
    }


    private void FixedUpdate()
    {
        if (!isInit)
        {
            return;
        }
        mapViewMgr.TimeGo();
    }
    #endregion


    #region GetInfo

    public LevelData GetLevelData()
    {
        return levelData;
    }
    #endregion
}
