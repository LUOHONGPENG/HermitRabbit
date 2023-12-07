using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlantInfoTipUIMgr : UnitInfoTipUIMgr
{
    public UnitSkillUIMgr skillUIMgr;
    public Transform tfBuff;
    public GameObject pfBuff;

    public GameObject objDetail;
    public GameObject objDetailText;

    protected override void UpdateSpecial(BattleUnitData unitData)
    {
        //This Phase wont change when HP change
        if (!objPopup.activeSelf || recordTypeID != unitData.typeID)
        {
            PlantExcelItem plantExcelItem = PublicTool.GetPlantItem(unitData.typeID);
            if (plantExcelItem != null)
            {
                recordTypeID = unitData.typeID;
                basicUIMgr.SetNameDesc(plantExcelItem.GetName(), plantExcelItem.GetDesc());
                basicUIMgr.SetType(BattleUnitType.Plant);
                basicUIMgr.HideMov();

                if (plantExcelItem.triggerCondition != PlantTriggerType.Passive)
                {
                    skillUIMgr.gameObject.SetActive(true);
                    skillUIMgr.Init(plantExcelItem.skillID, BattleUnitType.Plant);


                    //BuffUI

                    PublicTool.ClearChildItem(tfBuff);

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
                else
                {
                    PublicTool.ClearChildItem(tfBuff);

                    skillUIMgr.gameObject.SetActive(false);
                }
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
