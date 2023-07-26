using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{
    public static GameData GetGameData()
    {
        return GameMgr.Instance.curGameData;
    }

    public static SkillExcelItem GetSkillItem(int SkillID)
    {
        return ExcelDataMgr.Instance.skillExcelData.GetExcelItem(SkillID);
    }
}
