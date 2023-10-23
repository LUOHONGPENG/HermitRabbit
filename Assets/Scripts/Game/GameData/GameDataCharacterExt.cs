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
        //Mage
        GenerateCharacterData(1001);
        //Worker
        GenerateCharacterData(1002);
    }

    public void LoadCharacterData(GameSaveData saveData)
    {
        //SkillInfo
        Dictionary<int, List<int>> dicSaveSkillNode = new Dictionary<int, List<int>>();
        Dictionary<int, int> dicSaveSP = new Dictionary<int, int>();

        //PreSkill
        for (int i = 0; i < saveData.listCharacterSkill.Count; i++)
        {
            Vector2Int saveSkillInfo = saveData.listCharacterSkill[i];
            if (dicSaveSkillNode.ContainsKey(saveSkillInfo.x))
            {
                dicSaveSkillNode[saveSkillInfo.x].Add(saveSkillInfo.y);
            }
            else
            {
                List<int> listTemp = new List<int>();
                listTemp.Add(saveSkillInfo.y);
                dicSaveSkillNode.Add(saveSkillInfo.x, listTemp);
            }
            int costSP = PublicTool.GetSkillNodeItem(saveSkillInfo.y).costSP;
            if (dicSaveSP.ContainsKey(saveSkillInfo.x))
            {
                dicSaveSP[saveSkillInfo.x] += costSP;
            }
            else
            {
                dicSaveSP.Add(saveSkillInfo.x, costSP);
            }
        }

        listCharacter.Clear();
        dicCharacter.Clear();
        for(int i = 0; i < saveData.listCharacterExp.Count; i++)
        {
            Vector2Int saveExpInfo = saveData.listCharacterExp[i];
            int characterID = saveExpInfo.x;
            GenerateCharacterData(characterID);
            BattleCharacterData characterData = dicCharacter[characterID];
            characterData.EXP = saveExpInfo.y;
            if (dicSaveSkillNode.ContainsKey(characterID))
            {
                List<int> listSkillNodeID = dicSaveSkillNode[characterID];
                characterData.listUnlockSkillNode.Clear();
                for (int j = 0; j < listSkillNodeID.Count; j++)
                {
                    characterData.AcquireSkillNode(listSkillNodeID[j]);
                }
            }
            if (dicSaveSP.ContainsKey(characterID))
            {
                characterData.SPSpent = dicSaveSP[characterID];
            }
        }
    }

    public void SaveCharacterData(GameSaveData saveData)
    {
        saveData.listCharacterExp.Clear();
        saveData.listCharacterSkill.Clear();

        for (int i = 0; i < listCharacter.Count; i++)
        {
            BattleCharacterData characterData = listCharacter[i];
            Vector2Int saveExpInfo = new Vector2Int(characterData.typeID, characterData.EXP);
            saveData.listCharacterExp.Add(saveExpInfo);
            for(int j = 0; j < characterData.listUnlockSkillNode.Count; j++)
            {
                int SkillNodeID = characterData.listUnlockSkillNode[j];
                Vector2Int saveSkillNodeInfo = new Vector2Int(characterData.typeID, SkillNodeID);
                saveData.listCharacterSkill.Add(saveSkillNodeInfo);
            }
        }
    }
    #endregion

    #region Use-Character

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
