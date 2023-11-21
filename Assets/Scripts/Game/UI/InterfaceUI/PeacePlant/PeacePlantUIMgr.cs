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

    public Button btnSave;

    public Button btnRemovePlant;

    private List<PeacePlantUIItem> listPlantBtn = new List<PeacePlantUIItem>();

    public void Init()
    {
/*        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(delegate ()
        {

        });*/

        btnRemovePlant.onClick.RemoveAllListeners();
        btnRemovePlant.onClick.AddListener(delegate ()
        {
            PeaceMgr.Instance.plantTypeID = -1;
        });

        btnSave.onClick.RemoveAllListeners();
        btnSave.onClick.AddListener(delegate ()
        {
            PublicTool.EventChangeInteract(InteractState.PeaceNormal);
            EventCenter.Instance.EventTrigger("PeacePlantEnd", null);

        });
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void ShowPopup()
    {
        PublicTool.ClearChildItem(tfPlantBtn);
        listPlantBtn.Clear();

        //Plant
        for(int i = 0;i < PublicTool.GetGameData().listPlantHeld.Count; i++)
        {
            int typeID = PublicTool.GetGameData().listPlantHeld[i];
            GameObject objPlant = GameObject.Instantiate(pfPlantBtn, tfPlantBtn);
            PeacePlantUIItem itemPlant = objPlant.GetComponent<PeacePlantUIItem>();
            itemPlant.Init(typeID);
            listPlantBtn.Add(itemPlant);
        }

        objPopup.SetActive(true);
    }

    public void HidePopup()
    {
        objPopup.SetActive(false);
    }


}
