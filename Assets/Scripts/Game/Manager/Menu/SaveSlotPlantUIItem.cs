using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotPlantUIItem : MonoBehaviour
{
    public Image imgIcon;

    public void Init(int plantID)
    {
        PlantExcelItem plantItem = PublicTool.GetPlantItem(plantID);
        if (plantItem != null)
        {
            imgIcon.sprite = Resources.Load("Sprite/Plant/" + plantItem.pixelUrl, typeof(Sprite)) as Sprite;
        }
    }

}
