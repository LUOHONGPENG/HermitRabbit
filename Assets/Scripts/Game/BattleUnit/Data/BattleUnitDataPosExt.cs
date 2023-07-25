using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleUnitData
{
    /// <summary>
    /// Pos Data
    /// </summary>
    public Vector2Int posID = new Vector2Int(0, 0);
    //Store the valid Move Path
    public Dictionary<Vector2Int, List<Vector2Int>> dicValidMovePath = new Dictionary<Vector2Int, List<Vector2Int>>();

    public Dictionary<Vector2Int, FindPathNode> dicValidMoveNode = new Dictionary<Vector2Int, FindPathNode>();

    //Store the skill range display to the character
    public List<Vector2Int> listViewSkill = new List<Vector2Int>();
    //Store the valid pos that the player can choose
    public List<Vector2Int> listValidSkill = new List<Vector2Int>();

    public List<Vector2Int> listValidRange = new List<Vector2Int>();



    #region CharacterMoveBFS

/*    public void RefreshValidCharacterMoveBFSNode()
    {
        GameData gameData = PublicTool.GetGameData();
        gameData.ResetFindPathNode();

        dicValidMoveNode.Clear();

        List<Vector2Int> listFoe = PublicTool.GetGameData().listTempFoePos;

        Queue<FindPathNode> ququeOpen = new Queue<FindPathNode>();
        Dictionary<Vector2Int, FindPathNode> dicClose = new Dictionary<Vector2Int, FindPathNode>();

        ququeOpen.Enqueue(gameData.GetFindPathNode(posID));
        while (ququeOpen.Count > 0)
        {
            FindPathNode tarNode = ququeOpen.Dequeue();



        }


    }*/

    public void RefreshValidCharacterMoveBFS()
    {
        dicValidMovePath.Clear();
        Dictionary<Vector2Int, Vector2Int> dicParentChild = new Dictionary<Vector2Int, Vector2Int>();
        List<Vector2Int> listFoe = PublicTool.GetGameData().listTempFoePos;
        Queue<Vector2Int> ququeOpen = new Queue<Vector2Int>();
        ququeOpen.Enqueue(posID);
        while (ququeOpen.Count > 0)
        {
            Vector2Int tarPos = ququeOpen.Dequeue();
            List<Vector2Int> path;
            if (dicParentChild.ContainsKey(tarPos))
            {
                Vector2Int parent = dicParentChild[tarPos];
                if (dicValidMovePath.ContainsKey(parent))
                {
                    path = new List<Vector2Int>(dicValidMovePath[parent]);
                }
                else
                {
                    Debug.LogError("!!!!");
                    path = new List<Vector2Int>();
                }
            }
            else
            {
                path = new List<Vector2Int>();
            }

            if (path.Count > curMOV)
            {
                continue;
            }
            path.Add(tarPos);
            dicValidMovePath.Add(tarPos, path);

            //Next Four
            List<Vector2Int> listNear = PublicTool.GetNearPos(tarPos);
            for (int i = 0; i < listNear.Count; i++)
            {
                if (dicParentChild.ContainsKey(listNear[i]))
                {
                    continue;
                }
                else if (dicValidMovePath.ContainsKey(listNear[i]))
                {
                    continue;
                }
                else if (listFoe.Contains(listNear[i]))
                {
                    continue;
                }
                dicParentChild.Add(listNear[i], tarPos);
                ququeOpen.Enqueue(listNear[i]);
            }
        }

        foreach (Vector2Int tarPos in PublicTool.GetGameData().listTempAllPos)
        {
            if (dicValidMovePath.ContainsKey(tarPos))
            {
                dicValidMovePath.Remove(tarPos);
            }
        }
    }

    #endregion




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

    private void CheckWhetherSkillContainTarget(List<Vector2Int> listStore, List<Vector2Int> listTarget, List<Vector2Int> listToCheck, SkillBattleInfo skillInfo)
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



}
