using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{
    public static LevelData GetLevelData()
    {
        return GameMgr.Instance.curLevelData;
    }

    public static CharacterSkillExcelItem GetSkillItem(int SkillID)
    {
        return ExcelDataMgr.Instance.characterSkillExcelData.GetExcelItem(SkillID);
    }
}
