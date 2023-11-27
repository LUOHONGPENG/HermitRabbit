using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class PeacePlantUIItem : MonoBehaviour
{
    public Button btnPlant;

    public Outline outlinePlant;
    public CanvasGroup canvasPlant;

    public Image imgPlant;
    public Image imgSelected;

    private int typeID = -1;

    private PeacePlantUIMgr parent;

    public int GetTypeID()
    {
        return typeID;
    }

    public void Init(int typeID, PeacePlantUIMgr parent)
    {
        this.parent = parent;
        this.typeID = typeID;

        btnPlant.onClick.RemoveAllListeners();
        btnPlant.onClick.AddListener(delegate ()
        {
            PeaceMgr.Instance.plantTypeID = typeID;
            parent.RefreshSelected();
        });

        PlantExcelItem plantItem = PublicTool.GetPlantItem(typeID);
        imgPlant.sprite = Resources.Load("Sprite/Plant/" + plantItem.pixelUrl, typeof(Sprite)) as Sprite;
    }

    public void UpdateSelect(bool isSelected)
    {
        if (isSelected)
        {
            imgSelected.gameObject.SetActive(true);
        }
        else
        {
            imgSelected.gameObject.SetActive(false);
        }
    }

    public void UpdateWhetherUsed(bool isUsed)
    {
        if (isUsed)
        {
            canvasPlant.alpha = 0.2f;
        }
        else
        {
            canvasPlant.alpha = 1f;
        }
    }
}
