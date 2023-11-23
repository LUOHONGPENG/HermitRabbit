using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class SkillButtonTipUIMgr : ButtonInfoTipUIMgr
{
    [Header("BasicInfo")]
    public SkillDescTipUIMgr descUI;
    public SkillRangeTipUIMgr rangeUI;

    [Header("Tag")]
    public Transform tfTag;
    public GameObject pfTag;

    public void ShowTip(int skillID, Vector2 mousePos)
    {
        if (!objPopup.activeSelf || recordID != skillID)
        {
            SkillExcelItem skillItem = PublicTool.GetSkillItem(skillID);
            //Desc
            descUI.Init(skillID, false);
            //Range
            rangeUI.Init(skillItem.regionType, skillItem.RealRange, skillItem.RealRadius);

            //TagUI
            PublicTool.ClearChildItem(tfTag);
            List<SkillTag> listTag = PublicTool.GetSkillDescTag(skillID);
            for (int i = 0; i < listTag.Count; i++)
            {
                GameObject objTag = GameObject.Instantiate(pfTag, tfTag);
                SkillTagTipUIMgr tagUI = objTag.GetComponent<SkillTagTipUIMgr>();
                tagUI.Init(listTag[i]);
            }
            recordID = skillID;
        }

        ShowTipSetPos(mousePos);
    }


}
