using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButtonMgr : MonoBehaviour
{
    public GameObject objPopup;

    public Button btnAddExp;
    public Button btnGenerateFoe;
    public Button btnRandomMap;

    public void Init()
    {
        btnAddExp.onClick.RemoveAllListeners();
        btnAddExp.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("TestButton", "AddExp");
        });

        btnGenerateFoe.onClick.RemoveAllListeners();
        btnGenerateFoe.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("TestButton", "GenerateFoe");
        });

        btnRandomMap.onClick.RemoveAllListeners();
        btnRandomMap.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("TestButton", "RandomMap");
        });
    }
}
