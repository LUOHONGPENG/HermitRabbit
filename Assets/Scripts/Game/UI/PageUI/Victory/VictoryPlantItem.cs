using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VictoryPlantItem : MonoBehaviour
{
    public Image imgMapClip;
    public Button btnMapClip;

    public Image imgPlant;

    public Text codeName;
    public Text codeEssence;


    private int typeID = -1;
    private VictoryUIMgr parent;


    public void Init(int typeID)
    {
        this.typeID = typeID;

        PlantExcelItem plantExcelItem = PublicTool.GetPlantItem(typeID);
        imgPlant.sprite = Resources.Load("Sprite/Plant/" + plantExcelItem.pixelUrl, typeof(Sprite)) as Sprite;

        codeName.text = plantExcelItem.GetName();
        codeEssence.text = plantExcelItem.essence.ToString();

        btnMapClip.onClick.RemoveAllListeners();
        btnMapClip.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("VictoryAddPlant", typeID);
        });
    }

    public int GetTypeID()
    {
        return typeID;
    }

}
