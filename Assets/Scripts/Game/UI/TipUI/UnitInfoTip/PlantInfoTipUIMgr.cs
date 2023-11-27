using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlantInfoTipUIMgr : UnitInfoTipUIMgr
{

    protected override void UpdateSpecial(BattleUnitData unitData)
    {
        //This Phase wont change when HP change
        if (!objPopup.activeSelf || recordTypeID != unitData.typeID)
        {
            PlantExcelItem plantExcelItem = PublicTool.GetPlantItem(unitData.typeID);
            if (plantExcelItem != null)
            {
                recordTypeID = unitData.typeID;
                basicUIMgr.SetNameDesc(plantExcelItem.name, plantExcelItem.desc);
                basicUIMgr.HideMov();
            }
        }
    }
}
