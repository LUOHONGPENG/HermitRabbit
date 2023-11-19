using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        int centerX = (GameGlobal.mapClipSize * GameGlobal.mapClipNumX - 1) / 2;
        int centerZ = (GameGlobal.mapClipSize * GameGlobal.mapClipNumY + GameGlobal.mapRowFoe + GameGlobal.mapRowFriend - 1) / 2;
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

    public static List<Vector2Int> GetTargetLineRange(Vector2Int targetPos, Vector2Int sourcePos, int Range,int Radius)
    {
        List<Vector2Int> listTarget = new List<Vector2Int>();
        Vector2Int direction = targetPos - sourcePos;
        Vector2Int verticalDir = new Vector2Int(direction.y, direction.x);
        if (Range > 0)
        {
            for (int i = 1; i <= Range; i++)
            {
                Vector2Int thisPos = sourcePos + direction * i;
                //listRange.Add(thisPos);
                for(int j = -Radius; j <= Radius; j++)
                {
                    listTarget.Add(thisPos + verticalDir * j);
                }
            }
        }
        return listTarget;
    }

    public static List<Vector2Int> GetTargetWaterRange(Vector2Int targetPos)
    {
        List<Vector2Int> listRange = new List<Vector2Int>();
        Queue<Vector2Int> listOpen = new Queue<Vector2Int>();
        List<Vector2Int> listClose = new List<Vector2Int>();

        listOpen.Enqueue(targetPos);
        listRange.Add(targetPos);

        GameData gameData = GetGameData();

        while (listOpen.Count > 0)
        {
            Vector2Int tarPos = listOpen.Dequeue();
            if (gameData.GetMapTileData(tarPos)!=null && !listClose.Contains(tarPos))
            {
                listClose.Add(tarPos);
                MapTileData mapTileData = gameData.GetMapTileData(tarPos);

                if(mapTileData.curMapTileStatus == MapTileStatus.Wet)
                {
                    if (!listRange.Contains(tarPos))
                    {
                        listRange.Add(tarPos);
                    }
                    listOpen.Enqueue(tarPos + new Vector2Int(0, 1));
                    listOpen.Enqueue(tarPos + new Vector2Int(0, -1));
                    listOpen.Enqueue(tarPos + new Vector2Int(1, 0));
                    listOpen.Enqueue(tarPos + new Vector2Int(-1, 0));

                }

            }
        }

        return listRange;
    }

    public static List<Vector2Int> GetTargetBurningRange()
    {
        List<Vector2Int> listRange = new List<Vector2Int>();
        GameData gameData = GetGameData();
        foreach(var foe in gameData.listFoe)
        {
            if (foe.CheckBuffExist(4001))
            {
                listRange.Add(foe.posID);
            }
        }

        foreach (var plant in gameData.listPlant)
        {
            if (plant.CheckBuffExist(4001))
            {
                listRange.Add(plant.posID);
            }
        }

        foreach (var character in gameData.listCharacter)
        {
            if (character.CheckBuffExist(4001))
            {
                listRange.Add(character.posID);
            }
        }
        return listRange;
    }

    public static List<Vector2Int> GetTargetBurningRadius(List<Vector2Int> listTargetPos,int radius)
    {
        List<Vector2Int> listRange = new List<Vector2Int>();
        GameData gameData = GetGameData();

        foreach(Vector2Int pos in listTargetPos)
        {
            List<Vector2Int> listPos = GetTargetCircleRange(pos, radius);
            for(int i = 0; i < listPos.Count; i++)
            {
                if (!listRange.Contains(listPos[i]))
                {
                    listRange.Add(listPos[i]);
                }
            }
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

    public static void FoeFindTargetInfoSortHighestHate(List<FoeFindTargetInfo> list)
    {
        list.Sort((y, x) => { return x.TotalHate.CompareTo(y.TotalHate); });
    }

    #endregion

    #region MapClip

    public static Vector2Int ConvertMapTileIDToClip(Vector2Int posIDTile)
    {
        int upLimitNum = GameGlobal.mapRowFriend + GameGlobal.mapClipSize * GameGlobal.mapClipNumY;
        if (posIDTile.y < GameGlobal.mapRowFriend)
        {
            return new Vector2Int(-1, -1);
        }
        else if(posIDTile.y >= GameGlobal.mapRowFriend && posIDTile.y < upLimitNum)
        {
            int tempX = posIDTile.x / GameGlobal.mapClipSize;
            int tempY = (posIDTile.y - GameGlobal.mapRowFriend) / GameGlobal.mapClipSize;
            return new Vector2Int(tempX, tempY);
        }
        else
        {
            return new Vector2Int(-1, -1);
        }
    }

    public static Vector2Int ConvertMapClipToTile(Vector2Int posIDClip)
    {
        int tempX = posIDClip.x * GameGlobal.mapClipSize;
        int tempY = posIDClip.y * GameGlobal.mapClipSize + GameGlobal.mapRowFriend;

        return new Vector2Int(tempX, tempY);
    }


    public static List<Vector2Int> GetEmptyPosFromRowRange(int rowLow,int rowHigh)
    {
        List<Vector2Int> listTemp = new List<Vector2Int>();
        List<Vector2Int> listEmpty = GetGameData().listTempEmptyPos;
        for (int i = 0; i < listEmpty.Count; i++)
        {
            if (listEmpty[i].y <= rowHigh && listEmpty[i].y >= rowLow)
            {
                listTemp.Add(listEmpty[i]);
            }
        }
        return listTemp;
    }
    #endregion
}
