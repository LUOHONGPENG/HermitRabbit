using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapTileData
{
    public Vector2Int posID;
    public MapTileType tileType = MapTileType.Normal;

    public MapTileData(Vector2Int posID)
    {
        this.posID = posID;
        tileType = MapTileType.Normal;
    }

    public void SetMapType(MapTileType tileType)
    {
        this.tileType = tileType;
    }

    public void RandomMapType()
    {
        tileType = (MapTileType)Random.Range(0, 4);
    }

}

/// <summary>
/// 
/// </summary>
public class FindPathNode
{
    public Vector2Int pos;
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