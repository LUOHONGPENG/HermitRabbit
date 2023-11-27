using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButtonMgr : MonoBehaviour
{
    public GameObject objPopup;

    public Button btnAddExp;

    public Button btnBack;

    public void Init()
    {
        btnAddExp.onClick.RemoveAllListeners();
        btnAddExp.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("TestButton", "AddExp");
        });


        btnBack.onClick.RemoveAllListeners();
        btnBack.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("TestButton", "BackToMenu");
        });
    }


}
