using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLoadGameUIMgr : MonoBehaviour
{
    public GameObject objPopup;

    public Transform tfSaveSlot;
    public GameObject pfSaveSlot;

    public Button btnClose;

    public void Init()
    {
        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(delegate ()
        {
            objPopup.SetActive(false);
        });
    }

    public void ShowPopup()
    {
        PublicTool.ClearChildItem(tfSaveSlot);

        for(int i = (int)SaveSlotName.Auto;i < (int)SaveSlotName.End; i++)
        {
            SaveSlotName slotName = (SaveSlotName)i;
            GameObject objSlot = GameObject.Instantiate(pfSaveSlot, tfSaveSlot);
            SaveSlotUIItem itemSlot = objSlot.GetComponent<SaveSlotUIItem>();
            itemSlot.Init(slotName);
        }

        objPopup.SetActive(true);

    }

}
