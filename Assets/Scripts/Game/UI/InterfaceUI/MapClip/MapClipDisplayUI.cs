using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapClipDisplayUI : MonoBehaviour
{
    public List<MapTileIconUIItem> listIcon = new List<MapTileIconUIItem>();

    public void Init(int typeID)
    {
        if (typeID < 0)
        {
            for (int i = 0; i < listIcon.Count; i++)
            {
                listIcon[i].Init(MapTileType.Normal);
            }
        }
        else
        {
            MapClipExcelItem mapItem = PublicTool.GetMapClipItem(typeID);
            for (int i = 0; i < mapItem.listMapTile.Count; i++)
            {
                MapTileType type = mapItem.listMapTile[i];
                listIcon[i].Init(type);
            }
        }
    }

}
