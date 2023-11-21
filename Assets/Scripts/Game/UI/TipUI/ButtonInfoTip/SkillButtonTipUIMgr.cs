using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class SkillButtonTipUIMgr : ButtonInfoTipUIMgr
{
    [Header("BasicInfo")]
    public Text codeName;
    public Text codeType;
    public Text codeDesc;
    public SkillRangeTipUIMgr rangeUI;

    [Header("Cost")]
    public GameObject objCostAP;
    public Text codeCostAP;
    public GameObject objCostMOV;
    public Text codeCostMOV;
    public GameObject objCostHP;
    public Text codeCostHP;
    public GameObject objCostMemory;
    public Text codeCostMemory;

    public void ShowTip(int skillID, Vector2 mousePos)
    {
        if (!objPopup.activeSelf || recordID != skillID)
        {
            SkillExcelItem skillItem = PublicTool.GetSkillItem(skillID);

            codeName.text = skillItem.name;
            codeType.text = skillItem.activeSkillType.ToString();
            codeDesc.text = skillItem.desc;

            RefreshCost(skillItem);

            rangeUI.Init(skillItem.regionType, skillItem.RealRange, skillItem.RealRadius);
            recordID = skillID;
        }

        ShowTipSetPos(mousePos);
    }

    private void RefreshCost(SkillExcelItem skillItem)
    {
        if (skillItem.RealCostAP > 0)
        {
            objCostAP.SetActive(true);
            codeCostAP.text = skillItem.RealCostAP.ToString();
        }
        else
        {
            objCostAP.SetActive(false);
        }

        if (skillItem.costMOV > 0)
        {
            objCostMOV.SetActive(true);
            codeCostMOV.text = skillItem.costMOV.ToString();
        }
        else
        {
            objCostMOV.SetActive(false);
        }

        if (skillItem.RealCostHP > 0)
        {
            objCostHP.SetActive(true);
            codeCostHP.text = skillItem.RealCostHP.ToString();
        }
        else
        {
            objCostHP.SetActive(false);
        }

        if (skillItem.costMemory > 0)
        {
            objCostMemory.SetActive(true);
            codeCostMemory.text = skillItem.costMemory.ToString();
        }
        else
        {
            objCostMemory.SetActive(false);
        }
    }

}
