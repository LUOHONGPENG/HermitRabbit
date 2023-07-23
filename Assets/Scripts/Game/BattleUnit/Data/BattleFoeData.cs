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

    public CharacterSkillExcelItem GetSkillItem()
    {
        return PublicTool.GetSkillItem(item.skillID);
    }

    public new float curATK 
    {
        get
        {
            return item.ATK;
        }
    }

    public new float curDEF
    {
        get
        {
            return item.DEF;
        }
    }

    public new float curRES
    {
        get
        {
            return item.RES;
        }
    }
}
