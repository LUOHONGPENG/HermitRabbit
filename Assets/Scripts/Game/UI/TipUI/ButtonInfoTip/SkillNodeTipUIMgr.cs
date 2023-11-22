using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillNodeTipUIMgr : ButtonInfoTipUIMgr
{
    [Header("Info")]
    public SkillDescTipUIMgr descUI;
    public SkillRangeTipUIMgr rangeUI;

    [Header("Cost")]
    public Text codeCostSP;

    public void ShowTip(int nodeID,Vector2 mousePos)
    {
        if (!objPopup.activeSelf || recordID != nodeID)
        {
            SkillNodeExcelItem nodeExcelItem = PublicTool.GetSkillNodeItem(nodeID);

            descUI.Init(nodeID,true);

            if (nodeExcelItem.nodeType == SkillNodeType.Active)
            {
                SkillExcelItem skillItem = PublicTool.GetSkillItem(nodeExcelItem.id);

                rangeUI.gameObject.SetActive(true);
                rangeUI.Init(skillItem.regionType, skillItem.RealRange, skillItem.RealRadius);
            }
            else
            {
                rangeUI.gameObject.SetActive(false);
            }

            recordID = nodeID;
        }
        ShowTipSetPos(mousePos);

    }
}
