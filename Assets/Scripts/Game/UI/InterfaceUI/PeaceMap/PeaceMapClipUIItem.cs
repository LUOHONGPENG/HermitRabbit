using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeaceMapClipUIItem : MonoBehaviour
{
    public Button btnClip;
    public Image imgSelected;

    public Outline outlineClip;
    public CanvasGroup canvasGroupClip;

    public MapClipDisplayUI mapClipDisplay;

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

        mapClipDisplay.Init(typeID);

    }

    public void UpdateSelect(bool isSelected)
    {
        if (isSelected)
        {
            imgSelected.gameObject.SetActive(true);
            //outlineClip.enabled = true;
        }
        else
        {
            imgSelected.gameObject.SetActive(false);

            //outlineClip.enabled = false;
        }
    }

    public void UpdateWhetherUsed(bool isUsed)
    {
        if (isUsed)
        {
            canvasGroupClip.alpha = 0.2f;
        }
        else
        {
            canvasGroupClip.alpha = 1f;
        }
    }

    
}
