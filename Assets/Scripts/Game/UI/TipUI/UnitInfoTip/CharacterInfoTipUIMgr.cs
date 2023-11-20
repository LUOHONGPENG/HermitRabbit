using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterInfoTipUIMgr : UnitInfoTipUIMgr
{

    protected override void UpdateSpecial(BattleUnitData unitData)
    {
        //This Phase wont change when HP change
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
