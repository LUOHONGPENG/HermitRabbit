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

    [Header("Cost")]
    public GameObject objCostAP;
    public Text codeCostAP;
    public GameObject objCostMOV;
    public Text codeCostMOV;
    public GameObject objCostHP;
    public Text codeCostHP;
    public GameObject objCostMemory;
    public Text codeCostMemory;

    public void ShowTip(int skillID,int characterID, Vector2 mousePos)
    {
        SkillExcelItem skillItem = PublicTool.GetSkillItem(skillID);

        codeName.text = skillItem.name;
        codeType.text = skillItem.activeSkillType.ToString();
        codeDesc.text = skillItem.desc;

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

        tfMouse.position = new Vector3(mousePos.x, mousePos.y, tfMouse.transform.position.z);
        objPopup.SetActive(true);
    }

    public void HideTip()
    {
        objPopup.SetActive(false);
    }
}
