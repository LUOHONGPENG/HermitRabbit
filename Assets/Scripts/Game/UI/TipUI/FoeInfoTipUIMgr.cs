using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FoeInfoTipUIMgr : UnitInfoTipUIMgr
{
    private int recordTypeID = -1;

    protected override void UpdateSpecial(BattleUnitData unitData)
    {
        if (!objPopup.activeSelf || recordTypeID != unitData.typeID)
        {
            FoeExcelItem foeExcelItem = PublicTool.GetFoeExcelItem(unitData.typeID);
            if (foeExcelItem != null)
            {
                recordTypeID = unitData.typeID;
                codeName.text = foeExcelItem.name;
                codeDesc.text = foeExcelItem.desc;
            }
        }
    }
}
