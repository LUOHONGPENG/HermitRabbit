using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameMgr : MonoBehaviour
{
    public Camera mapCamera;
    public Camera uiCamera;

    public LevelMgr testLevelMgr;
    public UIMgr testUIMgr;

    public void Start()
    {
        testLevelMgr.Init();
        testUIMgr.Init(uiCamera);
    }

}
