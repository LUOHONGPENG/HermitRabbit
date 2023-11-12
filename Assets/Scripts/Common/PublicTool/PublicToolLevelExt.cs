using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{

    public static void RecalculateOccupancy()
    {
        if (GetGameData() != null)
        {
            GetGameData().RecalculateOccupancy();
        }
    }

    public static void RecalculateSkillCover()
    {
        if (GetGameData() != null)
        {
            GetGameData().RecalculateSkillCover();
        }
    }

    public static int GetPlantNumInThisRow(int rowID, int typeID)
    {
        int temp = 0;
        GameData gameData = GetGameData();
        foreach(Vector2Int pos in gameData.listTempPlantPos)
        {
            if(pos.y == rowID && gameData.dicTempMapUnit.ContainsKey(pos))
            {
                UnitInfo unitInfo = gameData.dicTempMapUnit[pos];
                BattlePlantData plantData =(BattlePlantData)gameData.GetDataFromUnitInfo(unitInfo);
                if(plantData.typeID == typeID)
                {
                    temp++;
                }
            }
        }
        return temp;
    }

    public static int GetPlantNumInThisColumn(int columnID, int typeID)
    {
        int temp = 0;
        GameData gameData = GetGameData();
        foreach (Vector2Int pos in gameData.listTempPlantPos)
        {
            if (pos.x == columnID && gameData.dicTempMapUnit.ContainsKey(pos))
            {
                UnitInfo unitInfo = gameData.dicTempMapUnit[pos];
                BattlePlantData plantData = (BattlePlantData)gameData.GetDataFromUnitInfo(unitInfo);
                if (plantData.typeID == typeID)
                {
                    temp++;
                }
            }
        }
        return temp;
    }

}
