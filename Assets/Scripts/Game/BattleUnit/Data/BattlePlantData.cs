using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlantData : BattleUnitData
{
    private PlantExcelItem item;

    public BattlePlantData(int typeID,int keyID)
    {
        //Basic Setting
        this.typeID = typeID;
        this.keyID = keyID;
        this.battleUnitType = BattleUnitType.Plant;

        item = ExcelDataMgr.Instance.plantExcelData.GetExcelItem(typeID);
        curHP = item.HP;
        maxHP = item.HP;
    }

    public PlantExcelItem GetItem()
    {
        return item;
    }

    public int GetSkillID()
    {
        return item.skillID;
    }

    #region Basic Attribute
    public override int curATK
    {
        get
        {
            return item.ATK;
        }
    }

    public override int curDEF
    {
        get
        {
            return item.DEF;
        }
    }

    public override int curRES
    {
        get
        {
            return item.RES;
        }
    }
    #endregion
}
