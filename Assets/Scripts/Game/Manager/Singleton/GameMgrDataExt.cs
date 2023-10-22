using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameMgr
{
    public GameData curGameData = new GameData();

    public void NewGameInitData()
    {
        curGameData = new GameData();
        curGameData.NewGame();
    }


    public void LoadGameData(SaveSlotName saveSlotName)
    {
        GameSaveData tempSaveData = GetSaveData(saveSlotName);
        if(tempSaveData != null)
        {
            curGameData = new GameData();
            curGameData.LoadGameData(tempSaveData);
        }
    }

    public GameSaveData GetSaveData(SaveSlotName saveSlotName)
    {
        var json = RabbitSaveSystem.LoadFromPlayerPrefs(saveSlotName.ToString());
        GameSaveData tempSaveData = JsonUtility.FromJson<GameSaveData>(json);
        return tempSaveData;
    }

    public bool CheckSaveData(SaveSlotName saveSlotName)
    {
        GameSaveData tempSaveData = GetSaveData(saveSlotName);
        if (tempSaveData != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SaveGameData(SaveSlotName saveSlotName)
    {
        if (curGameData != null)
        {
            GameSaveData newSaveData = new GameSaveData();
            curGameData.SaveGameData(newSaveData);
            RabbitSaveSystem.SaveByPlayerPrefs(saveSlotName.ToString(), newSaveData);
        }
    }
}
