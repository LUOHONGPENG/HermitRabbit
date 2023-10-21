using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BattleFoeData : BattleUnitData
{
    private FoeExcelItem item;

    public int exp;

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
        if (skillItem != null && skillItem.regionType == SkillRegionType.Circle)
        {
            return skillItem.RealRange + skillItem.RealRadius;
        }
        else
        {
            return 0;
        }

    }

    #region Override

    public override void ResetNewTurn()
    {
        curMOV = curMaxMOV;
        TurnBuffDecrease();
    }

    public override void InvokeDead()
    {
        EventCenter.Instance.EventTrigger("BattleFoeDead", exp);
    }

    #endregion

    #region Basic Attribute
    public override int curATK
    {
        get
        {
            int tempATK = 0;
            tempATK += item.ATK;
            tempATK += buffATK;
            if (tempATK < 0)
            {
                tempATK = 0;
            }
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
            if (tempDEF < 0)
            {
                tempDEF = 0;
            }
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
            if (tempRES < 0)
            {
                tempRES = 0;
            }
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
            if (tempMOV < 0)
            {
                tempMOV = 0;
            }
            return tempMOV;
        }
    }
    #endregion

}
