using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{
    public static BattleCharacterData GetCharacterData(int characterID)
    {
        if (GetGameData().dicCharacter.ContainsKey(characterID))
        {
            BattleCharacterData characterData = GetGameData().dicCharacter[characterID];
            return characterData;
        }
        else
        {
            return null;
        }
    }


    public static bool CheckWhetherCharacterUnlockSkill(int characterID,int skillNodeID)
    {
        if (GetCharacterData(characterID) != null)
        {
            BattleCharacterData characterData = GetCharacterData(characterID);
            if (characterData.CheckUnlockSkillNode(skillNodeID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
