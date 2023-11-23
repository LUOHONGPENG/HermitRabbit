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

    [Header("Tag")]
    public Transform tfTag;
    public GameObject pfTag;

    [Header("Cost")]
    public Text codeCostSP;

    public void ShowTip(int nodeID,Vector2 mousePos)
    {
        if (!objPopup.activeSelf || recordID != nodeID)
        {
            SkillNodeExcelItem nodeExcelItem = PublicTool.GetSkillNodeItem(nodeID);

            //DescUI
            descUI.Init(nodeID,true);

            //RangeUI
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

            //TagUI
            PublicTool.ClearChildItem(tfTag);
            List<SkillTag> listTag = PublicTool.GetSkillDescTag(nodeID);
            for(int i = 0; i < listTag.Count; i++)
            {
                GameObject objTag = GameObject.Instantiate(pfTag, tfTag);
                SkillTagTipUIMgr tagUI = objTag.GetComponent<SkillTagTipUIMgr>();
                tagUI.Init(listTag[i]);
            }

            recordID = nodeID;
        }
        ShowTipSetPos(mousePos);

    }
}
