using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitData
{
    public BattleUnitType battleUnitType;
    public int typeID = -1;

    //The current HP of this unit
    public float curHP;
    //The maximum HP of this unit
    public float maxHP { get; }

    public float curATK { get; }
    public float curDEF { get; }
    public float curRES { get; }

    public int GetTypeID()
    {
        return typeID;
    }

    public void GetHurt(float damage)
    {
        curHP -= damage;
    }
}
