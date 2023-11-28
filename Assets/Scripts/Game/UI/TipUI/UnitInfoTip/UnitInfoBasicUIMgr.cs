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


    [Header("Special")]
    public Image imgBg;
    public Image imgType;
    public List<Sprite> listSpType = new List<Sprite>();
    public List<Color> listColorTx = new List<Color>();
    public List<Color> listColorBg = new List<Color>();

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

    public void SetType(BattleUnitType type)
    {
        switch (type)
        {
            case BattleUnitType.Character:
                imgBg.color = listColorBg[0];
                imgType.sprite = listSpType[0];
                codeName.color = listColorTx[0];
                codeDesc.color = listColorTx[0];
                break;
            case BattleUnitType.Foe:
                imgBg.color = listColorBg[1];
                imgType.sprite = listSpType[1];
                codeName.color = listColorTx[1];
                codeDesc.color = listColorTx[1];
                break;
            case BattleUnitType.Plant:
                imgBg.color = listColorBg[2];
                imgType.sprite = listSpType[2];
                codeName.color = listColorTx[2];
                codeDesc.color = listColorTx[2];
                break;
        }
    }


    public void HideMov()
    {
        objMove.SetActive(false);
    }

}
