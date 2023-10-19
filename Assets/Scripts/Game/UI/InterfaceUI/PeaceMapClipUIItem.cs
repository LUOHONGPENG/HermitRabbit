using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeaceMapClipUIItem : MonoBehaviour
{
    public Button btnClip;
    public List<Image> listTile = new List<Image>();
    public List<Color> listColor = new List<Color>();

    public Outline outlineClip;
    public CanvasGroup canvasGroupClip;

    private int typeID = -1;

    public int GetTypeID()
    {
        return typeID;
    }

    public void Init(int typeID)
    {
        this.typeID = typeID;

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

    public void UpdateSelect(bool isSelected)
    {
        if (isSelected)
        {
            outlineClip.enabled = true;
        }
        else
        {
            outlineClip.enabled = false;
        }
    }

    public void UpdateWhetherUsed(bool isUsed)
    {
        if (isUsed)
        {
            canvasGroupClip.alpha = 0.5f;
        }
        else
        {
            canvasGroupClip.alpha = 1f;
        }
    }

    
}
