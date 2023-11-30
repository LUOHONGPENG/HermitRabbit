using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public partial class GameMgr : MonoSingleton<GameMgr>
{
    public Camera curMapCamera;
    public Camera curUICamera;
    public SceneGameMgr curSceneGameMgr = null;
    public SceneName curSceneName = SceneName.Init;

    public Texture2D cursorTex;

    public bool isInit = false;

    #region Init
    public override void Init()
    {
        StartCoroutine(IE_InitGame());
    }

    public IEnumerator IE_InitGame()
    {
        Cursor.SetCursor(cursorTex, Vector2.zero, CursorMode.Auto);

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

                PublicTool.PlayMusic(MusicType.Peace);
                break;
            case SceneName.Menu:
                if (curSceneName != SceneName.Menu)
                {
                    PublicTool.PlayMusic(MusicType.Menu);
                }
                break;
        }
        curSceneName = sceneName;
        SceneManager.LoadScene(GameGlobal.targetScene.ToString());
    }
    #endregion

    public bool GetWhetherPageOn()
    {
        if (curSceneGameMgr != null)
        {
            if (curSceneGameMgr.uiMgr.pageUIMgr.isPageOn)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

}
