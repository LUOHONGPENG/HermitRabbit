using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MapTileExcelData
{

    public Dictionary<MapTileType, MapTileExcelItem> dicMapTileInfo = new Dictionary<MapTileType, MapTileExcelItem>();

    public void Init()
    {
        dicMapTileInfo.Clear();

        for(int i = 0; i < items.Length; i++)
        {
            MapTileExcelItem mapTileInfo = items[i];
            if (!dicMapTileInfo.ContainsKey(mapTileInfo.tileType))
            {
                dicMapTileInfo.Add(mapTileInfo.tileType, mapTileInfo);
            }
        }
    }

    public MapTileExcelItem GetMapTileInfo(MapTileType tileType)
    {
        if (dicMapTileInfo.ContainsKey(tileType))
        {
            return dicMapTileInfo[tileType];
        }
        else
        {
            return null;
        }
    }

}


public partial class MapTileExcelItem
{
    public string GetName()
    {
        if (GameGlobal.languageType == LanguageType.CN)
        {
            return name_CN;
        }
        else
        {
            return name_EN;
        }
    }

    public string GetDesc()
    {
        if (GameGlobal.languageType == LanguageType.CN)
        {
            return desc_CN;
        }
        else
        {
            return desc_EN;
        }
    }


}
