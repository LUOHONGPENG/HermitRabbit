using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleInterfaceUIMgr : MonoBehaviour
{
    public GameObject objPopup;

    public BattleMiniCharacterUIItem miniCharacterUI1001;
    public BattleMiniCharacterUIItem miniCharacterUI2001;

    public Button btnEndTurn;

    public void Init()
    {
        btnEndTurn.onClick.RemoveAllListeners();
        btnEndTurn.onClick.AddListener(delegate() {
            EventCenter.Instance.EventTrigger("CharacterPhaseEnd", null);
            HideEndTurnBtn();
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
        btnEndTurn.gameObject.SetActive(true);
    }

    public void HideEndTurnBtn()
    {
        btnEndTurn.gameObject.SetActive(false);
    }
    #endregion

    #region CharacterUI

    public void BindCharacterData()
    {
        GameData gameData = PublicTool.GetGameData();

        miniCharacterUI1001.Init(gameData.GetBattleCharacterData(1001));
        miniCharacterUI2001.Init(gameData.GetBattleCharacterData(2001));

    }

    public void RefreshCharacterInfo()
    {
        miniCharacterUI1001.RefreshUI();
        miniCharacterUI2001.RefreshUI();
    }
    #endregion
}
