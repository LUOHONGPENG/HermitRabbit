using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMgr : MonoSingleton<GameMgr>
{
    public Camera curMapCamera;
    public Camera curUICamera;

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
        SceneManager.LoadScene(GameGlobal.targetScene.ToString());
        Debug.Log("Init Game Manager");
        isInit = true;
    }
    #endregion
}
