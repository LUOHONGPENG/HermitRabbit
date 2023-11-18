using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapTileData
{
    public Vector2Int posID;
    public MapTileType tileType = MapTileType.Normal;
    public bool isBurning = false;

    public MapTileData(Vector2Int posID)
    {
        this.posID = posID;
        SetMapType(MapTileType.Normal);
        isBurning = false;
    }

    public void SetMapType(MapTileType tileType)
    {
        this.tileType = tileType;
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