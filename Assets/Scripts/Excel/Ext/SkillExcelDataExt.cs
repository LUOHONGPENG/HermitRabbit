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
            if (skillItem.activeSkillType == ActiveSkillType.NormalAttack)
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
                List<SkillExcelItem> listSkill = new List<SkillExcelItem> { skillItem };
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
            if (foeEffect != SkillEffectType.None)
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

    /// <summary>
    /// The source type of the skill
    /// </summary>
    public BattleUnitType skillSubjectType
    {
        get
        {
            switch (activeSkillType)
            {
                case ActiveSkillType.NormalAttack:
                case ActiveSkillType.DamageSkill:
                case ActiveSkillType.SupportSkill:
                case ActiveSkillType.UltimateSkill:
                    return BattleUnitType.Character;
                case ActiveSkillType.PlantSkill:
                    return BattleUnitType.Plant;
                case ActiveSkillType.MonsterSkill:
                    return BattleUnitType.Foe;
                default:
                    return BattleUnitType.Character;
            }
        }
    }

    public float damageDeltaFloat
    {
        get
        {
            return 0.01f * RealDamageDelta;
        }
    }

    public List<BuffExcelItem> listBuffUse
    {
        get
        {
            List<BuffExcelItem> listTemp = new List<BuffExcelItem>();
            for (int i = 0; i < listBuffEffect.Count; i++)
            {
                int tempID = listBuffEffect[i];
                if (tempID > 0)
                {
                    listTemp.Add(PublicTool.GetBuffExcelItem(tempID));
                }
            }
            return listTemp;
        }
    }

    public List<SkillSpecialExcelItem> listSpecialEffectUse
    {
        get
        {
            List<SkillSpecialExcelItem> listTemp = new List<SkillSpecialExcelItem>();

            for (int i = 0; i < listSpecialEffect.Count; i++)
            {
                int tempID = listSpecialEffect[i];
                if (tempID > 0)
                {
                    listTemp.Add(PublicTool.GetSkillSpecialItem(tempID));
                }
            }

            return listTemp;
        }
    }

    public int RealCostAP
    {
        get
        {
            int temp = costAP;
            if (id == 1101)
            {
                if (PublicTool.CheckWhetherCharacterUnlockSkill(1001, 1191))
                {
                    temp--;
                }
                else if (PublicTool.CheckWhetherCharacterUnlockSkill(1001, 1193) && characterID == 1001)
                {
                    BattleCharacterData characterData = PublicTool.GetCharacterData(characterID);
                    if (characterData.curAP > costAP)
                    {
                        temp = characterData.curAP;
                    }
                }
            }
            else if(id == 1301)
            {
                if (PublicTool.CheckWhetherCharacterUnlockSkill(1001, 1391))
                {
                    temp++;
                }
            }
            return temp;
        }
    }

    public int RealCostHP
    {
        get
        {
            int temp = costHP;
            if (id == 2903)
            {
                if (characterID == 1002)
                {
                    BattleCharacterData characterData = PublicTool.GetCharacterData(characterID);
                    if (costHP + 1 >= characterData.curHP)
                    {
                        temp = Mathf.RoundToInt(characterData.curHP - 1);
                    }
                }
            }
            return temp;
        }
    }

    public int RealRange
    {
        get
        {
            int temp = range;
            if (characterID == 1002 && activeSkillType == ActiveSkillType.SupportSkill && PublicTool.CheckWhetherCharacterUnlockSkill(1002, 2491))
            {
                temp++;
            }
            if (characterID == 1001 && id != 1903)
            {
                BattleCharacterData characterData = PublicTool.GetCharacterData(characterID);
                if (characterData.CheckBuffExist(1005))
                {
                    temp += 2;
                }
            }
            return temp;
        }
    }

    public int RealRadius
    {
        get
        {
            int temp = radius;
            if (id == 1101 && PublicTool.CheckWhetherCharacterUnlockSkill(1001, 1192))
            {
                temp++;
            }
            if (skillSubjectType == BattleUnitType.Character && PublicTool.CheckWhetherCharacterUnlockSkill(1001, 1902) &&

                 (activeSkillType == ActiveSkillType.SupportSkill || activeSkillType == ActiveSkillType.DamageSkill))
            {
                BattleCharacterData characterData = PublicTool.GetCharacterData(characterID);
                if (characterData.CheckBuffExist(1003))
                {
                    temp++;
                }
            }
            return temp;
        }
    }

    public SkillDamageDeltaStd RealDamageStd
    {
        get
        {
            SkillDamageDeltaStd temp = damageDeltaStd;
            if (id == 1401 && PublicTool.CheckWhetherCharacterUnlockSkill(1001, 1491))
            {
                temp = SkillDamageDeltaStd.ATKDISD2;
            }
            return temp;
        }
    }


    public float RealDamageDelta
    {
        get
        {
            float temp = damageDelta;
            if (id == 1101)
            {
                if (PublicTool.CheckWhetherCharacterUnlockSkill(1001, 1193) && characterID == 1001)
                {
                    BattleCharacterData characterData = PublicTool.GetCharacterData(characterID);
                    if (characterData.curAP > costAP)
                    {
                        temp += (characterData.curAP - costAP) * 115;
                    }
                }

                //Radius Up
                if (PublicTool.CheckWhetherCharacterUnlockSkill(1001, 1192) && id == 1101)
                {
                    temp *= 0.75f;
                }
            }
            return temp;
        }
    }


    public int RealDamageModifier
    {
        get
        {
            int temp = damageModifier;
            //Inferno AP - 1
            if (id == 1101 && PublicTool.CheckWhetherCharacterUnlockSkill(1001, 1191))
            {
                temp -= 4;
            }
            //Lost HP add damage
            if(characterID == 1002 && PublicTool.CheckWhetherCharacterUnlockSkill(1002, 2191))
            {
                if(activeSkillType == ActiveSkillType.NormalAttack || activeSkillType == ActiveSkillType.DamageSkill || activeSkillType == ActiveSkillType.UltimateSkill)
                {
                    BattleCharacterData characterData = PublicTool.GetCharacterData(characterID);
                    int lostHP = Mathf.RoundToInt((characterData.curMaxHP - characterData.curHP) / 2);
                    temp += lostHP;
                }
            }
            return temp;
        }
    }

    public SkillTileEffectType RealTileEffectType
    {
        get
        {
            SkillTileEffectType temp = tileEffectType;

            if(id == 1301 && PublicTool.CheckWhetherCharacterUnlockSkill(1001, 1391))
            {
                temp = SkillTileEffectType.WaterPlus;
            }
            return temp;
        }
    }
}

