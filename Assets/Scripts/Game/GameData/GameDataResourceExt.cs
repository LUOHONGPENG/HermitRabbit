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
            return 0;
        }
    }

    public void NewGameResourceData()
    {
        essence = 1;
        memory = 0;
    }

    public void AddEssenceLimit(int essenceCount)
    {
        essence += essenceCount;

    }

    public void AddMemory(int memoryCount)
    {
        memory += memoryCount;
    }
}
