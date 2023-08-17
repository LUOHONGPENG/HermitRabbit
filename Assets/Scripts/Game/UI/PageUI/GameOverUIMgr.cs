using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIMgr : MonoBehaviour
{
    public GameObject objPopup;
    public Button btnRetry;

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("BattleLose", BattleLoseEvent);

    }



    public void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("BattleLose", BattleLoseEvent);

    }

    private void BattleLoseEvent(object arg0)
    {
        ShowPopup();
    }

    public void ShowPopup()
    {
        objPopup.SetActive(true);
    }

    public void HidePopup()
    {
        objPopup.SetActive(false);
    }
}
