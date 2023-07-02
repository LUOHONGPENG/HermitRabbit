using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Storing the Data
/// </summary>
public class LevelData
{
    public List<BattleCharacterData> listCharacter = new List<BattleCharacterData>();
    public Dictionary<int, BattleCharacterData> dicCharacter = new Dictionary<int, BattleCharacterData>();

    public void Init()
    {
        InitCharacterData();
    }

    public void InitCharacterData()
    {
        BattleCharacterData character_1001 = new BattleCharacterData(1001);
        listCharacter.Add(character_1001);
        dicCharacter.Add(1001, character_1001);

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
}
