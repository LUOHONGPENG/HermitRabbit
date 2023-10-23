using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameData
{

    //CharacterData
    public List<BattleCharacterData> listCharacter = new List<BattleCharacterData>();
    public Dictionary<int, BattleCharacterData> dicCharacter = new Dictionary<int, BattleCharacterData>();




    #region Basic-Character
    public void NewGameCharacterData()
    {
        listCharacter.Clear();
        dicCharacter.Clear();
        //Leo
        GenerateCharacterData(1001);
        //Kizuna
        GenerateCharacterData(1002);
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

    public void AddCharacterExp(int characterID,int EXP)
    {
        if (dicCharacter.ContainsKey(characterID))
        {
            dicCharacter[characterID].EXP += EXP;
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
