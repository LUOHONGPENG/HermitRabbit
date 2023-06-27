using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIMgr : MonoBehaviour
{
    public Button btnTest;

    public void Init()
    {
        btnTest.onClick.RemoveAllListeners();
        btnTest.onClick.AddListener(delegate ()
        {
            Debug.Log("Init Menu UI");
        });
    }
}
