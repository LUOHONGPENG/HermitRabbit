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
            if(skillItem.skillSubjectType != BattleUnitType.Character)
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

public partial class CharacterSkillExcelItem
{
    public bool isTargetFoe
    {
        get
        {
            if(foeEffect != SkillEffectType.None)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool isTargetCharacter
    {
        get
        {
            if (characterEffect != SkillEffectType.None)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool isTargetPlant
    {
        get
        {
            if (plantEffect != SkillEffectType.None)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public float damageDeltaFloat
    {
        get
        {
            return 0.01f * damageDelta;
        }
    }
}
