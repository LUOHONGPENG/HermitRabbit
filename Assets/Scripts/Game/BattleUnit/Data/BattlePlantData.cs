using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlantData : BattleUnitData
{
    public BattlePlantData(int typeID,int keyID)
    {
        //Basic Setting
        this.typeID = typeID;
        this.keyID = keyID;
        this.battleUnitType = BattleUnitType.Plant;

    }
}
