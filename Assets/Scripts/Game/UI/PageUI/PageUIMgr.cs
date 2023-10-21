using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageUIMgr : MonoBehaviour
{
    public GameOverUIMgr gameOverUIMgr;
    public StatusUIMgr statusUIMgr;
    public VictoryUIMgr victoryUIMgr;

    public void Init()
    {
        gameOverUIMgr.Init();
        statusUIMgr.Init();
        victoryUIMgr.Init();
    }
}
