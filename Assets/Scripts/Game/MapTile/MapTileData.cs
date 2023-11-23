using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public enum MapTileStatus
{
    None,
    CanBurn,
    Burning,
    CanWet,
    Wet,
    CanBreak,
    Broken
}

public class MapTileData
{
    public Vector2Int posID;
    private MapTileType tileType = MapTileType.Normal;
    public MapTileData(Vector2Int posID)
    {
        this.posID = posID;
        SetMapType(MapTileType.Normal);
        ResetTileStatus();
    }

    public void SetMapType(MapTileType tileType)
    {
        this.tileType = tileType;
    }

    public MapTileType GetMapType()
    {
        return tileType;
    }

    #region Status
    public MapTileStatus curMapTileStatus
    {
        get
        {
            switch (tileType)
            {
                case MapTileType.Normal:
                    if (isWet)
                    {
                        return MapTileStatus.Wet;
                    }
                    else
                    {
                        return MapTileStatus.CanWet;
                    }
                case MapTileType.Water:
                    return MapTileStatus.Wet;
                case MapTileType.Grass:
                    if (isBurning)
                    {
                        return MapTileStatus.Burning;
                    }
                    else
                    {
                        return MapTileStatus.CanBurn;
                    }
                case MapTileType.Flower:
                    if (isBurning)
                    {
                        return MapTileStatus.Burning;
                    }
                    else
                    {
                        return MapTileStatus.CanBurn;
                    }
                case MapTileType.Stone:
                    if (isBroken)
                    {
                        return MapTileStatus.Broken;
                    }
                    else
                    {
                        return MapTileStatus.CanBreak;
                    }
                default:
                    return MapTileStatus.None;
            }
        }
    }


    //Special
    public bool isBurning = false;
    public bool isWet = false;
    public bool isBroken = false;
    public int breakResultID = -1;

    public void ResetTileStatus()
    {
        isBurning = false;
        isWet = false;
        isBroken = false;
        breakResultID = -1;
    }

    #endregion

    #region Display

    public MapTileType GetDisplayMapType()
    {
        switch (tileType)
        {
            case MapTileType.Normal:
                if (curMapTileStatus == MapTileStatus.Wet)
                {
                    return MapTileType.Water;
                }
                else
                {
                    return MapTileType.Normal;
                }
            case MapTileType.Stone:
                if (curMapTileStatus == MapTileStatus.Broken)
                {
                    switch (breakResultID)
                    {
/*                        default:
                            return MapTileType.Hope;*/


                        case 0:
                            return MapTileType.Magic;
                        case 1:
                            return MapTileType.Duel;
                        case 2:
                            return MapTileType.Guard;
                        case 3:
                            return MapTileType.Stealth;
                        case 4:
                            return MapTileType.Hope;
                        default:
                            return MapTileType.Stone;
                    }
                }
                else
                {
                    return MapTileType.Stone;
                }
            default:
                return tileType;
        }
    }
    #endregion

    public bool CanPlant
    {
        get
        {
            switch (GetDisplayMapType())
            {
                case MapTileType.Normal:
                case MapTileType.Water:
                case MapTileType.Grass:
                case MapTileType.Flower:
                    return true;
                default:
                    return false;
            }
        }
    }


}

/// <summary>
/// 
/// </summary>
public class FindPathNode
{
    public Vector2Int pos;
    //F = G + H
    public int gCost = int.MaxValue;
    public int hCostReal = int.MaxValue;

    public List<Vector2Int> path;
    public FindPathNode parentNode;
    public FindPathNode hParentNode;

    public FindPathNode(Vector2Int nodePos)
    {
        this.pos = nodePos;
        this.path = null;
        this.parentNode = null;
        this.hParentNode = null;
    }

}