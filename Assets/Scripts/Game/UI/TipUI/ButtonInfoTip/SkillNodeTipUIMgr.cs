using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillNodeTipUIMgr : ButtonInfoTipUIMgr
{
    [Header("BasicInfo")]
    public Text codeName;
    public Text codeType;
    public Text codeDesc;
    public SkillRangeTipUIMgr rangeUI;

    [Header("Cost")]
    public Text codeCostSP;

    public void ShowTip(int nodeID,Vector2 mousePos)
    {
        if (!objPopup.activeSelf || recordID != nodeID)
        {
            SkillNodeExcelItem nodeExcelItem = PublicTool.GetSkillNodeItem(nodeID);
            codeName.text = nodeExcelItem.name;
            if (nodeExcelItem.nodeType == SkillNodeType.Active)
            {
                SkillExcelItem skillItem = PublicTool.GetSkillItem(nodeExcelItem.id);
                codeType.text = skillItem.activeSkillType.ToString();

                rangeUI.gameObject.SetActive(true);

                rangeUI.Init(skillItem.regionType, skillItem.RealRange, skillItem.RealRadius);

            }
            else
            {
                rangeUI.gameObject.SetActive(false);

                codeType.text = nodeExcelItem.nodeType.ToString();
            }
            codeDesc.text = nodeExcelItem.desc;

            recordID = nodeID;
        }
        ShowTipSetPos(mousePos);

    }
}
