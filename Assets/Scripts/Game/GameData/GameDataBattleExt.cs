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

    private int curBattleSkillID = -1;
    private SkillBattleInfo curSkillBattleInfo;

    public void SetCurBattleSkillID(int skillID)
    {
        curBattleSkillID = skillID;
        CharacterSkillExcelItem skillItem = PublicTool.GetSkillItem(skillID);
        curSkillBattleInfo = new SkillBattleInfo(skillItem);
    }

    public int GetCurBattleSkillID()
    {
        return curBattleSkillID;
    }

    public SkillBattleInfo GetCurSkillBattleInfo()
    {
        return curSkillBattleInfo;
    }
    #endregion
}