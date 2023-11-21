using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTileIconUIItem : MonoBehaviour
{
    public Image imgIcon;

    public void Init(MapTileType tileType)
    {
        MapTileExcelItem mapTileItem = PublicTool.GetMapTileItem(tileType);

        if(mapTileItem != null)
        {
            imgIcon.sprite = Resources.Load("Sprite/Tile/" + mapTileItem.iconUrl, typeof(Sprite)) as Sprite;
            //imgIcon.SetNativeSize();
        }

    }
}
