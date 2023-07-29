using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PeaceInterfaceUIMgr : MonoBehaviour
{
    public GameObject objPopup;
    public CanvasGroup groupBottom;

    public Button btnStartBattle;
    public Button btnStartPlant;

    public void Init()
    {
        btnStartBattle.onClick.RemoveAllListeners();
        btnStartBattle.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("BattleStart", null);
        });

        btnStartPlant.onClick.RemoveAllListeners();
        btnStartPlant.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("PeacePlantStart", null);
        });

    }

    public void ShowPopup()
    {
        objPopup.SetActive(true);
    }

    public void HidePopup()
    {
        objPopup.SetActive(false);
    }

    public void ScrollHide()
    {
        objPopup.SetActive(false);

    }

    public void ScrollShow()
    {
        objPopup.SetActive(true);

    }
}
