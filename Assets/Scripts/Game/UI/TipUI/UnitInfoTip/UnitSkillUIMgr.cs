using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitSkillUIMgr : MonoBehaviour
{
    public Text codeName;

    public Text codeRange;
    public Text codeRadius;

    public CommonRangeUIMgr rangeIconUI;

    public void Init(int skillID,BattleUnitType type)
    {
        SkillExcelItem skillItem = PublicTool.GetSkillItem(skillID);
        SkillDescExcelItem descItem = PublicTool.GetSkillDescItem(skillID);

        rangeIconUI.Init(skillItem.regionType, skillItem.RealRange, skillItem.RealRadius, type);

        codeName.text = descItem.GetName();

        codeRange.text = skillItem.RealRange.ToString();
        codeRadius.text = skillItem.RealRadius.ToString();
    }
}
