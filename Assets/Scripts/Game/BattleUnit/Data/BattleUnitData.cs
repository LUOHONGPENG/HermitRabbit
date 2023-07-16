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
    public float maxHP;

    public int curMOV = 0;
    public int maxMOV = 0;

    public int curSP = 1;
    public int maxSP = 1;

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

    public UnitInfo GetUnitInfo()
    {
        return new UnitInfo(battleUnitType, keyID);
    }

    #region About Position
    /// <summary>
    /// Pos Data
    /// </summary>
    public Vector2Int posID = new Vector2Int(0, 0);
    //Store the valid Move 
    public List<Vector2Int> listValidMove = new List<Vector2Int>();
    //Store the skill range display to the character
    public List<Vector2Int> listViewSkill = new List<Vector2Int>();
    //Store the valid pos that the player can choose
    public List<Vector2Int> listValidSkill = new List<Vector2Int>();

    public List<Vector2Int> listValidRange = new List<Vector2Int>();

    public void RefreshValidMove()
    {
        listValidMove = new List<Vector2Int>(PublicTool.GetTargetCircleRange(posID, curMOV));
        foreach(var pos in PublicTool.GetGameData().listTempAllPos)
        {
            if (listValidMove.Contains(pos))
            {
                listValidMove.Remove(pos);
            }
        }
    }

    public void RefreshValidSkill()
    {
        SkillMapInfo skillMapInfo = PublicTool.GetGameData().GetCurSkillMapInfo();
        listViewSkill = new List<Vector2Int>(PublicTool.GetTargetCircleRange(posID, skillMapInfo.range));

        //According to the SkillType to decide the skill range
    }

    public void RefreshValidRange()
    {
        //I will write it later
    }

    #endregion
}
