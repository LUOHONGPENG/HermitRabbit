using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlantInfoTipUIMgr : UnitInfoTipUIMgr
{
    private int recordTypeID = -1;

    protected override void UpdateSpecial(BattleUnitData unitData)
    {
        if (!objPopup.activeSelf || recordTypeID != unitData.typeID)
        {
            PlantExcelItem plantExcelItem = PublicTool.GetPlantItem(unitData.typeID);
            if (plantExcelItem != null)
            {
                recordTypeID = unitData.typeID;
                codeName.text = plantExcelItem.name;
                codeDesc.text = plantExcelItem.desc;
                codeMOV.gameObject.SetActive(false);
            }
        }
    }
}
