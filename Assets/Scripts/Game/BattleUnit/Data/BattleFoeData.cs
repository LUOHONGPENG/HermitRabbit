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
        //Buff Go first
        TurnBuffDecrease();
        curMOV = regenMOV;
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
            int temp = item.ATK + buffATK;
            if (temp < 0)
            {
                temp = 0;
            }
            return temp;
        }
    }

    public override int curDEF
    {
        get
        {
            int temp = item.DEF + buffDEF;
            if (temp < 0)
            {
                temp = 0;
            }
            return temp;
        }
    }

    public override int curRES
    {
        get
        {
            int temp = item.RES + buffRES;
            if (temp < 0)
            {
                temp = 0;
            }
            return temp;
        }
    }


    #endregion

}
