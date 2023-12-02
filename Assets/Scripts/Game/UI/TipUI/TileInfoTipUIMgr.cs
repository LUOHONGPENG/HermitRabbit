using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileInfoTipUIMgr : MonoBehaviour
{
    [Header("Basic")]
    public GameObject objPopup;
    public Transform tfMouse;

    [Header("Info")]
    public Text codeName;
    public TextMeshProUGUI codeDesc;
    public Image imgIcon;

    private MapTileData recordTileData = null;
    private MapTileType recordTileType = MapTileType.End;

    public void UpdateBasicInfo(MapTileData tileData)
    {
        if (!objPopup.activeSelf || this.recordTileData != tileData)
        {
            this.recordTileData = tileData;
            RefreshBasicInfo();
            objPopup.SetActive(true);
        }
        else
        {
            RefreshBasicInfo();
        }
    }

    public void UpdateBasicInfo(MapTileType tileType)
    {
        MapTileExcelItem tileItem = PublicTool.GetMapTileItem(tileType);
        codeName.text = tileItem.GetName();
        codeDesc.text = tileItem.GetDesc();
        imgIcon.sprite = Resources.Load("Sprite/Tile/" + tileItem.iconUrl, typeof(Sprite)) as Sprite;
        objPopup.SetActive(true);
    }

    //Not Include Burning
    private void RefreshBasicInfo()
    {
        if(recordTileData != null)
        {
            if(recordTileData.GetDisplayMapType() == recordTileType)
            {
                return;
            }
            else
            {
                recordTileType = recordTileData.GetDisplayMapType();
            }

            MapTileExcelItem tileItem = PublicTool.GetMapTileItem(recordTileType);
            codeName.text = tileItem.GetName();
            codeDesc.text = tileItem.GetDesc();
            imgIcon.sprite = Resources.Load("Sprite/Tile/" + tileItem.iconUrl, typeof(Sprite)) as Sprite;
        }
    }

    public void HideTip()
    {
        objPopup.SetActive(false);
    }
}
