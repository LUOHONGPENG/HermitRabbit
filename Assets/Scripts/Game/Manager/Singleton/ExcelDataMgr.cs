using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelDataMgr : MonoSingleton<ExcelDataMgr>
{
    public CharacterExcelData characterExcelData;
    public CharacterSkillExcelData characterSkillExcelData;
    public FoeExcelData foeExcelData;

    public IEnumerator IE_Init()
    {
        characterExcelData = ExcelManager.Instance.GetExcelData<CharacterExcelData, CharacterExcelItem>();
        characterSkillExcelData = ExcelManager.Instance.GetExcelData<CharacterSkillExcelData, CharacterSkillExcelItem>();
        foeExcelData = ExcelManager.Instance.GetExcelData<FoeExcelData, FoeExcelItem>();
        characterSkillExcelData.Init();
        yield break;
    }
    
}
