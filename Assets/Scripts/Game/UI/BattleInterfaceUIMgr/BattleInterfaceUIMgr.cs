using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleInterfaceUIMgr : MonoBehaviour
{
    public GameObject objPopup;
    public Button btnEndTurn;

    public void Init()
    {
        btnEndTurn.onClick.RemoveAllListeners();
        btnEndTurn.onClick.AddListener(delegate() {
            EventCenter.Instance.EventTrigger("InputEndCharacterPhase", null);
        });
    }

    #region Show & Hide
    public void ShowPopup()
    {
        objPopup.SetActive(true);
    }

    public void HidePopup()
    {
        objPopup.SetActive(false);
    }

    public void ShowEndTurnBtn()
    {

    }

    public void HideEndTurnBtn()
    {

    }
    #endregion
}
