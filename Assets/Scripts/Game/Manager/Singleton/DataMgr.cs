using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : MonoSingleton<DataMgr>
{
    [HideInInspector]
    public CharacterSkillExcelData characterSkillExcelData;

    [HideInInspector]
    public CharacterDataExcelData characterDataExcelData;


    public IEnumerator IE_Init()
    {
        characterSkillExcelData = ExcelManager.Instance.GetExcelData<CharacterSkillExcelData, CharacterSkillExcelItem>();
        characterDataExcelData = ExcelManager.Instance.GetExcelData<CharacterDataExcelData, CharacterDataExcelItem>();
        yield break;
    }
    
}
