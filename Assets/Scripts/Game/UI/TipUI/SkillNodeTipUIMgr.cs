using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillNodeTipUIMgr : MonoBehaviour
{
    public GameObject objPopup;
    public Transform tfMouse;
    public Text codeName;
    public Text codeDesc;

    private int curNodeID;

    public void ShowTip(int nodeID,Vector2 mousePos)
    {
        if (nodeID != curNodeID)
        {
            SkillNodeExcelItem nodeExcelItem = PublicTool.GetSkillNodeItem(nodeID);
            codeName.text = nodeExcelItem.name;
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
