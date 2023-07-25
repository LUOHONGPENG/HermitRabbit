using System.Collections;
using System.Collections.Generic;
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
        CharacterSkillExcelItem skillItem = PublicTool.GetSkillItem(GetSkillID());
        if (skillItem != null)
        {
            return skillItem.range + skillItem.radius;
        }
        else
        {
            return 0;
        }

    }

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
}
