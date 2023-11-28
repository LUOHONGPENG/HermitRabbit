using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlantInfoTipUIMgr : UnitInfoTipUIMgr
{
    public UnitSkillUIMgr skillUIMgr;

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
                basicUIMgr.SetType(BattleUnitType.Plant);
                basicUIMgr.HideMov();

                if (plantExcelItem.triggerCondition != PlantTriggerType.Passive)
                {
                    skillUIMgr.gameObject.SetActive(true);
                    skillUIMgr.Init(plantExcelItem.skillID, BattleUnitType.Plant);
                }
                else
                {
                    skillUIMgr.gameObject.SetActive(false);
                }
            }
        }
    }
}
