using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class PlantPreviewTipUIMgr : ButtonInfoTipUIMgr
{
    [Header("BasicInfo")]
    public Text codeName;
    public TextMeshProUGUI codeDesc;

    [Header("DataInfo")]
    public Text codeHP;
    public Text codeATK;
    public Text codeDEF;
    public Text codeRES;

    [Header("Cost")]
    public Text codeCost;

    [Header("Tag")]
    public Transform tfBuff;
    public GameObject pfBuff;

    public void ShowTip(int plantID, Vector2 mousePos)
    {
        if (!objPopup.activeSelf || recordID != plantID)
        {
            PlantExcelItem plantExcelItem = PublicTool.GetPlantItem(plantID);

            codeName.text = plantExcelItem.GetName();
            codeDesc.text = plantExcelItem.GetDesc();

            codeHP.text = plantExcelItem.HP.ToString();
            codeATK.text = plantExcelItem.ATK.ToString();
            codeDEF.text = plantExcelItem.DEF.ToString();
            codeRES.text = plantExcelItem.RES.ToString();

            codeCost.text = plantExcelItem.essence.ToString();

            recordID = plantID;

            //BuffUI
            PublicTool.ClearChildItem(tfBuff);

            if (plantExcelItem.triggerCondition != PlantTriggerType.Passive)
            {
                SkillDescExcelItem descItem = PublicTool.GetSkillDescItem(plantExcelItem.skillID);
                List<int> listBuff = descItem.listBuffType;
                if (listBuff[0] != 0)
                {
                    for (int i = 0; i < listBuff.Count; i++)
                    {
                        BuffExcelItem buffItem = PublicTool.GetBuffExcelItem(listBuff[i]);
                        GameObject objBuff = GameObject.Instantiate(pfBuff, tfBuff);
                        SkillBuffTipUIMgr buffUI = objBuff.GetComponent<SkillBuffTipUIMgr>();
                        buffUI.Init(buffItem);
                    }
                }
            }
        }
        ShowTipSetPos(mousePos);

    }
}
