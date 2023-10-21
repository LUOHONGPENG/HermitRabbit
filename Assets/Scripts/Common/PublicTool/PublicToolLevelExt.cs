using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public partial class PublicTool
{
    public static GameData GetGameData()
    {
        return GameMgr.Instance.curGameData;
    }

    #region Excel

    public static SkillExcelItem GetSkillItem(int skillID)
    {
        return ExcelDataMgr.Instance.skillExcelData.GetExcelItem(skillID);
    }

    public static SkillNodeExcelItem GetSkillNodeItem(int nodeID)
    {
        return ExcelDataMgr.Instance.skillNodeExcelData.GetExcelItem(nodeID);
    }

    public static FoeExcelItem GetFoeExcelItem(int foeID)
    {
        return ExcelDataMgr.Instance.foeExcelData.GetExcelItem(foeID);
    }

    public static SkillSpecialExcelItem GetSkillSpecialItem(int specialID)
    {
        return ExcelDataMgr.Instance.skillSpecialExcelData.GetExcelItem(specialID);
    }

    public static BuffExcelItem GetBuffExcelItem(int buffID)
    {
        return ExcelDataMgr.Instance.buffExcelData.GetExcelItem(buffID);
    }

    public static MapClipExcelItem GetMapClipItem(int mapClipID)
    {
        return ExcelDataMgr.Instance.mapClipExcelData.GetExcelItem(mapClipID);
    }
    #endregion
}
