using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LevelMgr : MonoBehaviour
{
    [Header("Manager")]
    public MapViewMgr mapViewMgr;
    public UnitViewMgr unitViewMgr;
    public EffectViewMgr effectViewMgr;
    public BattleMgr battleMgr;
    public PeaceMgr peaceMgr;

    private bool isInit = false;

    private GameData gameData;

    #region Basic & Bind
    public void Init()
    {
        gameData = PublicTool.GetGameData();

        mapViewMgr.Init();
        unitViewMgr.Init();
        effectViewMgr.Init();

        battleMgr = BattleMgr.Instance;
        battleMgr.Init(this);
        peaceMgr = PeaceMgr.Instance;
        peaceMgr.Init(this);

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
