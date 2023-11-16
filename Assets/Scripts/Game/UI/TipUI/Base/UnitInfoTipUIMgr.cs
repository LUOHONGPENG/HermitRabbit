using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoTipUIMgr : MonoBehaviour
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


    public void UpdateBasicInfo(UnitInfo unitInfo)
    {
        BattleUnitData unitData = PublicTool.GetGameData().GetDataFromUnitInfo(unitInfo);

        if (unitData != null)
        {
            imgHPFill.fillAmount = unitData.HPrate;
            codeHP.text = string.Format("{0}/{1}", unitData.curHP, unitData.curMaxHP);
            codeATK.text = unitData.curATK.ToString();
            codeDEF.text = unitData.curDEF.ToString();
            codeRES.text = unitData.curRES.ToString();
            codeMOV.text = unitData.curMOV.ToString();

            UpdateSpecial(unitData);
            objPopup.SetActive(true);
        }
        else
        {
            HideTip();
        }
    }

    protected virtual void UpdateSpecial(BattleUnitData unitData)
    {

    } 


    public void HideTip()
    {
        objPopup.SetActive(false);
    }
}
