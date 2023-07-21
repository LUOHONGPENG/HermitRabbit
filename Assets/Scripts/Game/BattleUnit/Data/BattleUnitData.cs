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

    public int curAP = 1;
    public int maxAP = 1;

    public float curATK { get; }
    public float curDEF { get; }
    public float curRES { get; }

    public int GetTypeID()
    {
        return typeID;
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
        SkillBattleInfo skillInfo = PublicTool.GetGameData().GetCurSkillBattleInfo();
        listViewSkill = new List<Vector2Int>(PublicTool.GetTargetCircleRange(posID, skillInfo.range));

        //According to the SkillType to decide the skill radius
        List<Vector2Int> listTemp = new List<Vector2Int>();

        if (skillInfo.isTargetFoe)
        {
            //Get the position of All Foes
            List<Vector2Int> listFoePos = PublicTool.GetGameData().listTempFoePos;
            foreach (Vector2Int viewPos in listViewSkill)
            {
                //Get the radius of each view pos
                List<Vector2Int> listRadius = new List<Vector2Int>();
                switch (skillInfo.regionType)
                {
                    case SkillRegionType.Circle:
                        listRadius = PublicTool.GetTargetCircleRange(viewPos, skillInfo.radius);
                        break;
                }
                for(int i = 0; i < listRadius.Count; i++)
                {
                    if (listFoePos.Contains(listRadius[i]))
                    {
                        listTemp.Add(viewPos);
                        Debug.Log(viewPos);
                        break;
                    }
                }
            }
        }

        //if(listTemp contains viewPos) continue;

        listValidSkill = listTemp;
    }

    public void RefreshAttackRange()
    {
        //I will write it later
    }

    #endregion

    #region About Battle Action

    public bool isDead = false;

    public void GetHurt(float damage)
    {
        curHP -= damage;
        if (curHP <= 0)
        {
            isDead = true;
        }
    }

    #endregion
}
