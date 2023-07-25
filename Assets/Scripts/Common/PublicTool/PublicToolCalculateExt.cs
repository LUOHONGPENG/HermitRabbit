using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{
    /// <summary>
    /// Translate the PosID into the world position?
    /// </summary>
    /// <param name="posID"></param>
    /// <returns></returns>
    public static Vector3 ConvertPosFromID(Vector2Int posID)
    {
        int centerX = (GameGlobal.mapSize - 1) / 2;
        int centerZ = (GameGlobal.mapSize - 1) / 2;
        /*        int centerX = 0;
                int centerZ = 0;*/

        return new Vector3(posID.x - centerX,0 ,posID.y - centerZ);
    }

    /// <summary>
    /// Calculate the Distance between two posID
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static int CalculateGlobalDis(Vector2Int start,Vector2Int end)
    {
        int disX = Mathf.Abs(start.x - end.x);
        int disY = Mathf.Abs(start.y - end.y);
        return disX + disY;
    }

    #region CalculateRange
    public static List<Vector2Int> GetTargetCircleRange(Vector2Int targetPos, int Range)
    {
        List<Vector2Int> listRange = new List<Vector2Int>();
        if (Range > 0)
        {
            for (int i = -Range; i <= Range; i++)
            {
                for (int j = -Range; j <= Range; j++)
                {
                    Vector2Int thisPos = new Vector2Int(targetPos.x + i, targetPos.y + j);
                    if (PublicTool.CalculateGlobalDis(thisPos, targetPos) <= Range)
                    {
                        listRange.Add(thisPos);
                    }
                }
            }
        }
        else
        {
            listRange.Add(targetPos);
        }
        return listRange;
    }

    public static List<Vector2Int> GetTargetSquareRange(Vector2Int targetPos,int Range)
    {
        List<Vector2Int> listRange = new List<Vector2Int>();
        if(Range > 0)
        {
            for (int i = -Range; i <= Range; i++)
            {
                for (int j = -Range; j <= Range; j++)
                {
                    Vector2Int thisPos = new Vector2Int(targetPos.x + i, targetPos.y + j);
                    listRange.Add(thisPos);
                }
            }
        }
        else
        {
            listRange.Add(targetPos);
        }
        return listRange;
    }

    #endregion

    #region FindPathSupporter


    public static FindPathNode GetFindPathNode(Dictionary<Vector2Int, FindPathNode> dic, Vector2Int tarPos)
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

    public static List<FindPathNode> GetNearFindPathNode(Dictionary<Vector2Int, FindPathNode> dic, Vector2Int tarPos)
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

    public static void FindPathNodeSortLowestGCost(List<FindPathNode> list)
    {
        list.Sort((x,y)=> {return x.gCost.CompareTo(y.gCost); });
    }

    public static void FindPathNodeSortLowestHCost(List<FindPathNode> list)
    {
        list.Sort((x, y) => { return x.hCostReal.CompareTo(y.hCostReal); });
    }

    #endregion
}
