using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    public Canvas thisCanvas;

    public EffectUIMgr effectUIMgr;
    public InterfaceUIMgr interfaceUIMgr;
    public PageUIMgr pageUIMgr;
    public TestButtonMgr testButtonMgr;

    public void Init(Camera camera)
    {
        thisCanvas.worldCamera = camera;

        //Debug
        interfaceUIMgr.Init();
        testButtonMgr.Init();
    }
}
