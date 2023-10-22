using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public partial class GameMgr : MonoSingleton<GameMgr>
{
    public Camera curMapCamera;
    public Camera curUICamera;
    public SceneGameMgr curSceneGameMgr = null;

    public bool isInit = false;

    #region Init
    public override void Init()
    {
        StartCoroutine(IE_InitGame());
    }

    public IEnumerator IE_InitGame()
    {
        yield return StartCoroutine(ExcelDataMgr.Instance.IE_Init());
        yield return StartCoroutine(InputMgr.Instance.IE_Init());
        yield return StartCoroutine(SoundMgr.Instance.IE_Init());

        LoadScene(SceneName.Menu);

        Debug.Log("Init Game Manager");
        isInit = true;
    }
    #endregion

    #region LoadScene

    public void LoadScene(SceneName sceneName,bool isNewGame=true,SaveSlotName saveSlotName= SaveSlotName.Auto)
    {
        GameGlobal.targetScene = sceneName;
        switch (GameGlobal.targetScene)
        {
            case SceneName.Test:
                NewGameInitData();
                break;
            case SceneName.Game:
                if (isNewGame)
                {
                    NewGameInitData();
                }
                else
                {
                    LoadGameData(saveSlotName);
                }
                break;
        }
        SceneManager.LoadScene(GameGlobal.targetScene.ToString());
    }
    #endregion

}
