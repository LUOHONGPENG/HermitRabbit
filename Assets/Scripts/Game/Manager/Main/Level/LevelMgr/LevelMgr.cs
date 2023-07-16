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

    private bool isInit = false;

    private LevelData levelData;


    #region Basic & Bind
    public void Init()
    {
        levelData = PublicTool.GetLevelData();

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


}
