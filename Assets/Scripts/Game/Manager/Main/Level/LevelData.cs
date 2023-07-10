using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Storing the Data
/// </summary>
public class LevelData
{
    //CharacterData
    public List<BattleCharacterData> listCharacter = new List<BattleCharacterData>();
    public Dictionary<int, BattleCharacterData> dicCharacter = new Dictionary<int, BattleCharacterData>();
    //PlantData
    public List<BattlePlantData> listPlant = new List<BattlePlantData>();
    public Dictionary<int, BattlePlantData> dicPlant = new Dictionary<int, BattlePlantData>();
    private int curPlantKeyID = -1;
    //FoeData
    public List<BattleFoeData> listFoe = new List<BattleFoeData>();
    public Dictionary<int, BattleFoeData> dicFoe = new Dictionary<int, BattleFoeData>();
    private int curFoeKeyID = -1;

    public void NewGameData()
    {
        NewGameCharacterData();
        NewGamePlantData();
        NewGameFoeData();
    }

    #region Basic-Character
    public void NewGameCharacterData()
    {
        listCharacter.Clear();
        dicCharacter.Clear();
        //Leo
        GenerateCharacterData(1001);
        //Kizuna
        GenerateCharacterData(2001);
    }

    public void GenerateCharacterData(int typeID)
    {
        BattleCharacterData characterData = new BattleCharacterData(typeID);
        listCharacter.Add(characterData);
        dicCharacter.Add(typeID, characterData);
    }

    public BattleCharacterData GetBattleCharacterData(int ID)
    {
        if (dicCharacter.ContainsKey(ID))
        {
            return dicCharacter[ID];
        }
        else
        {
            return null;
        }
    }
    #endregion

    #region Basic-Plant
    public void NewGamePlantData()
    {
        listPlant.Clear();
        dicPlant.Clear();
        curPlantKeyID = -1;
    }

    public void GeneratePlantData(int typeID)
    {
        curPlantKeyID++;
        BattlePlantData plantData = new BattlePlantData(typeID,curPlantKeyID);
        listPlant.Add(plantData);
        dicPlant.Add(curPlantKeyID, plantData);
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
    #endregion

    #region Basic-Foe
    public void NewGameFoeData()
    {
        listFoe.Clear();
        dicFoe.Clear();
        curFoeKeyID = -1;
    }

    public void GenerateFoeData(int typeID)
    {
        curFoeKeyID++;
        BattleFoeData foeData = new BattleFoeData(typeID, curFoeKeyID);
        listFoe.Add(foeData);
        dicFoe.Add(curFoeKeyID, foeData);
    }

    public BattleFoeData GetBattleFoeData(int ID)
    {
        if (dicFoe.ContainsKey(ID))
        {
            return dicFoe[ID];
        }
        else
        {
            return null;
        }
    }
    #endregion

}
