using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFoeData : BattleUnitData
{
    public BattleFoeData(int ID)
    {
        //Basic Setting
        this.typeID = ID;
        this.battleUnitType = BattleUnitType.Foe;

    }
}
