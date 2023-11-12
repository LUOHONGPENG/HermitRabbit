using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameData
{
    public List<int> listPlantHeld = new List<int>();
    //CurPlantData
    public List<BattlePlantData> listPlant = new List<BattlePlantData>();
    public Dictionary<int, BattlePlantData> dicPlant = new Dictionary<int, BattlePlantData>();
    private int curPlantKeyID = -1;

    #region Basic-Plant
    public void NewGamePlantData()
    {
        //Unlock Plant
        listPlantHeld.Clear();
        //Test
        listPlantHeld.Add(1001);
        listPlantHeld.Add(1002);
        listPlantHeld.Add(1003);
        listPlantHeld.Add(1004);

        //Cur Plant
        listPlant.Clear();
        dicPlant.Clear();
        curPlantKeyID = -1;
    }

    public void LoadPlantData(GameSaveData savedata)
    {
        //Held Plant
        listPlantHeld.Clear();
        for (int i = 0; i < savedata.listPlantHeld.Count; i++)
        {
            listPlantHeld.Add(savedata.listPlantHeld[i]);
        }
        //Used Plant
        listPlant.Clear();
        dicPlant.Clear();
        curPlantKeyID = -1;
        for (int i = 0; i < savedata.listPlantUsed.Count; i++)
        {
            Vector3Int savePlantInfo = savedata.listPlantUsed[i];
            Vector2Int savePosID = new Vector2Int(savePlantInfo.x, savePlantInfo.y);
            BattlePlantData newPlant = GeneratePlantData(savePlantInfo.z);
            newPlant.posID = savePosID;
        }
    }

    public void SavePlantData(GameSaveData savedata)
    {
        //Held Plant
        savedata.listPlantHeld.Clear();
        for (int i = 0; i < listPlantHeld.Count; i++)
        {
            savedata.listPlantHeld.Add(listPlantHeld[i]);
        }
        //Used Plant
        savedata.listPlantUsed.Clear();
        for(int i = 0; i < listPlant.Count; i++)
        {
            BattlePlantData plantData = listPlant[i];
            Vector3Int savePlantInfo = new Vector3Int(plantData.posID.x, plantData.posID.y, plantData.typeID);
            savedata.listPlantUsed.Add(savePlantInfo);
        }
    }
    #endregion

    #region Use-Plant

    public BattlePlantData GeneratePlantData(int typeID)
    {
        curPlantKeyID++;
        BattlePlantData plantData = new BattlePlantData(typeID, curPlantKeyID);
        listPlant.Add(plantData);
        dicPlant.Add(curPlantKeyID, plantData);
        return plantData;
    }

    public BattlePlantData GetBattlePlantData(int ID)
    {
        if (dicPlant.ContainsKey(ID))
        {
            return dicPlant[ID];
        }
        else
        {
            return null;
        }
    }


    public void RemovePlantData(int keyID)
    {
        if (dicPlant.ContainsKey(keyID))
        {
            BattlePlantData plantData = dicPlant[keyID];
            listPlant.Remove(plantData);
            dicPlant.Remove(keyID);
        }
    }
    #endregion
}
