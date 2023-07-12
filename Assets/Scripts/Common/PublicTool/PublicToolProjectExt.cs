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

    public static int CalculateGlobalDis(Vector2Int start,Vector2Int end)
    {
        int disX = Mathf.Abs(start.x - end.x);
        int disY = Mathf.Abs(start.y - end.y);
        return disX + disY;
    }

    public static List<Vector2Int> GetTargetCrossRange(Vector2Int targetPos, int Range)
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

}
