using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitData
{
    /// <summary>
    ///  Basic Data
    /// </summary>
    public BattleUnitType battleUnitType;
    public int typeID = -1;
    public int keyID = -1;

    /// <summary>
    /// Pos Data
    /// </summary>
    public Vector2Int posID = new Vector2Int(0, 0);

    //The current HP of this unit
    public float curHP;
    //The maximum HP of this unit
    public float maxHP { get; }

    public float curATK { get; }
    public float curDEF { get; }
    public float curRES { get; }

    public int curMOV = 0;
    public int maxMOV = 0;

    public int GetTypeID()
    {
        return typeID;
    }

    public void GetHurt(float damage)
    {
        curHP -= damage;
    }
}
