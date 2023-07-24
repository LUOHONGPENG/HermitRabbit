using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButtonMgr : MonoBehaviour
{
    public GameObject objPopup;

    public Button btnStartBattle;
    public Button btnGenerateFoe;

    public void Init()
    {
        btnStartBattle.onClick.RemoveAllListeners();
        btnStartBattle.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("TestButton", "StartBattle");
        });


        btnGenerateFoe.onClick.RemoveAllListeners();
        btnGenerateFoe.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("TestButton", "GenerateFoe");
        });
    }
}
