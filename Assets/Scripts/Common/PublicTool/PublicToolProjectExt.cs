using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{
    public static Vector3 ConvertPosFromID(Vector2Int posID)
    {
        int centerX = (GameGlobal.mapSize - 1) / 2;
        int centerZ = (GameGlobal.mapSize - 1) / 2;
        /*        int centerX = 0;
                int centerZ = 0;*/

        return new Vector3(posID.x - centerX,0 ,posID.y - centerZ);
    }



}
