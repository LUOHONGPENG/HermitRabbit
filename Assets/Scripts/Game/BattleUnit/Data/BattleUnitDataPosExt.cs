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

    //Store the skill range display to the character
    public List<Vector2Int> listViewSkill = new List<Vector2Int>();
    //Store the valid pos that the player can choose
    public List<Vector2Int> listValidSkill = new List<Vector2Int>();

    public List<Vector2Int> listValidRange = new List<Vector2Int>();


    #region FindPath

    public void RefreshValidCharacterMoveBFSNode()
    {
        //Init 
        Dictionary<Vector2Int, FindPathNode> dicPathNode = new Dictionary<Vector2Int, FindPathNode>();
        List<MapTileData> listMap = PublicTool.GetGameData().listMapTile;
        for(int i = 0;i < listMap.Count; i++)
        {
            dicPathNode.Add(listMap[i].posID, new FindPathNode(listMap[i].posID));
        }

        dicValidMoveNode.Clear();

        List<Vector2Int> listBlock = PublicTool.GetGameData().listTempFoePos;

        Queue<FindPathNode> ququeOpen = new Queue<FindPathNode>();
        Dictionary<Vector2Int, FindPathNode> dicClose = new Dictionary<Vector2Int, FindPathNode>();
        dicClose.Clear();

        FindPathNode startNode = GetFindPathNode(dicPathNode,posID);
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

            dicClose.Add(tarNode.pos, tarNode);

            if (tarNode.gCost > curMOV)
            {
                Debug.Log(tarNode.gCost);
                continue;
            }
            tarNode.path.Add(tarNode.pos);
            dicValidMoveNode.Add(tarNode.pos,tarNode);

            List<FindPathNode> listNearNode = GetNearFindPathNode(dicPathNode, tarNode.pos);
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
    }

    #endregion


    #region FindPathSupporter


    public FindPathNode GetFindPathNode(Dictionary<Vector2Int, FindPathNode> dic, Vector2Int tarPos)
    {
        if (dic.ContainsKey(tarPos))
        {
            return dic[tarPos];
        }
        else
        {
            return null;
        }
    }

    public List<FindPathNode> GetNearFindPathNode(Dictionary<Vector2Int, FindPathNode> dic, Vector2Int tarPos)
    {
        List<FindPathNode> listPathNode = new List<FindPathNode>();

        FindPathNode node1 = GetFindPathNode(dic, tarPos + new Vector2Int(0, 1));
        FindPathNode node2 = GetFindPathNode(dic, tarPos + new Vector2Int(1, 0));
        FindPathNode node3 = GetFindPathNode(dic, tarPos + new Vector2Int(0, -1));
        FindPathNode node4 = GetFindPathNode(dic, tarPos + new Vector2Int(-1, 0));
        if (node1 != null)
        {
            listPathNode.Add(node1);
        }
        if (node2 != null)
        {
            listPathNode.Add(node2);
        }
        if (node3 != null)
        {
            listPathNode.Add(node3);
        }
        if (node4 != null)
        {
            listPathNode.Add(node4);
        }
        return listPathNode;
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
