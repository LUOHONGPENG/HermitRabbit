using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TestGameMgr : MonoBehaviour
{
    public Camera mapCamera;
    public Camera uiCamera;
    public CinemachineVirtualCamera virtualCamera;

    public LevelMgr testLevelMgr;
    public UIMgr testUIMgr;
    public CameraMgr cameraMgr;

    public void Start()
    {
        GameGlobal.targetScene = SceneName.Test;
        StartCoroutine(IE_Init());
    }


    public IEnumerator IE_Init()
    {
        yield return new WaitUntil(() => GameMgr.Instance.isInit);
        testLevelMgr.Init();
        testUIMgr.Init(uiCamera);
        GameMgr.Instance.curMapCamera = mapCamera;
        GameMgr.Instance.curUICamera = uiCamera;
        virtualCamera.Follow = cameraMgr.transform;
        virtualCamera.LookAt = cameraMgr.transform;

    }
}
