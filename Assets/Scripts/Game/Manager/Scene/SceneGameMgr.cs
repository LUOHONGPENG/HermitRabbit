using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class SceneGameMgr : MonoBehaviour
{
    public Camera mapCamera;
    public Camera uiCamera;
    public SceneVirtualCameraMgr sceneVirtualCameraMgr;

    public LevelMgr levelMgr;
    public UIMgr uiMgr;
    public CameraMgr cameraMgr;
    public LightMgr lightMgr;

    private bool isInit = false;

    public IEnumerator IE_Init()
    {
        yield return new WaitUntil(() => GameMgr.Instance.isInit);
        GameMgr.Instance.curSceneGameMgr = this;
        GameMgr.Instance.curUICamera = uiCamera;
        InitSceneVirtualCamera();

        //Reset Phase
        InputMgr.Instance.SetInteractState(InteractState.PeaceNormal);
        levelMgr.Init();
        uiMgr.Init(uiCamera);
        lightMgr.Init();

        GameMgr.Instance.curMapCamera = mapCamera;

        isInit = true;

        yield return new WaitForEndOfFrame();

        if (PublicTool.GetGameData().numDay == 1)
        {
            uiMgr.pageUIMgr.talkUIMgr.StartTalk(TalkGroup.Day1);
        }

    }

    private void InitSceneVirtualCamera()
    {
        sceneVirtualCameraMgr.virtualCamera_Normal.Follow = cameraMgr.tfNormalCamera;
        sceneVirtualCameraMgr.virtualCamera_Normal.LookAt = cameraMgr.tfNormalCamera;

        sceneVirtualCameraMgr.virtualCamera_SkillPerform.Follow = cameraMgr.tfSkillPerformDelta;
        sceneVirtualCameraMgr.virtualCamera_SkillPerform.LookAt = cameraMgr.tfSkillPerformLookAt;

        sceneVirtualCameraMgr.Init();
    }
}
