using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitData
{
    #region About Data Type
    /// <summary>
    ///  Basic Data
    /// </summary>
    public BattleUnitType battleUnitType;
    public int typeID = -1;
    public int keyID = -1;
    #endregion


    //The current HP of this unit
    public float curHP;
    //The maximum HP of this unit
    public float maxHP { get; }

    public float curATK { get; }
    public float curDEF { get; }
    public float curRES { get; }

    public int curMOV = 0;
    public int maxMOV = 0;

    public int curSP = 1;
    public int maxSP = 1;

    public int GetTypeID()
    {
        return typeID;
    }

    public void GetHurt(float damage)
    {
        curHP -= damage;
    }

    public UnitInfo GetUnitInfo()
    {
        return new UnitInfo(battleUnitType, keyID);
    }

    #region About Position
    /// <summary>
    /// Pos Data
    /// </summary>
    public Vector2Int posID = new Vector2Int(0, 0);

    public List<Vector2Int> listValidMove = new List<Vector2Int>();
    public List<Vector2Int> listValidRange = new List<Vector2Int>();

    public void RefreshValidMove()
    {
        listValidMove = new List<Vector2Int>(PublicTool.GetTargetCrossRange(posID, curMOV));
        listValidMove.Remove(posID);
    }

    public void RefreshValidRange()
    {
        //I will write it later
    }

    #endregion
}
