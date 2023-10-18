using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeacePlantUIMgr : MonoBehaviour
{
    public GameObject objPopup;

    public Transform tfPlantBtn;
    public GameObject pfPlantBtn;

    public Button btnClose;

    public void Init()
    {
        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(delegate ()
        {

        });
    }

    public void ShowPopup()
    {

        PublicTool.ClearChildItem(tfPlantBtn);


    }

    public void HidePopup()
    {

    }

}
