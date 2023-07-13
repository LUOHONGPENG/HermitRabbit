using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class SceneGameMgr : MonoBehaviour
{
    public Camera mapCamera;
    public Camera uiCamera;
    public CinemachineVirtualCamera virtualCamera;

    public LevelMgr levelMgr;
    public UIMgr uiMgr;
    public CameraMgr cameraMgr;

    public IEnumerator IE_Init()
    {
        yield return new WaitUntil(() => GameMgr.Instance.isInit);
        levelMgr.Init();
        uiMgr.Init(uiCamera);
        GameMgr.Instance.curMapCamera = mapCamera;
        GameMgr.Instance.curUICamera = uiCamera;
        virtualCamera.Follow = cameraMgr.transform;
        virtualCamera.LookAt = cameraMgr.transform;
        //Set Current SceneMgr
        GameMgr.Instance.curSceneGameMgr = this;
    }
}
