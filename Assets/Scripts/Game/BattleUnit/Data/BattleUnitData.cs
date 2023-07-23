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

    public virtual float curATK { get; }
    public virtual float curDEF { get; }
    public virtual float curRES { get; }

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
        //Remove the position where there is a unit
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

        //Range Type
        listViewSkill = new List<Vector2Int>(PublicTool.GetTargetCircleRange(posID, skillInfo.range));

        //If not range self
        if (!skillInfo.isRangeSelf)
        {
            listViewSkill.Remove(posID);
        }

        //According to the SkillType to decide the skill radius
        List<Vector2Int> listTemp = new List<Vector2Int>();

        if (skillInfo.isTargetFoe)
        {
            //Get the position of All Foes
            List<Vector2Int> listFoePos = PublicTool.GetGameData().listTempFoePos;
            CheckWhetherSkillContainTarget(listTemp, listFoePos, listViewSkill, skillInfo);
        }

        if (skillInfo.isTargetCharacter)
        {
            List<Vector2Int> listCharacterPos = PublicTool.GetGameData().listTempCharacterPos;
            CheckWhetherSkillContainTarget(listTemp, listCharacterPos, listViewSkill, skillInfo);
        }

        if (skillInfo.isTargetPlant)
        {
            List<Vector2Int> listPlantPos = PublicTool.GetGameData().listTempPlantPos;
            CheckWhetherSkillContainTarget(listTemp, listPlantPos, listViewSkill, skillInfo);
        }

        listValidSkill = listTemp;
    }

    private void CheckWhetherSkillContainTarget(List<Vector2Int> listStore, List<Vector2Int> listTarget, List<Vector2Int> listToCheck,SkillBattleInfo skillInfo)
    {
        foreach (Vector2Int viewPos in listToCheck)
        {
            //Get the radius of each view pos
            List<Vector2Int> listRadius = new List<Vector2Int>();
            switch (skillInfo.regionType)
            {
                case SkillRegionType.Circle:
                    listRadius = PublicTool.GetTargetCircleRange(viewPos, skillInfo.radius);
                    break;
                case SkillRegionType.Square:
                    listRadius = PublicTool.GetTargetSquareRange(viewPos, skillInfo.radius);
                    break;
            }
            for (int i = 0; i < listRadius.Count; i++)
            {
                if (listTarget.Contains(listRadius[i]) && !listStore.Contains(viewPos))
                {
                    listStore.Add(viewPos);
                    break;
                }
            }
        }
    }



    public void RefreshAttackRange()
    {
        //I will write it later
    }

    #endregion

    #region About Battle Action

    public bool isDead = false;

    public virtual void ResetNewTurn() { }

    public void GetHurt(float damage)
    {
        curHP -= damage;
        if (curHP <= 0)
        {
            isDead = true;
        }
    }

    public void GetHeal(float healPoint)
    {
        curHP += healPoint;
        if(curHP >= maxHP)
        {
            curHP = maxHP;
        }
    }

    #endregion

    #region BattleTextQueue

    private Queue<BattleTextInfo> queueBattleText = new Queue<BattleTextInfo>();

    public void ClearBattleTextQueue()
    {
        queueBattleText.Clear();
    }

    public void EnqueueBattleText(BattleTextInfo info)
    {
        queueBattleText.Enqueue(info);
    }

    public Queue<BattleTextInfo> GetQueueBattleText()
    {
        return queueBattleText;
    }
    #endregion
}
