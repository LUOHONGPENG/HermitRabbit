using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceInterfaceUIMgr : MonoBehaviour
{
    public Text codeEssence;
    public Text codeMemory;

    public Canvas canvas;
    private GameData gameData;

    private VictoryUIMgr victoryUIMgr;
    private bool isInit = false;
    public void Init()
    {
        gameData = PublicTool.GetGameData();
        RefreshResourceUI();

        victoryUIMgr = GameMgr.Instance.curSceneGameMgr.uiMgr.pageUIMgr.victoryUIMgr;
        isInit = true;
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
        codeEssence.text = string.Format("{0}/{1}", gameData.curEssence, gameData.essence);
        codeMemory.text = gameData.memory.ToString();
    }

    private void Update()
    {
        if (isInit)
        {
            if (victoryUIMgr.objMapClip.activeSelf||victoryUIMgr.objPlant.activeSelf)
            {
                canvas.sortingOrder = 10;
            }
            else
            {
                canvas.sortingOrder = 0;
            }
        }
    }
}
