using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FoeInfoTipUIMgr : MonoBehaviour
{
    public GameObject objPopup;
    public Transform tfMouse;

    public Text codeName;
    public Text codeDesc;
    public Image imgHPFill;
    public Text codeHP;
    public Text codeATK;
    public Text codeDEF;
    public Text codeRES;
    public Text codeMOV;

    private int recordTypeID = -1;

    public void ShowTip(int foeID)
    {
        BattleFoeData foeData = PublicTool.GetGameData().GetBattleFoeData(foeID);
        if(foeData != null)
        {
            imgHPFill.fillAmount = foeData.HPrate;
            codeHP.text = string.Format("{0}/{1}", foeData.curHP, foeData.curMaxHP);
            codeATK.text = foeData.curATK.ToString();
            codeDEF.text = foeData.curDEF.ToString();
            codeRES.text = foeData.curRES.ToString();
            codeMOV.text = foeData.curMOV.ToString();

            if (!objPopup.activeSelf || recordTypeID!=foeData.typeID)
            {
                FoeExcelItem foeExcelItem = PublicTool.GetFoeExcelItem(foeData.typeID);
                if (foeExcelItem != null)
                {
                    recordTypeID = foeData.typeID;
                    codeName.text = foeExcelItem.name;
                    codeDesc.text = foeExcelItem.desc;
                }
            }

            objPopup.SetActive(true);
        }
        else
        {
            HideTip();
        }
    }

    public void HideTip()
    {
        objPopup.SetActive(false);
    }
}
