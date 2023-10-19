using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeaceMapClipUIItem : MonoBehaviour
{
    public Button btnClip;
    public List<Image> listTile = new List<Image>();
    public List<Color> listColor = new List<Color>();

    public void Init(int typeID)
    {
        btnClip.onClick.RemoveAllListeners();
        btnClip.onClick.AddListener(delegate ()
        {
            PeaceMgr.Instance.mapClipTypeID = typeID;
        });

        if (typeID < 0)
        {
            for (int i = 0; i < listTile.Count; i++)
            {
                listTile[i].color = listColor[0];
            }
        }
        else
        {
            MapClipExcelItem mapItem = PublicTool.GetMapClipItem(typeID);
            for (int i = 0; i < mapItem.listMapTile.Count; i++)
            {
                MapTileType type = mapItem.listMapTile[i];
                listTile[i].color = listColor[(int)type];
            }
        }

    }
}
