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
    protected override void UpdateSpecial(BattleUnitData unitData)
    {
        //This Phase wont change when HP change
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
