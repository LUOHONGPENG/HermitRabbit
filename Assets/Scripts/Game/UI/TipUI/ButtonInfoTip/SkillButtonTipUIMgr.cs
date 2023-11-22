using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class SkillButtonTipUIMgr : ButtonInfoTipUIMgr
{
    [Header("BasicInfo")]
    public SkillDescTipUIMgr descUI;
    public SkillRangeTipUIMgr rangeUI;

    public void ShowTip(int skillID, Vector2 mousePos)
    {
        if (!objPopup.activeSelf || recordID != skillID)
        {
            SkillExcelItem skillItem = PublicTool.GetSkillItem(skillID);

            descUI.Init(skillID, false);

            rangeUI.Init(skillItem.regionType, skillItem.RealRange, skillItem.RealRadius);
            recordID = skillID;
        }

        ShowTipSetPos(mousePos);
    }


}
