using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMgr : MonoBehaviour
{
    public GameObject objPopup;

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
