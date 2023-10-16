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

    public override void ResetNewTurn()
    {
        TurnBuffDecrease();
    }

    #region Basic Attribute
    public override int curATK
    {
        get
        {
            int tempATK = 0;
            tempATK += item.ATK;
            tempATK += buffATK;
            return tempATK;
        }
    }

    public override int curDEF
    {
        get
        {
            int tempDEF = 0;
            tempDEF += item.DEF;
            tempDEF += buffDEF;
            return tempDEF;
        }
    }

    public override int curRES
    {
        get
        {
            int tempRES = 0;
            tempRES += item.RES;
            tempRES += buffRES;
            return tempRES;
        }
    }
    #endregion
}
