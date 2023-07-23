using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CharacterSkillExcelData
{
    public Dictionary<int, List<CharacterSkillExcelItem>> dicAllCharacterSkill = new Dictionary<int, List<CharacterSkillExcelItem>>();

    public void Init()
    {
        dicAllCharacterSkill.Clear();

        for (int i = 0; i < items.Length; i++)
        {
            CharacterSkillExcelItem skillItem = items[i];
            if (skillItem.isNormalAttack)
            {
                continue;
            }
            if (dicAllCharacterSkill.ContainsKey(skillItem.characterID))
            {
                List<CharacterSkillExcelItem> listSkill = dicAllCharacterSkill[skillItem.characterID];
                listSkill.Add(skillItem);
            }
            else
            {
                List<CharacterSkillExcelItem> listSkill = new List<CharacterSkillExcelItem>();
                listSkill.Add(skillItem);
                dicAllCharacterSkill.Add(skillItem.characterID, listSkill);
            }

        }
    }
}
