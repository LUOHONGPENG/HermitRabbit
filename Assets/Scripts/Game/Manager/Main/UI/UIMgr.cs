using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    public Canvas thisCanvas;

    public BattleOptionUIMgr battleOptionUIMgr;
    public TestButtonMgr testButtonMgr;

    public void Init(Camera camera)
    {
        thisCanvas.worldCamera = camera;

        //Debug
        battleOptionUIMgr.Init();
        testButtonMgr.Init();
    }
}
