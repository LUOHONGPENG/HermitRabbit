using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class SkillButtonTipUIMgr : MonoBehaviour
{
    public GameObject objPopup;
    public Transform tfMouse;
    public Text codeName;
    public Text codeType;
    public Text codeDesc;

    public void ShowTip(int skillID,int characterID, Vector2 mousePos)
    {
        SkillExcelItem skillItem = PublicTool.GetSkillItem(skillID);

        codeName.text = skillItem.name;
        codeType.text = skillItem.activeSkillType.ToString();
        codeDesc.text = skillItem.desc;

        tfMouse.position = new Vector3(mousePos.x, mousePos.y, tfMouse.transform.position.z);
        objPopup.SetActive(true);
    }


    public void HideTip()
    {
        objPopup.SetActive(false);
    }
}
