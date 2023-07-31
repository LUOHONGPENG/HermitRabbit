using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameData
{

    //CharacterData
    public List<BattleCharacterData> listCharacter = new List<BattleCharacterData>();
    public Dictionary<int, BattleCharacterData> dicCharacter = new Dictionary<int, BattleCharacterData>();
    //CurPlantData
    public List<BattlePlantData> listPlant = new List<BattlePlantData>();
    public Dictionary<int, BattlePlantData> dicPlant = new Dictionary<int, BattlePlantData>();
    public List<int> listCurUnlockPlant = new List<int>();
    private int curPlantKeyID = -1;
    //FoeData
    public List<BattleFoeData> listFoe = new List<BattleFoeData>();
    public Dictionary<int, BattleFoeData> dicFoe = new Dictionary<int, BattleFoeData>();
    private int curFoeKeyID = -1;


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
        //Cur Plant
        listPlant.Clear();
        dicPlant.Clear();
        curPlantKeyID = -1;
        //Unlock Plant
        listCurUnlockPlant.Clear();
        //Test
        listCurUnlockPlant.Add(1001);
    }

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

    public void CheckClearPlant()
    {
        for (int i = listPlant.Count - 1; i >= 0; i--)
        {
            if (listPlant[i].isDead)
            {
                RemovePlantData(listPlant[i].keyID);
            }
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

    #region Basic-Foe
    public void NewGameFoeData()
    {
        listFoe.Clear();
        dicFoe.Clear();
        curFoeKeyID = -1;
    }

    public BattleFoeData GenerateFoeData(int typeID)
    {
        curFoeKeyID++;
        BattleFoeData foeData = new BattleFoeData(typeID, curFoeKeyID);
        listFoe.Add(foeData);
        dicFoe.Add(curFoeKeyID, foeData);
        return foeData;
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

    public void CheckClearFoe()
    {
        for (int i = listFoe.Count - 1; i >= 0; i--)
        {
            if (listFoe[i].isDead)
            {
                RemoveFoeData(listFoe[i].keyID);
            }
        }
    }

    public void RemoveFoeData(int keyID)
    {
        if (dicFoe.ContainsKey(keyID))
        {
            BattleFoeData foeData = dicFoe[keyID];
            listFoe.Remove(foeData);
            dicFoe.Remove(keyID);
        }
    }
    #endregion

    public BattleUnitData GetDataFromUnitInfo(UnitInfo unitInfo)
    {
        switch (unitInfo.type)
        {
            case BattleUnitType.Character:
                return GetBattleCharacterData(unitInfo.keyID);
            case BattleUnitType.Plant:
                return GetBattlePlantData(unitInfo.keyID);
            case BattleUnitType.Foe:
                return GetBattleFoeData(unitInfo.keyID);
        }
        return null;
    }
}
