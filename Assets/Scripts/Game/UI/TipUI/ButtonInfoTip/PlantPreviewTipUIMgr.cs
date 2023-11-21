using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class PlantPreviewTipUIMgr : ButtonInfoTipUIMgr
{
    [Header("BasicInfo")]
    public Text codeName;
    public Text codeDesc;

    [Header("DataInfo")]
    public Text codeHP;
    public Text codeATK;
    public Text codeDEF;
    public Text codeRES;

    [Header("Cost")]
    public Text codeCost;

    public void ShowTip(int plantID, Vector2 mousePos)
    {
        if (!objPopup.activeSelf || recordID != plantID)
        {
            PlantExcelItem plantExcelItem = PublicTool.GetPlantItem(plantID);

            codeName.text = plantExcelItem.name;
            codeDesc.text = plantExcelItem.desc;

            codeHP.text = plantExcelItem.HP.ToString();
            codeATK.text = plantExcelItem.ATK.ToString();
            codeDEF.text = plantExcelItem.DEF.ToString();
            codeRES.text = plantExcelItem.RES.ToString();

            codeCost.text = plantExcelItem.essence.ToString();

            recordID = plantID;
        }
        ShowTipSetPos(mousePos);

    }
}
