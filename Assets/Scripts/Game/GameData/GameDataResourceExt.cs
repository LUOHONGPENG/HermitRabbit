using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameData
{
    public int essence = 0;
    public int memory = 0;

    public int curEssence
    {
        get
        {
            int temp = 0;
            for(int i = 0; i < listPlant.Count; i++)
            {
                temp += listPlant[i].GetEssence();
            }
            return temp;
        }
    }

    private void NewGameResourceData()
    {
        essence = 2;
        memory = 0;
    }

    private void LoadResourceData(GameSaveData savedata)
    {
        essence = savedata.essence;
        memory = savedata.memory;
    }

    private void SaveResourceData(GameSaveData savedata)
    {
        savedata.essence = essence;
        savedata.memory = memory;
    }

    public void AddEssenceLimit(int essenceCount)
    {
        essence += essenceCount;
        EventCenter.Instance.EventTrigger("RefreshResourceUI",null);
    }

    public void AddMemory(int memoryCount)
    {
        memory += memoryCount;
        EventCenter.Instance.EventTrigger("RefreshResourceUI", null);

    }
}
