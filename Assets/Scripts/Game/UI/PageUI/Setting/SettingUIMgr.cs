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
        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(delegate ()
        {
            objPopup.SetActive(false);
        });

        btnSave.onClick.RemoveAllListeners();
        btnSave.onClick.AddListener(delegate ()
        {

        });

        btnLoad.onClick.RemoveAllListeners();
        btnLoad.onClick.AddListener(delegate ()
        {

        });

        btnBackToMain.onClick.RemoveAllListeners();
        btnBackToMain.onClick.AddListener(delegate ()
        {
            GameMgr.Instance.LoadScene(SceneName.Menu);
        });
    }

    private void ShowSettingEvent(object arg0)
    {
        objPopup.SetActive(true);
    }

}
