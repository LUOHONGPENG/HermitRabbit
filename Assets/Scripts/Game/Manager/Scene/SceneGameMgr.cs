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
    public LightMgr lightMgr;

    public IEnumerator IE_Init()
    {
        yield return new WaitUntil(() => GameMgr.Instance.isInit);
        GameMgr.Instance.curSceneGameMgr = this;
        GameMgr.Instance.curUICamera = uiCamera;
        virtualCamera.Follow = cameraMgr.transform;
        virtualCamera.LookAt = cameraMgr.transform;

        //Reset Phase
        InputMgr.Instance.SetInteractState(InteractState.PeaceNormal);
        levelMgr.Init();
        uiMgr.Init(uiCamera);



        GameMgr.Instance.curMapCamera = mapCamera;

        //Set Current SceneMgr
    }
}
