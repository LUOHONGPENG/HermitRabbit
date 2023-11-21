using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadGameUIMgr : MonoBehaviour
{
    public GameObject objPopup;

    public Transform tfSaveSlot;
    public GameObject pfSaveSlot;

    public Text codeTitle;
    public Button btnClose;

    public SaveSlotUIItem.SaveButtonType saveButtonType;

    public void Init()
    {
        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(delegate ()
        {
            HidePopup();
        });
    }

    public void ShowPopup(SaveSlotUIItem.SaveButtonType saveButtonType)
    {
        this.saveButtonType = saveButtonType;

        PublicTool.ClearChildItem(tfSaveSlot);

        switch (saveButtonType)
        {
            case SaveSlotUIItem.SaveButtonType.MenuLoad:
                codeTitle.text = "Load";
                ShowMenuLoad();
                break;
            case SaveSlotUIItem.SaveButtonType.GameLoad:
                codeTitle.text = "Load";
                ShowGameLoad();
                break;
            case SaveSlotUIItem.SaveButtonType.GameSave:
                codeTitle.text = "Save";
                ShowGameSave();
                break;
        }

        objPopup.SetActive(true);
    }

    public void HidePopup()
    {
        objPopup.SetActive(false);
    }


    public void ShowMenuLoad()
    {
        for (int i = (int)SaveSlotName.Auto; i < (int)SaveSlotName.End; i++)
        {
            SaveSlotName slotName = (SaveSlotName)i;
            GameObject objSlot = GameObject.Instantiate(pfSaveSlot, tfSaveSlot);
            SaveSlotUIItem itemSlot = objSlot.GetComponent<SaveSlotUIItem>();
            itemSlot.Init(SaveSlotUIItem.SaveButtonType.MenuLoad, slotName, null);
        }
    }

    public void ShowGameLoad()
    {
        for (int i = (int)SaveSlotName.Auto; i < (int)SaveSlotName.End; i++)
        {
            SaveSlotName slotName = (SaveSlotName)i;
            GameObject objSlot = GameObject.Instantiate(pfSaveSlot, tfSaveSlot);
            SaveSlotUIItem itemSlot = objSlot.GetComponent<SaveSlotUIItem>();
            itemSlot.Init(SaveSlotUIItem.SaveButtonType.GameLoad, slotName, null);
        }
    }

    public void ShowGameSave()
    {
        for (int i = (int)SaveSlotName.Slot1; i < (int)SaveSlotName.End; i++)
        {
            SaveSlotName slotName = (SaveSlotName)i;
            GameObject objSlot = GameObject.Instantiate(pfSaveSlot, tfSaveSlot);
            SaveSlotUIItem itemSlot = objSlot.GetComponent<SaveSlotUIItem>();
            itemSlot.Init(SaveSlotUIItem.SaveButtonType.GameSave, slotName, HidePopup);
        }
    }
}
