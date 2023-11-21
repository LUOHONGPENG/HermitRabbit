using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUIItem : MonoBehaviour
{
    public Text txSlotName;
    public Text codeMemory;
    public Button btnLoadSlot;
    public GameObject objInfo;
    public GameObject objNull;

    private bool canLoad = false;

    public void Init(SaveSlotName slotName)
    {
        btnLoadSlot.onClick.RemoveAllListeners();
        btnLoadSlot.onClick.AddListener(delegate ()
        {
            if (canLoad)
            {
                GameMgr.Instance.LoadScene(SceneName.Game, false, slotName);
            }
        });

        txSlotName.text = slotName.ToString();
        GameSaveData saveData = GameMgr.Instance.GetSaveData(slotName);
        if (saveData != null)
        {
            canLoad = true;
            RefreshInfo(saveData);
            objInfo.SetActive(true);
            objNull.SetActive(false);
        }
        else
        {
            canLoad = false;
            objInfo.SetActive(false);
            objNull.SetActive(true);
        }

    }

    private void RefreshInfo(GameSaveData saveData)
    {
        codeMemory.text = saveData.memory.ToString();
    }
}
