using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFoeData : BattleUnitData
{
    public BattleFoeData(int typeID,int keyID)
    {
        //Basic Setting
        this.typeID = typeID;
        this.keyID = keyID;
        this.battleUnitType = BattleUnitType.Foe;

    }
}
