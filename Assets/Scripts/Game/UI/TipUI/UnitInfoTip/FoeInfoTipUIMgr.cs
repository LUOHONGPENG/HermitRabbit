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
    public UnitSkillUIMgr skillUIMgr;

    public GameObject objDetail;
    public GameObject objDetailText;

    protected override void UpdateSpecial(BattleUnitData unitData)
    {
        //This Phase wont change when HP change
        if (!objPopup.activeSelf || recordTypeID != unitData.typeID)
        {
            FoeExcelItem foeExcelItem = PublicTool.GetFoeExcelItem(unitData.typeID);
            if (foeExcelItem != null)
            {
                recordTypeID = unitData.typeID;
                basicUIMgr.SetNameDesc(foeExcelItem.GetName(), foeExcelItem.GetDesc());
                basicUIMgr.SetType(BattleUnitType.Foe);


                skillUIMgr.Init(foeExcelItem.skillID,BattleUnitType.Foe);
            }
        }
    }

    private void Update()
    {
        if (InputMgr.Instance.isPressDetail)
        {
            objDetail.SetActive(true);
            objDetailText.SetActive(false);
        }
        else
        {
            objDetail.SetActive(false);
            objDetailText.SetActive(true);

        }
    }
}
