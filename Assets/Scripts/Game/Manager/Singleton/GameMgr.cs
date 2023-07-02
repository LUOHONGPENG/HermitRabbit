using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMgr : MonoSingleton<GameMgr>
{

    #region Init
    public override void Init()
    {
        StartCoroutine(IE_InitGame());
    }

    public IEnumerator IE_InitGame()
    {
        yield return StartCoroutine(DataMgr.Instance.IE_Init());
        yield return StartCoroutine(SoundMgr.Instance.IE_Init());
        SceneManager.LoadScene("Menu");
        Debug.Log("Init Game Manager");
    }
    #endregion
}
