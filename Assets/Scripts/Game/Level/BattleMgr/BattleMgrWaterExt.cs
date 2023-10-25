using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class BattleMgr
{
    public List<List<Vector2Int>> listGroupWater = new List<List<Vector2Int>>();

    public void RefreshWaterRange()
    {
        listGroupWater.Clear();
        for (int i = 0; i < gameData.listMapTile.Count; i++)
        {
            MapTileData tileData = gameData.listMapTile[i];
            if (tileData.tileType == MapTileType.Water)
            {
                if (GetWaterGroup(tileData.posID) == null)
                {
                    List<Vector2Int> listWater = PublicTool.GetTargetWaterRange(tileData.posID);
                    listGroupWater.Add(listWater);
                }
            }
        }
    }

    private List<Vector2Int> GetWaterGroup(Vector2Int tilePosID)
    {
        for(int i = 0; i < listGroupWater.Count; i++)
        {
            if (listGroupWater[i].Contains(tilePosID))
            {
                return listGroupWater[i];
            }
        }
        return null;
    }

    public List<Vector2Int> GetTargetWaterRange(List<Vector2Int> listTarget)
    {
        List<Vector2Int> listWaterPos = new List<Vector2Int>();
        for(int i = 0; i < listTarget.Count; i++)
        {
            Vector2Int target = listTarget[i];
            if (listWaterPos.Contains(target))
            {
                continue;
            }
            List<Vector2Int> listTemp = GetWaterGroup(target);
            if (listTemp == null)
            {
                listWaterPos.Add(target);
            }
            else
            {
                for(int j = 0; j < listTemp.Count;j++)
                {
                    if (!listWaterPos.Contains(listTemp[j]))
                    {
                        listWaterPos.Add(listTemp[j]);
                    }
                }
            }
        }
        return listWaterPos;
    }

}
