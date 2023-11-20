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

    public void UpdateBasicInfo(MapTileData tileData)
    {


        objPopup.SetActive(true) ;

    }

    public void HideTip()
    {
        objPopup.SetActive(false);
    }
}
