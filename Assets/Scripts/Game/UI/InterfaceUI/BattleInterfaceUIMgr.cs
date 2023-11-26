using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleInterfaceUIMgr : MonoBehaviour
{
    public GameObject objPopup;

    public BattleMiniCharacterUIItem miniCharacterUI1001;
    public BattleMiniCharacterUIItem miniCharacterUI1002;

    public Button btnEndTurn;

    public void Init()
    {
        btnEndTurn.onClick.RemoveAllListeners();
        btnEndTurn.onClick.AddListener(delegate() {

            switch (InputMgr.Instance.interactState)
            {
                case InteractState.BattleNormal:
                case InteractState.CharacterMove:
                case InteractState.CharacterSkill:
                    EventCenter.Instance.EventTrigger("CharacterPhaseEnd", null);
                    HideEndTurnBtn();
                    PublicTool.EventReadyAni(-1);
                    break;
            }

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
        miniCharacterUI1002.Init(gameData.GetBattleCharacterData(1002));

        miniCharacterUI1001.RefreshButton(false);
        miniCharacterUI1002.RefreshButton(false);
    }

    public void RefreshCharacterInfo()
    {
        miniCharacterUI1001.RefreshUI();
        miniCharacterUI1002.RefreshUI();
    }
    #endregion
}
