using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LevelData
{
    #region Current Unit
    /// <summary>
    /// Recording curActionUnit
    /// </summary>
    private UnitInfo curBattleUnitInfo = new UnitInfo(BattleUnitType.Character, -1);

    public UnitInfo GetCurUnitInfo()
    {
        return curBattleUnitInfo;
    }

    public BattleUnitData GetCurUnitData()
    {
        return GetDataFromUnitInfo(curBattleUnitInfo);
    }

    public void SetCurUnitInfo(BattleUnitType type, int keyID)
    {
        curBattleUnitInfo.type = type;
        curBattleUnitInfo.keyID = keyID;
    }
    #endregion

    #region Current Skill

    private int curBattleSkillID = -1;

    #endregion
}
