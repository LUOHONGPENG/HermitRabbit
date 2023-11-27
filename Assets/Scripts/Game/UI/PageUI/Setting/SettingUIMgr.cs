using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUIMgr : MonoBehaviour
{
    public GameObject objPopup;

    public Button btnClose;
    public Button btnSave;
    public Button btnLoad;
    public Button btnBackToMain;

    [Header("SaveLoad")]
    public SaveLoadGameUIMgr saveLoadGameUIMgr;


    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("ShowSetting", ShowSettingEvent);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("ShowSetting", ShowSettingEvent);
    }

    public void Init()
    {
        saveLoadGameUIMgr.Init();

        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(delegate ()
        {
            objPopup.SetActive(false);
        });

        btnSave.onClick.RemoveAllListeners();
        btnSave.onClick.AddListener(delegate ()
        {
            saveLoadGameUIMgr.ShowPopup(SaveSlotUIItem.SaveButtonType.GameSave);
        });

        btnLoad.onClick.RemoveAllListeners();
        btnLoad.onClick.AddListener(delegate ()
        {
            saveLoadGameUIMgr.ShowPopup(SaveSlotUIItem.SaveButtonType.GameLoad);
        });

        btnBackToMain.onClick.RemoveAllListeners();
        btnBackToMain.onClick.AddListener(delegate ()
        {
            PublicTool.BeforeLoad();
            GameMgr.Instance.LoadScene(SceneName.Menu);
        });
    }

    private void ShowSettingEvent(object arg0)
    {
        if(PublicTool.GetGameData().gamePhase == GamePhase.Battle)
        {
            btnSave.interactable = false;
        }
        else
        {
            btnSave.interactable = true;
        }

        objPopup.SetActive(true);
    }

}
