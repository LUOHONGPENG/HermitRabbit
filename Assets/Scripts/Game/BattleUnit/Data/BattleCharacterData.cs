using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacterData : BattleUnitData
{
    private int characterID = -1;

    public int STR;
    public int CON;
    public int INT;
    public int WIS;

    public BattleCharacterData(int ID)
    {
        this.characterID = ID;
        CharacterDataExcelItem characterData = DataMgr.Instance.characterDataExcelData.GetExcelItem(ID);

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
