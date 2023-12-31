using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameData
{
    #region Current Unit
    /// <summary>
    /// Recording curActionUnit
    /// </summary>
    private UnitInfo curBattleUnitInfo = new UnitInfo(BattleUnitType.Character, -1);

    public BattleUnitType GetCurUnitType()
    {
        return curBattleUnitInfo.type;
    }

    public UnitInfo GetCurUnitInfo()
    {
        return curBattleUnitInfo;
    }

    public BattleUnitData GetCurUnitData()
    {
        return GetDataFromUnitInfo(curBattleUnitInfo);
    }

    public void SetCurUnitInfo(UnitInfo unitInfo)
    {
        curBattleUnitInfo.type = unitInfo.type;
        curBattleUnitInfo.keyID = unitInfo.keyID;
    }
    #endregion

    #region Current Skill
    private SkillBattleInfo curSkillBattleInfo;

    public void SetCurBattleSkill(int skillID)
    {
        SkillExcelItem skillItem = PublicTool.GetSkillItem(skillID);
        curSkillBattleInfo = new SkillBattleInfo(skillItem, GetCurUnitData());
    }

    public int RefreshBattleSkillForCostHP()
    {
        SkillExcelItem skillItem = PublicTool.GetSkillItem(curSkillBattleInfo.ID);
        return new SkillBattleInfo(skillItem, GetCurUnitData()).damageModifier;
    }

    public void RefreshBattleSkill()
    {
        SkillExcelItem skillItem = PublicTool.GetSkillItem(curSkillBattleInfo.ID);
        if (skillItem != null)
        {
            curSkillBattleInfo = new SkillBattleInfo(skillItem, GetCurUnitData());
        }
    }

    public SkillBattleInfo GetCurSkillBattleInfo()
    {
        RefreshBattleSkill();
        return curSkillBattleInfo;
    }


    #endregion
}
