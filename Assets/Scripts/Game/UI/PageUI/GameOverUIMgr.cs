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

    public Image imgIcon;
    public List<Sprite> listSp = new List<Sprite>();
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
        imgIcon.sprite = listSp[0];
        btnAction.onClick.RemoveAllListeners();
        btnAction.onClick.AddListener(delegate ()
        {
            GoBackToMenu();
        });
        ShowPopup();
    }
    private void GoodEndStartEvent(object arg0)
    {
        codeGameOver.text = "Good End";
        codeBtnAction.text = "Menu";
        imgIcon.sprite = listSp[1];
        btnAction.onClick.RemoveAllListeners();
        btnAction.onClick.AddListener(delegate ()
        {
            GoBackToMenu();
        });
        ShowPopup();
    }

    public void GoBackToMenu()
    {
        PublicTool.BeforeLoad();
        GameMgr.Instance.LoadScene(SceneName.Menu);
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
