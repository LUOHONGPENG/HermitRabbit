using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelDataMgr : MonoSingleton<ExcelDataMgr>
{
    public SkillExcelData skillExcelData;
    public SkillSpecialExcelData skillSpecialExcelData;
    public SkillNodeExcelData skillNodeExcelData;
    public SkillPerformExcelData skillPerformExcelData;
    public SkillEffectViewExcelData skillEffectViewExcelData;
    public BuffExcelData buffExcelData;

    public MapClipExcelData mapClipExcelData;

    public CharacterExcelData characterExcelData;
    public CharacterExpExcelData characterExpExcelData;
    public FoeExcelData foeExcelData;
    public DayExcelData dayExcelData;
    public PlantExcelData plantExcelData;

    public SoundExcelData soundExcelData;

    public IEnumerator IE_Init()
    {
        skillExcelData = ExcelManager.Instance.GetExcelData<SkillExcelData, SkillExcelItem>();
        skillSpecialExcelData = ExcelManager.Instance.GetExcelData<SkillSpecialExcelData, SkillSpecialExcelItem>();
        skillNodeExcelData = ExcelManager.Instance.GetExcelData<SkillNodeExcelData, SkillNodeExcelItem>();
        skillPerformExcelData = ExcelManager.Instance.GetExcelData<SkillPerformExcelData, SkillPerformExcelItem>();
        skillEffectViewExcelData = ExcelManager.Instance.GetExcelData<SkillEffectViewExcelData, SkillEffectViewExcelItem>();
        buffExcelData = ExcelManager.Instance.GetExcelData<BuffExcelData, BuffExcelItem>();

        mapClipExcelData = ExcelManager.Instance.GetExcelData<MapClipExcelData, MapClipExcelItem>();

        characterExcelData = ExcelManager.Instance.GetExcelData<CharacterExcelData, CharacterExcelItem>();
        characterExpExcelData = ExcelManager.Instance.GetExcelData<CharacterExpExcelData, CharacterExpExcelItem>();
        foeExcelData = ExcelManager.Instance.GetExcelData<FoeExcelData, FoeExcelItem>();
        dayExcelData = ExcelManager.Instance.GetExcelData<DayExcelData, DayExcelItem>();
        plantExcelData = ExcelManager.Instance.GetExcelData<PlantExcelData, PlantExcelItem>();

        soundExcelData = ExcelManager.Instance.GetExcelData<SoundExcelData, SoundExcelItem>();

        //Init Data Table
        skillExcelData.Init();
        skillNodeExcelData.Init();
        skillPerformExcelData.Init();
        skillEffectViewExcelData.Init();

        characterExpExcelData.Init();
        dayExcelData.Init();
        soundExcelData.Init();
        yield break;
    }
    
}
