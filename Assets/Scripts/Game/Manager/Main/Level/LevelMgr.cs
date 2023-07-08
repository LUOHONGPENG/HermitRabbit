using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMgr : MonoBehaviour
{
    public MapMgr mapMgr;
    public UnitMgr unitMgr;

    private LevelData levelData;
    private bool isInit = false;

    public void Init()
    {
        //If New Game
        levelData = new LevelData();
        levelData.Init();

        mapMgr.Init(this);
        unitMgr.Init(this);

        isInit = true;
    }

    public LevelData GetLevelData()
    {
        return levelData;
    }

    private void FixedUpdate()
    {
        mapMgr.TimeGo();
    }


}
