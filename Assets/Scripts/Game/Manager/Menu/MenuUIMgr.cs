using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIMgr : MonoBehaviour
{
    public SaveLoadGameUIMgr loadGameUIMgr;

    public Button btnTest;
    public Button btnNewGame;
    public Button btnLoadGame;
    public Button btnQuit;

    public Button btnDelete;

    public Button btnEN;
    public Button btnCN;



    public void Init()
    {
        loadGameUIMgr.Init();

        btnTest.onClick.RemoveAllListeners();
        btnTest.onClick.AddListener(delegate ()
        {
            GameMgr.Instance.LoadScene(SceneName.Test);
        });

        btnNewGame.onClick.RemoveAllListeners();
        btnNewGame.onClick.AddListener(delegate ()
        {
            GameMgr.Instance.LoadScene(SceneName.Game,true);
        });

        btnLoadGame.onClick.RemoveAllListeners();
        btnLoadGame.onClick.AddListener(delegate ()
        {
            loadGameUIMgr.ShowPopup(SaveSlotUIItem.SaveButtonType.MenuLoad);
        });

        btnQuit.onClick.RemoveAllListeners();
        btnQuit.onClick.AddListener(delegate ()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//用于退出运行

#else
Application.Quit();
#endif
        });


        btnDelete.onClick.RemoveAllListeners();
        btnDelete.onClick.AddListener(delegate ()
        {
            PlayerPrefs.DeleteAll();
        });

        btnEN.onClick.RemoveAllListeners();
        btnEN.onClick.AddListener(delegate ()
        {
            GameGlobal.languageType = LanguageType.EN;
            GameMgr.Instance.LoadScene(SceneName.Menu);
        });

        btnCN.onClick.RemoveAllListeners();
        btnCN.onClick.AddListener(delegate ()
        {
            GameGlobal.languageType = LanguageType.CN;
            GameMgr.Instance.LoadScene(SceneName.Menu);
        });

        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            btnQuit.gameObject.SetActive(false);
        }
        else
        {
            btnQuit.gameObject.SetActive(true);
        }
    }
}
