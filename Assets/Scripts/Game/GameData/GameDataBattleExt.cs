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
        curSkillBattleInfo = new SkillBattleInfo(skillItem);
        SkillBattleInfo_PerkFilter();
    }

    public SkillBattleInfo GetCurSkillBattleInfo()
    {
        return curSkillBattleInfo;
    }

    /// <summary>
    /// Maybe I need to rewrite it
    /// </summary>
    public void SkillBattleInfo_PerkFilter()
    {
        if(GetCurUnitType() == BattleUnitType.Character)
        {
            BattleCharacterData character =(BattleCharacterData)GetCurUnitData();
            if (curSkillBattleInfo.activeSkillType == ActiveSkillType.SupportSkill && character.CheckUnlockSkillNode(2491))
            {
                curSkillBattleInfo.range = curSkillBattleInfo.range + 1;
            }
        }
    }
    #endregion
}
