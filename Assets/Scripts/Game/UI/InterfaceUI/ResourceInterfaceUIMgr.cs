using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceInterfaceUIMgr : MonoBehaviour
{
    public Text codeEssence;
    public Text codeMemory;

    private GameData gameData;

    public void Init()
    {
        gameData = PublicTool.GetGameData();
        RefreshResourceUI();
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("RefreshResourceUI", RefreshResourceUIEvent);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("RefreshResourceUI", RefreshResourceUIEvent);
    }

    private void RefreshResourceUIEvent(object arg0)
    {
        RefreshResourceUI();
    }

    public void RefreshResourceUI()
    {
        codeEssence.text = string.Format("{0}/{1}", 0, gameData.essence);
        codeMemory.text = gameData.memory.ToString();
    }
}
