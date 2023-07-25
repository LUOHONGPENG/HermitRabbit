using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapTileData
{
    public Vector2Int posID;
    public int typeID;

    public MapTileData(Vector2Int posID, int typeID)
    {
        this.posID = posID;
        this.typeID = typeID;
    }

}
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