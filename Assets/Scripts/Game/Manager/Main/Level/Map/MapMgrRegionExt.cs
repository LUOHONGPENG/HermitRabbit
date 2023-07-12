using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MapMgr
{
    public List<Vector2Int> GetTargetCrossRange(Vector2Int targetPos,int Range)
    {
        List<Vector2Int> listRange = new List<Vector2Int>();
        if (Range > 0)
        {
            for(int i = -Range; i <= Range;i++)
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

        return listRange;
    }
}
