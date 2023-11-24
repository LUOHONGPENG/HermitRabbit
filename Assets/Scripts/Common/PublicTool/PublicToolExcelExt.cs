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

    public static SkillDescExcelItem GetSkillDescItem(int skillID)
    {
        return ExcelDataMgr.Instance.skillDescExcelData.GetExcelItem(skillID);
    }

    public static List<SkillTag> GetSkillDescTag(int skillID)
    {
        return ExcelDataMgr.Instance.skillDescExcelData.GetTag(skillID);
    }

    public static SkillTagExcelItem GetSkillTagItem(SkillTag tag)
    {
        return ExcelDataMgr.Instance.skillTagExcelData.GetItem(tag);
    }

    public static List<SkillPerformInfo> GetSkillPerformInfo(int skillID)
    {
        if (ExcelDataMgr.Instance.skillPerformExcelData.dicSkillPerformInfo.ContainsKey(skillID))
        {
            return ExcelDataMgr.Instance.skillPerformExcelData.dicSkillPerformInfo[skillID];
        }
        else
        {
            return null;
        }
    }

    public static float GetSkillPerformTotalTime(int skillID)
    {
        if (ExcelDataMgr.Instance.skillPerformExcelData.dicSkillPerformTotalTime.ContainsKey(skillID))
        {
            return ExcelDataMgr.Instance.skillPerformExcelData.dicSkillPerformTotalTime[skillID];
        }
        else
        {
            return 0.5F;
        }
    }

    public static SkillEffectViewExcelItem GetSkillEffectViewExcelItem(EffectViewType viewType)
    {
        if (ExcelDataMgr.Instance.skillEffectViewExcelData.dicSkillEffectView.ContainsKey(viewType))
        {
            return ExcelDataMgr.Instance.skillEffectViewExcelData.dicSkillEffectView[viewType];
        }
        else
        {
            return null;
        }
    }


    public static CharacterExcelItem GetCharacterExcelItem(int characterID)
    {
        return ExcelDataMgr.Instance.characterExcelData.GetExcelItem(characterID);
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

    public static PlantExcelItem GetPlantItem(int plantID)
    {
        return ExcelDataMgr.Instance.plantExcelData.GetExcelItem(plantID);
    }

    public static MapClipExcelItem GetMapClipItem(int mapClipID)
    {
        return ExcelDataMgr.Instance.mapClipExcelData.GetExcelItem(mapClipID);
    }

    public static MapTileExcelItem GetMapTileItem(MapTileType tileType)
    {
        return ExcelDataMgr.Instance.mapTileExcelData.GetMapTileInfo(tileType);
    }
    #endregion

    public static string GetLanguageText(string key)
    {
        return ExcelDataMgr.Instance.languageExcelData.GetText(key);
    }
}
