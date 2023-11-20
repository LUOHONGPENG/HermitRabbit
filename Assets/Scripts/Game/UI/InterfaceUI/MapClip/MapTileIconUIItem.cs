using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTileIconUIItem : MonoBehaviour
{
    public Image imgFill;
    public Image imgIcon;

    public void Init(MapTileType tileType)
    {
        MapTileExcelItem mapTileItem = PublicTool.GetMapTileItem(tileType);

        if(mapTileItem != null)
        {
            
        }

    }
}
