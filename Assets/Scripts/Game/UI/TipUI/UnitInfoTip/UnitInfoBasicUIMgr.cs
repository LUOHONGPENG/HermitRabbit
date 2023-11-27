using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoBasicUIMgr : MonoBehaviour
{
    [Header("Basic Info")]
    public Text codeName;
    public Text codeDesc;
    public Image imgHPFill;
    public Text codeHP;
    public Text codeATK;
    public Text codeDEF;
    public Text codeRES;
    public Text codeMOV;

    public GameObject objMove;


    public void UpdateInfo(BattleUnitData unitData)
    {
        imgHPFill.fillAmount = unitData.HPrate;
        codeHP.text = string.Format("{0}/{1}", unitData.curHP, unitData.curMaxHP);
        codeATK.text = unitData.curATK.ToString();
        codeDEF.text = unitData.curDEF.ToString();
        codeRES.text = unitData.curRES.ToString();
        codeMOV.text = unitData.curMOV.ToString();
    }

    public void SetNameDesc(string name,string desc)
    {
        codeName.text = name;
        codeDesc.text = desc;
    }

    public void HideMov()
    {
        objMove.SetActive(false);
    }

}
