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
    public Button btnShowStatus;
    public Button btnStartPlant;
    public Button btnStartMap;

    public void Init()
    {
        btnStartBattle.onClick.RemoveAllListeners();
        btnStartBattle.onClick.AddListener(delegate ()
        {
            PublicTool.GetGameData().gamePhase = GamePhase.Battle;
            EventCenter.Instance.EventTrigger("BattleStart", null);
        });

        btnShowStatus.onClick.RemoveAllListeners();
        btnShowStatus.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("ShowStatusPage", null);
        });

        btnStartPlant.onClick.RemoveAllListeners();
        btnStartPlant.onClick.AddListener(delegate ()
        {
            PublicTool.EventChangeInteract(InteractState.PeacePlant);
            EventCenter.Instance.EventTrigger("PeacePlantStart", null);
        });

        btnStartMap.onClick.RemoveAllListeners();
        btnStartMap.onClick.AddListener(delegate ()
        {
            PublicTool.EventChangeInteract(InteractState.PeaceMap);
            EventCenter.Instance.EventTrigger("PeaceMapStart", null);
        });
    }

    public void ShowPopup()
    {
        if(PublicTool.GetGameData().numDay == 2)
        {
            btnStartPlant.gameObject.SetActive(false);
        }
        else
        {
            btnStartPlant.gameObject.SetActive(true);
        }

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
