using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelDataMgr : MonoSingleton<ExcelDataMgr>
{
    public CharacterExcelData characterExcelData;
    public SkillExcelData skillExcelData;
    public FoeExcelData foeExcelData;

    public IEnumerator IE_Init()
    {
        characterExcelData = ExcelManager.Instance.GetExcelData<CharacterExcelData, CharacterExcelItem>();
        skillExcelData = ExcelManager.Instance.GetExcelData<SkillExcelData, SkillExcelItem>();
        foeExcelData = ExcelManager.Instance.GetExcelData<FoeExcelData, FoeExcelItem>();
        skillExcelData.Init();
        yield break;
    }
    
}
