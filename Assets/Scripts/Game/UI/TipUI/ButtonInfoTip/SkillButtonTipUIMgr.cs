using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class SkillButtonTipUIMgr : ButtonInfoTipUIMgr
{
    [Header("BasicInfo")]
    public Text codeName;
    public Text codeType;
    public TextMeshProUGUI codeDesc;
    public SkillRangeTipUIMgr rangeUI;

    public SkillCostTipUIMgr costTipUI;

    public void ShowTip(int skillID, Vector2 mousePos)
    {
        if (!objPopup.activeSelf || recordID != skillID)
        {
            SkillExcelItem skillItem = PublicTool.GetSkillItem(skillID);

            codeName.text = skillItem.name;
            codeType.text = skillItem.activeSkillType.ToString();

            codeDesc.spriteAsset = Resources.Load<TMP_SpriteAsset>("Sprite Assets/Common");
            codeDesc.text = skillItem.desc;

            RefreshCost(skillItem);

            rangeUI.Init(skillItem.regionType, skillItem.RealRange, skillItem.RealRadius);
            recordID = skillID;
        }

        ShowTipSetPos(mousePos);
    }

    private void RefreshCost(SkillExcelItem skillItem)
    {
        costTipUI.UpdateUI(skillItem.RealCostAP, skillItem.costMOV, skillItem.RealCostHP, skillItem.costMemory);
    }

}
