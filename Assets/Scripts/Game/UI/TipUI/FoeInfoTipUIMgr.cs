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
    public Image imgHPFill;
    public Text codeATK;
    public Text codeDEF;
    public Text codeRES;
    public Text codeMOV;


    public void ShowTip(int foeID)
    {
        BattleFoeData foeData = PublicTool.GetGameData().GetBattleFoeData(foeID);
        if(foeData != null)
        {
            imgHPFill.fillAmount = foeData.HPrate;

            codeATK.text = foeData.curATK.ToString();
            codeDEF.text = foeData.curDEF.ToString();
            codeRES.text = foeData.curRES.ToString();
            codeMOV.text = foeData.curMOV.ToString();
            objPopup.SetActive(true);
        }
    }

    public void HideTip()
    {
        objPopup.SetActive(false);
    }
}
