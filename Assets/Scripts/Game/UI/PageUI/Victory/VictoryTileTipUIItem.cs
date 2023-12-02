using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryTileTipUIItem : MonoBehaviour
{
    public Image imgIcon;
    public TextMeshProUGUI codeName;

    private MapTileType tileType;

    public void Init(MapTileType tileType)
    {
        MapTileExcelItem tileItem = PublicTool.GetMapTileItem(tileType);
        this.tileType = tileType;
        codeName.text = tileItem.GetName();
        imgIcon.sprite = Resources.Load("Sprite/Tile/" + tileItem.iconUrl, typeof(Sprite)) as Sprite;
    }

    public MapTileType GetTileType()
    {
        return tileType;
    }


}
