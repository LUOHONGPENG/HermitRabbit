using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlantData : BattleUnitData
{
    public BattlePlantData(int ID)
    {
        //Basic Setting
        this.typeID = ID;
        this.battleUnitType = BattleUnitType.Plant;

    }
}
