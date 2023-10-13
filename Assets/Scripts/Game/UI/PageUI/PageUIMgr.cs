using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageUIMgr : MonoBehaviour
{
    public GameOverUIMgr gameOverUIMgr;
    public StatusUIMgr statusUIMgr;


    public void Init()
    {
        gameOverUIMgr.Init();
        statusUIMgr.Init();
    }
}
