using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : MonoSingleton<DataMgr>
{
    [HideInInspector]
    public CharacterSkillExcelData characterSkillExcelData;

    public IEnumerator IE_Init()
    {
        characterSkillExcelData = ExcelManager.Instance.GetExcelData<CharacterSkillExcelData, CharacterSkillExcelItem>();
        yield break;
    }
    
}
