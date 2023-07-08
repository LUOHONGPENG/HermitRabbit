using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacterData : BattleUnitData
{
    public int STR;
    public int CON;
    public int INT;
    public int WIS;

    public BattleCharacterData(int ID)
    {
        //Basic Setting
        this.typeID = ID;
        this.battleUnitType = BattleUnitType.Character;
        CharacterExcelItem characterData = ExcelDataMgr.Instance.characterExcelData.GetExcelItem(ID);
        //Attribute Setting
        STR = characterData.STR;
        CON = characterData.CON;
        INT = characterData.INT;
        WIS = characterData.WIS;
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
