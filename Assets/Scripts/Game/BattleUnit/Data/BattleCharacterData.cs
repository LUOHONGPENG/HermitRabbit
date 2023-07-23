using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacterData : BattleUnitData
{
    //Data
    public int STR;
    public int CON;
    public int INT;
    public int WIS;

    private CharacterExcelItem item;

    public BattleCharacterData(int typeID)
    {
        //Basic Setting
        this.typeID = typeID;
        this.keyID = typeID;
        this.battleUnitType = BattleUnitType.Character;
        item = ExcelDataMgr.Instance.characterExcelData.GetExcelItem(typeID);
        //Pos Setting
        posID = new Vector2Int(item.startPos[0], item.startPos[1]);
        //Attribute Setting
        curHP = item.HP;
        maxHP = item.HP;
        curAP = item.AP;
        maxAP = item.AP;
        curMOV = item.MOV;
        maxMOV = item.MOV;

        STR = item.STR;
        CON = item.CON;
        INT = item.INT;
        WIS = item.WIS;

    }

    public CharacterExcelItem GetItem()
    {
        return item;
    }

    public override int curATK
    {
        get
        {
            if(typeID == 1001)
            {
                return INT;
            }
            else
            {
                return STR;
            }
        }
    }

    public override int curDEF
    {
        get
        {
            return CON;
        }
    }

    public override int curRES
    {
        get
        {
            return WIS;
        }
    }

    public override void ResetNewTurn()
    {
        curAP = maxAP;
        curMOV = maxMOV;
    }
}
