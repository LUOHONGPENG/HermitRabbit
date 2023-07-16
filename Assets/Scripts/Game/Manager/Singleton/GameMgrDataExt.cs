using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameMgr
{
    public LevelData curLevelData = new LevelData();

    public void NewLevelData()
    {
        curLevelData = new LevelData();
        curLevelData.NewGameData();
    }


    public void LoadLevelData()
    {

    }

    public void SaveLevelData()
    {

    }
}
