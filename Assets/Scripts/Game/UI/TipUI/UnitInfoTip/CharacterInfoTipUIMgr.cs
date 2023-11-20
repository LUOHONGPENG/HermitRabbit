using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterInfoTipUIMgr : UnitInfoTipUIMgr
{
    private int recordTypeID = -1;

    protected override void UpdateSpecial(BattleUnitData unitData)
    {
        if (!objPopup.activeSelf || recordTypeID != unitData.typeID)
        {
            CharacterExcelItem characterExcelItem = PublicTool.GetCharacterExcelItem(unitData.typeID);
            if (characterExcelItem != null)
            {
                recordTypeID = unitData.typeID;
                codeName.text = characterExcelItem.name;
                codeDesc.text = "";
            }
        }
    }
}
