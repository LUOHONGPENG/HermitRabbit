using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameMgr : MonoBehaviour
{
    public Camera mapCamera;
    public Camera uiCamera;

    public MapMgr testMapMgr;
    public UIMgr testUIMgr;

    public void Start()
    {
        testMapMgr.Init();
        testUIMgr.Init(uiCamera);
    }

}
