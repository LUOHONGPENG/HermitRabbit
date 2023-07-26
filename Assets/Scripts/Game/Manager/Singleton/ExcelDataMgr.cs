using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelDataMgr : MonoSingleton<ExcelDataMgr>
{
    public SkillExcelData skillExcelData;
    public CharacterExcelData characterExcelData;
    public FoeExcelData foeExcelData;
    public PlantExcelData plantExcelData;

    public IEnumerator IE_Init()
    {
        skillExcelData = ExcelManager.Instance.GetExcelData<SkillExcelData, SkillExcelItem>();
        characterExcelData = ExcelManager.Instance.GetExcelData<CharacterExcelData, CharacterExcelItem>();
        foeExcelData = ExcelManager.Instance.GetExcelData<FoeExcelData, FoeExcelItem>();
        plantExcelData = ExcelManager.Instance.GetExcelData<PlantExcelData, PlantExcelItem>();
        skillExcelData.Init();
        yield break;
    }
    
}
