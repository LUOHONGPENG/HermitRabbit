using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SaveSlotUIItem : MonoBehaviour
{
    public enum SaveButtonType
    {
        MenuLoad,
        GameSave,
        GameLoad
    }

    public Text txSlotName;
    public Text codeMemory;
    public Button btnSlot;
    public GameObject objInfo;
    public GameObject objNull;

    private bool isNull = true;
    private SaveSlotName slotName;

    public void Init(SaveButtonType saveButtonType, SaveSlotName tempSlotName,UnityAction extraAction)
    {
        this.slotName = tempSlotName;
        txSlotName.text = slotName.ToString();
        GameSaveData saveData = GameMgr.Instance.GetSaveData(slotName);
        CheckWhetherNull(saveData);

        btnSlot.onClick.RemoveAllListeners();
        btnSlot.onClick.AddListener(delegate ()
        {
            switch (saveButtonType)
            {
                case SaveButtonType.MenuLoad:
                    if (!isNull)
                    {
                        GameMgr.Instance.LoadScene(SceneName.Game, false, slotName);
                    }
                    break;
                case SaveButtonType.GameLoad:
                    if (!isNull)
                    {
                        GameMgr.Instance.LoadScene(SceneName.Game, false, slotName);
                    }
                    break;
                case SaveButtonType.GameSave:
                    GameMgr.Instance.SaveGameData(slotName);
                    break;
            }

            if (extraAction != null)
            {
                extraAction.Invoke();
            }

        });
    }

    private void CheckWhetherNull(GameSaveData saveData)
    {
        if (saveData != null)
        {
            isNull = false;
            RefreshInfo(saveData);
            objInfo.SetActive(true);
            objNull.SetActive(false);
        }
        else
        {
            isNull = true;
            objInfo.SetActive(false);
            objNull.SetActive(true);
        }
    }

    private void RefreshInfo(GameSaveData saveData)
    {
        codeMemory.text = saveData.memory.ToString();
    }
}
