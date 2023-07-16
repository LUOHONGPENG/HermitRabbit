using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButtonMgr : MonoBehaviour
{
    public GameObject objPopup;

    public Button btnEndTurn;
    public Button btnStartBattle;
    public Button btnGenerateFoe;


    public void Init()
    {
        btnStartBattle.onClick.RemoveAllListeners();
        btnStartBattle.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("StartBattle", null);
        });

        btnEndTurn.onClick.RemoveAllListeners();
        btnEndTurn.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("EndTurn", null);
        });

        btnGenerateFoe.onClick.RemoveAllListeners();
        btnGenerateFoe.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("TestGenerateFoe", null);
        });
    }
}
