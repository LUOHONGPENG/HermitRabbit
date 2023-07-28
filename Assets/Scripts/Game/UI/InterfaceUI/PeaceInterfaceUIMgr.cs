using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeaceInterfaceUIMgr : MonoBehaviour
{
    public GameObject objPopup;
    public Button btnStartBattle;

    public void Init()
    {
        btnStartBattle.onClick.RemoveAllListeners();
        btnStartBattle.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("BattleStart", null);
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
}
