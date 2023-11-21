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
    public Button btnDelete;


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
            loadGameUIMgr.ShowPopup();
        });

        btnDelete.onClick.RemoveAllListeners();
        btnDelete.onClick.AddListener(delegate ()
        {
            PlayerPrefs.DeleteAll();
        });
    }
}
