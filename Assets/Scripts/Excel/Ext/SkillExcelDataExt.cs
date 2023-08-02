using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkillExcelData
{
    public Dictionary<int, List<SkillExcelItem>> dicAllCharacterSkill = new Dictionary<int, List<SkillExcelItem>>();

    public void Init()
    {
        dicAllCharacterSkill.Clear();

        for (int i = 0; i < items.Length; i++)
        {
            SkillExcelItem skillItem = items[i];
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
                List<SkillExcelItem> listSkill = dicAllCharacterSkill[skillItem.characterID];
                listSkill.Add(skillItem);
            }
            else
            {
                List<SkillExcelItem> listSkill = new List<SkillExcelItem>();
                listSkill.Add(skillItem);
                dicAllCharacterSkill.Add(skillItem.characterID, listSkill);
            }

        }
    }
}

public partial class SkillExcelItem
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

    public List<SkillSpecialExcelItem> listSpecialEffectUse
    {
        get
        {
            List<SkillSpecialExcelItem> listTemp = new List<SkillSpecialExcelItem>();

            for(int i = 0; i < listSpecialEffect.Count; i++)
            {
                int tempID = listSpecialEffect[i];
                if (tempID > 0)
                {
                    listTemp.Add(ExcelDataMgr.Instance.skillSpecialExcelData.GetExcelItem(listSpecialEffect[i]));
                }
            }

            return listTemp;
        }
    }
}

