using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIMgr : MonoBehaviour
{
    public Button btnTest;

    public void Init()
    {
        btnTest.onClick.RemoveAllListeners();
        btnTest.onClick.AddListener(delegate ()
        {
            GameMgr.Instance.LoadScene(SceneName.Test);
        });
    }
}
