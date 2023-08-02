using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleUnitData
{
    /// <summary>
    /// Pos Data
    /// </summary>
    public Vector2Int posID = new Vector2Int(0, 0);

    public Dictionary<Vector2Int, FindPathNode> dicValidMoveNode = new Dictionary<Vector2Int, FindPathNode>();
    public Dictionary<Vector2Int, FindPathNode> dicBFSAllNode = new Dictionary<Vector2Int, FindPathNode>();

    //Store the skill range display to the character
    public List<Vector2Int> listViewSkill = new List<Vector2Int>();
    //Store the valid pos that the player can choose
    public List<Vector2Int> listValidSkill = new List<Vector2Int>();

    public List<Vector2Int> listValidRange = new List<Vector2Int>();


    #region FindPathForMove

    public void RefreshValidCharacterMoveBFSNode()
    {
        //Init the virtual Grid
        Dictionary<Vector2Int, FindPathNode> dicPathNode = new Dictionary<Vector2Int, FindPathNode>();
        List<MapTileData> listMap = PublicTool.GetGameData().listMapTile;
        for(int i = 0;i < listMap.Count; i++)
        {
            dicPathNode.Add(listMap[i].posID, new FindPathNode(listMap[i].posID));
        }

        //Clear Valid Node
        dicValidMoveNode.Clear();

        //Get Block Pos
        List<Vector2Int> listBlock = PublicTool.GetGameData().listTempFoePos;

        //Prepare the container for search
        Queue<FindPathNode> ququeOpen = new Queue<FindPathNode>();
        Dictionary<Vector2Int, FindPathNode> dicClose = new Dictionary<Vector2Int, FindPathNode>();
        dicClose.Clear();

        //Start searching
        FindPathNode startNode = PublicTool.GetFindPathNode(dicPathNode,posID);
        ququeOpen.Enqueue(startNode);
        while (ququeOpen.Count > 0)
        {
            FindPathNode tarNode = ququeOpen.Dequeue();
            if (tarNode.parentNode != null)
            {
                FindPathNode parentNode = tarNode.parentNode;
                tarNode.path = new List<Vector2Int>(parentNode.path);
                tarNode.gCost = parentNode.gCost + 1;
            }
            else
            {
                tarNode.path = new List<Vector2Int>();
                tarNode.gCost = 0;
            }

            //No matter whether it is valid
            dicClose.Add(tarNode.pos, tarNode);

            if (tarNode.gCost > curMOV)
            {
                continue;
            }
            tarNode.path.Add(tarNode.pos);
            dicValidMoveNode.Add(tarNode.pos,tarNode);

            List<FindPathNode> listNearNode = PublicTool.GetNearFindPathNode(dicPathNode, tarNode.pos);
            for(int i = 0; i < listNearNode.Count; i++)
            {
                FindPathNode nextNode = listNearNode[i];
                if (dicClose.ContainsKey(nextNode.pos))
                {
                    continue;
                }
                if (ququeOpen.Contains(nextNode))
                {
                    continue;
                }
                if (listBlock.Contains(nextNode.pos))
                {
                    continue;
                }
                nextNode.parentNode = tarNode;
                
                ququeOpen.Enqueue(nextNode);
            }
        }

        foreach (Vector2Int tarPos in PublicTool.GetGameData().listTempAllPos)
        {
            if (dicValidMoveNode.ContainsKey(tarPos))
            {
                dicValidMoveNode.Remove(tarPos);
            }
        }
    }


    public void RefreshFoeMoveAllBFSNode()
    {
        //Init the virtual Grid
        Dictionary<Vector2Int, FindPathNode> dicPathNode = new Dictionary<Vector2Int, FindPathNode>();
        List<MapTileData> listMap = PublicTool.GetGameData().listMapTile;
        for (int i = 0; i < listMap.Count; i++)
        {
            dicPathNode.Add(listMap[i].posID, new FindPathNode(listMap[i].posID));
        }

        //Clear Valid Node
        dicBFSAllNode.Clear();
        dicValidMoveNode.Clear();

        //GetBlockPos
        List<Vector2Int> listBlockTarget = new List<Vector2Int>();
        foreach(Vector2Int blockPos in PublicTool.GetGameData().listTempCharacterPos)
        {
            if (!listBlockTarget.Contains(blockPos))
            {
                listBlockTarget.Add(blockPos);
            }
        }
        foreach (Vector2Int blockPos in PublicTool.GetGameData().listTempPlantPos)
        {
            if (!listBlockTarget.Contains(blockPos))
            {
                listBlockTarget.Add(blockPos);
            }
        }

        //Prepare the container for search
        Queue<FindPathNode> ququeOpen = new Queue<FindPathNode>();
        Dictionary<Vector2Int, FindPathNode> dicClose = new Dictionary<Vector2Int, FindPathNode>();
        dicClose.Clear();

        //Start searching
        FindPathNode startNode = PublicTool.GetFindPathNode(dicPathNode, posID);
        ququeOpen.Enqueue(startNode);
        while (ququeOpen.Count > 0)
        {
            FindPathNode tarNode = ququeOpen.Dequeue();
            //Calculate gCost or the move
            if (tarNode.parentNode != null)
            {
                FindPathNode parentNode = tarNode.parentNode;
                tarNode.path = new List<Vector2Int>(parentNode.path);
                tarNode.gCost = parentNode.gCost + 1;
            }
            else
            {
                tarNode.path = new List<Vector2Int>();
                tarNode.gCost = 0;
            }

            dicClose.Add(tarNode.pos, tarNode);
            dicBFSAllNode.Add(tarNode.pos, tarNode);
            if (listBlockTarget.Contains(tarNode.pos))
            {
                continue;
            }

            //Read and deal with cost and remember node
            if (tarNode.gCost <= curMOV)
            {
                tarNode.path.Add(tarNode.pos);
                dicValidMoveNode.Add(tarNode.pos, tarNode);
            }

            List<FindPathNode> listNearNode = PublicTool.GetNearFindPathNode(dicPathNode, tarNode.pos);
            for (int i = 0; i < listNearNode.Count; i++)
            {
                FindPathNode nextNode = listNearNode[i];
                if (dicClose.ContainsKey(nextNode.pos))
                {
                    continue;
                }
                if (ququeOpen.Contains(nextNode))
                {
                    continue;
                }
                nextNode.parentNode = tarNode;

                ququeOpen.Enqueue(nextNode);
            }
        }

        foreach (Vector2Int tarPos in PublicTool.GetGameData().listTempAllPos)
        {
            if (dicValidMoveNode.ContainsKey(tarPos))
            {
                dicValidMoveNode.Remove(tarPos);
            }
        }
    }

    public void RefreshDistanceFromAimNode(Vector2Int aimPos,int touchRange)
    {
        //GetBlockPos
        List<Vector2Int> listBlock = new List<Vector2Int>();
        foreach (Vector2Int blockPos in PublicTool.GetGameData().listTempCharacterPos)
        {
            if (!listBlock.Contains(blockPos))
            {
                listBlock.Add(blockPos);
            }
        }
        foreach (Vector2Int blockPos in PublicTool.GetGameData().listTempPlantPos)
        {
            if (!listBlock.Contains(blockPos))
            {
                listBlock.Add(blockPos);
            }
        }

        //GetFoePos
        List<Vector2Int> listTeam = new List<Vector2Int>();
        foreach (Vector2Int TeamPos in PublicTool.GetGameData().listTempFoePos)
        {
            if (TeamPos!=posID)
            {
                listTeam.Add(TeamPos);
            }
        }

        //Prepare the container for search
        Queue<FindPathNode> ququeOpen = new Queue<FindPathNode>();
        Dictionary<Vector2Int, FindPathNode> dicClose = new Dictionary<Vector2Int, FindPathNode>();

        //StartSearching
        FindPathNode startNode = PublicTool.GetFindPathNode(dicBFSAllNode, aimPos);
        ququeOpen.Enqueue(startNode);
        while (ququeOpen.Count > 0)
        {
            FindPathNode tarNode = ququeOpen.Dequeue();
            FindPathNode validNode = new FindPathNode(new Vector2Int(-1,-1));
            bool haveValidNode = false;
            if(PublicTool.GetFindPathNode(dicValidMoveNode, tarNode.pos) != null)
            {
                haveValidNode = true;
                validNode = PublicTool.GetFindPathNode(dicValidMoveNode, tarNode.pos);
            }
            //Calculate hCost
            if (PublicTool.CalculateGlobalDis(tarNode.pos, aimPos) <= touchRange)
            {
                if (listTeam.Contains(tarNode.pos))
                {
                    tarNode.hCostReal = 1;
                }
                else
                {
                    tarNode.hCostReal = 0;
                }
            }
            else
            {
                if (tarNode.hParentNode != null)
                {
                    FindPathNode hParentNode = tarNode.hParentNode;
                    tarNode.hCostReal = hParentNode.hCostReal + 1;
                }
                else
                {
                    tarNode.hCostReal = 0;
                }
            }




            if (haveValidNode)
            {
                validNode.hCostReal = tarNode.hCostReal;
            }

            dicClose.Add(tarNode.pos, tarNode);

            List<FindPathNode> listNearNode = PublicTool.GetNearFindPathNode(dicBFSAllNode, tarNode.pos);
            for (int i = 0; i < listNearNode.Count; i++)
            {
                FindPathNode nextNode = listNearNode[i];

                if (dicClose.ContainsKey(nextNode.pos))
                {
                    continue;
                }
                if (ququeOpen.Contains(nextNode))
                {
                    if(nextNode.hParentNode.hCostReal < tarNode.hCostReal)
                    {
                        nextNode.hParentNode = tarNode;
                    }
                    continue;
                }
                if (listBlock.Contains(nextNode.pos))
                {
                    continue;
                }
                nextNode.hParentNode = tarNode;

                ququeOpen.Enqueue(nextNode);
            }
        }
    }

    #endregion


    public void RefreshValidSkill()
    {
        SkillBattleInfo skillInfo = PublicTool.GetGameData().GetCurSkillBattleInfo();

        if (skillInfo.ID == 1302 && BattleMgr.Instance.isInExtraTargetMode)
        {
            //Range Type
            listViewSkill = new List<Vector2Int>(PublicTool.GetTargetCircleRange(posID, maxMOV));

            listViewSkill.Remove(posID);
            listViewSkill.Remove(BattleMgr.Instance.skillTargetPos);

            List<Vector2Int> listTemp = new List<Vector2Int>();
            List<Vector2Int> listEmptyPos = PublicTool.GetGameData().listTempEmptyPos;
            CheckWhetherSkillContainTarget(listTemp, listEmptyPos, listViewSkill, skillInfo);

            listValidSkill = listTemp;
        }
        else
        {
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
