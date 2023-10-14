using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{
    public static bool CheckWhetherCharacterUnlockSkill(int characterID,int skillNodeID)
    {
        if (GetGameData().dicCharacter.ContainsKey(characterID))
        {
            BattleCharacterData characterData = GetGameData().dicCharacter[characterID];
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
