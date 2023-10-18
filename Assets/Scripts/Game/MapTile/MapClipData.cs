using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapClipUsedData
{
    //(0,0)(0,1)(0,2)
    public Vector2Int clipPosID;
    public int clipID = -1;

    public MapClipUsedData(Vector2Int clipPosID)
    {
        this.clipPosID = clipPosID;
        this.clipID = -1;
    }

    public void SetClipID(int clipID)
    {
        this.clipID = clipID;
    }
}
