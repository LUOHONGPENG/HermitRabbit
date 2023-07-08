using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelDataMgr : MonoSingleton<ExcelDataMgr>
{
    public CharacterSkillExcelData characterSkillExcelData;
    public CharacterExcelData characterExcelData;
    public FoeExcelData foeExcelData;

    public IEnumerator IE_Init()
    {
        characterSkillExcelData = ExcelManager.Instance.GetExcelData<CharacterSkillExcelData, CharacterSkillExcelItem>();
        characterExcelData = ExcelManager.Instance.GetExcelData<CharacterExcelData, CharacterExcelItem>();
        foeExcelData = ExcelManager.Instance.GetExcelData<FoeExcelData, FoeExcelItem>();
        yield break;
    }
    
}
