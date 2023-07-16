using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameMgr
{
    public GameData curGameData = new GameData();

    public void NewLevelData()
    {
        curGameData = new GameData();
        curGameData.NewGame();
    }


    public void LoadGameData()
    {

    }

    public void SaveGameData()
    {

    }
}
