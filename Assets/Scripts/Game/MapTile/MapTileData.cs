using System.Collections;
using System.Collections.Generic;
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
    public int bonusID = -1;

    public void ResetTileStatus()
    {
        isBurning = false;
        isWet = false;
        isBroken = false;
        bonusID = -1;
    }

    #endregion

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