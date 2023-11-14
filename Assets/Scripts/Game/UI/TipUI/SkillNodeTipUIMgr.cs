using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillNodeTipUIMgr : MonoBehaviour
{
    public GameObject objPopup;
    public Transform tfMouse;
    public Text codeName;
    public Text codeType;
    public Text codeDesc;
    public SkillRangeTipUIMgr rangeUI;

    private int curNodeID;

    public void ShowTip(int nodeID,Vector2 mousePos)
    {
        if (nodeID != curNodeID)
        {
            SkillNodeExcelItem nodeExcelItem = PublicTool.GetSkillNodeItem(nodeID);
            codeName.text = nodeExcelItem.name;
            if(nodeExcelItem.nodeType == SkillNodeType.Active)
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
        }

        tfMouse.position = new Vector3(mousePos.x,mousePos.y,tfMouse.transform.position.z);
        objPopup.SetActive(true);
    }

    public void HideTip()
    {
        objPopup.SetActive(false);
    }

}
