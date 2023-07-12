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

    public BattleCharacterData(int typeID)
    {
        //Basic Setting
        this.typeID = typeID;
        this.battleUnitType = BattleUnitType.Character;
        item = ExcelDataMgr.Instance.characterExcelData.GetExcelItem(typeID);
        //Pos Setting
        posID = new Vector2Int(item.startPos[0], item.startPos[1]);
        //Attribute Setting
        STR = item.STR;
        CON = item.CON;
        INT = item.INT;
        WIS = item.WIS;
        curMOV = item.MOV;
        maxMOV = item.MOV;
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
