using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIMgr : MonoBehaviour
{
    public GameObject objPopup;
    public Text codeGameOver;
    public Button btnAction;
    public Text codeBtnAction;

    public void Init()
    {

    }

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("BattleLose", BattleLoseEvent);
        EventCenter.Instance.AddEventListener("GoodEndStart", GoodEndStartEvent);
    }

    public void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("BattleLose", BattleLoseEvent);
        EventCenter.Instance.RemoveEventListener("GoodEndStart", GoodEndStartEvent);
    }

    private void BattleLoseEvent(object arg0)
    {
        codeGameOver.text = "Game Over";
        codeBtnAction.text = "Menu";
        btnAction.onClick.RemoveAllListeners();
        btnAction.onClick.AddListener(delegate ()
        {
            GameMgr.Instance.LoadScene(SceneName.Menu);
        });
        ShowPopup();
    }
    private void GoodEndStartEvent(object arg0)
    {
        codeGameOver.text = "Good End";
        codeBtnAction.text = "Back To Menu";
        btnAction.onClick.RemoveAllListeners();
        btnAction.onClick.AddListener(delegate ()
        {
            GameMgr.Instance.LoadScene(SceneName.Menu);
        });
        ShowPopup();
    }

    public void ShowPopup()
    {
        objPopup.SetActive(true);
    }

    public void HidePopup()
    {
        objPopup.SetActive(false);
    }
}
