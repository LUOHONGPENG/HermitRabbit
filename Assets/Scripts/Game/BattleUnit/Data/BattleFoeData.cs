using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BattleFoeData : BattleUnitData
{
    private FoeExcelItem item;

    public BattleFoeData(int typeID,int keyID)
    {
        //Basic Setting
        this.typeID = typeID;
        this.keyID = keyID;
        this.battleUnitType = BattleUnitType.Foe;
        item = ExcelDataMgr.Instance.foeExcelData.GetExcelItem(typeID);
        curHP = item.HP;
        maxHP = item.HP;
        curMOV = item.MOV;
        maxMOV = item.MOV;
    }

    public FoeExcelItem GetItem()
    {
        return item;
    }

    public int GetSkillID()
    {
        return item.skillID;
    }

    public int GetSkillTouchRange()
    {
        SkillExcelItem skillItem = PublicTool.GetSkillItem(GetSkillID());
        if (skillItem != null)
        {
            return skillItem.range + skillItem.radius;
        }
        else
        {
            return 0;
        }

    }

    public override void ResetNewTurn()
    {
        curMOV = curMaxMOV;
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

    public override int curMaxMOV
    {
        get
        {
            int tempMOV = 0;
            tempMOV += maxMOV;
            tempMOV += buffMOV;
            return tempMOV;
        }
    }
    #endregion

}
