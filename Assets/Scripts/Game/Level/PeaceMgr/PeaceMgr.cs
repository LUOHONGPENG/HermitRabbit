using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PeaceMgr : MonoSingleton<PeaceMgr>
{
    private MapViewMgr mapViewMgr;
    private UnitViewMgr unitViewMgr;
    private GameData gameData;

    public void Init(LevelMgr levelMgr)
    {
        this.mapViewMgr = levelMgr.mapViewMgr;
        this.unitViewMgr = levelMgr.unitViewMgr;
        this.gameData = PublicTool.GetGameData();
    }

    public void StartPlant()
    {

    }

    public void EndPlant()
    {

    }
}
