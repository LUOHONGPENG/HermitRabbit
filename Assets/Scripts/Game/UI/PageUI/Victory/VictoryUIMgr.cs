using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryUIMgr : MonoBehaviour
{
    [Header("Basic")]
    public GameObject objPopup;
    public GameObject objMapClip;
    public GameObject objPlant;
    public Button btnContinue;

    [Header("EXP")]
    public Transform tfEXP;
    public GameObject objEXP;


    private VictoryPhase victoryPhase;

    public enum VictoryPhase
    {
        EXP,
        MapClip,
        Plant,
        End
    }

    #region Basic
    public void Init()
    {
        btnContinue.onClick.RemoveAllListeners();
        btnContinue.onClick.AddListener(delegate ()
        {
            //Need to modify
            HidePopup();
        });
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("NormalVictoryStart", ShowVictoryEvent);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("NormalVictoryStart", ShowVictoryEvent);
    }

    private void ShowVictoryEvent(object arg0)
    {
        victoryPhase = VictoryPhase.EXP;

        objPopup.SetActive(true);
    }

    public void HidePopup()
    {
        objPopup.SetActive(false);
    }
    #endregion

    #region EXP
    public void ShowExp()
    {

    }
    #endregion

    #region

    #endregion
}
