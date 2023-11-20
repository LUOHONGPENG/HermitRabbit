using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileInfoTipUIMgr : MonoBehaviour
{
    [Header("Basic")]
    public GameObject objPopup;
    public Transform tfMouse;

    [Header("Info")]
    public Text codeName;
    public Text codeDesc;
    public Image imgIcon;

    private MapTileData recordTileData = null;
    private MapTileType recordTileType = MapTileType.Normal;

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
            codeName.text = tileItem.name;
            codeDesc.text = tileItem.desc;
        }
    }

    public void HideTip()
    {
        objPopup.SetActive(false);
    }
}
