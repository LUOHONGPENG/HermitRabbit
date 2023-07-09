using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacterData : BattleUnitData
{
    public int STR;
    public int CON;
    public int INT;
    public int WIS;

    private CharacterExcelItem item;

    public BattleCharacterData(int ID)
    {
        //Basic Setting
        this.typeID = ID;
        this.battleUnitType = BattleUnitType.Character;
        item = ExcelDataMgr.Instance.characterExcelData.GetExcelItem(ID);
        //Attribute Setting
        STR = item.STR;
        CON = item.CON;
        INT = item.INT;
        WIS = item.WIS;
    }

    public CharacterExcelItem GetItem()
    {
        return item;
    }

    public new int curATK
    {
        get
        {
            return STR;
        }
    }

    public new int curDEF
    {
        get
        {
            return CON;
        }
    }

    public new int curRES
    {
        get
        {
            return WIS;
        }
    }
}
