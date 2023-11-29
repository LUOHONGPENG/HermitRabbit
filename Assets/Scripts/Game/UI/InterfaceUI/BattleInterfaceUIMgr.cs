using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class BattleInterfaceUIMgr : MonoBehaviour
{
    public GameObject objPopup;

    public BattleMiniCharacterUIItem miniCharacterUI1001;
    public BattleMiniCharacterUIItem miniCharacterUI1002;

    public Button btnSwitch;
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

        btnSwitch.onClick.RemoveAllListeners();
        btnSwitch.onClick.AddListener(delegate () {

            switch (InputMgr.Instance.interactState)
            {
                case InteractState.BattleNormal:
                case InteractState.CharacterMove:
                case InteractState.CharacterSkill:
                    GameData gameData = PublicTool.GetGameData();

                    UnitInfo info = gameData.GetCurUnitInfo();
                    if (info.type == BattleUnitType.Character && info.keyID == 1001)
                    {
                        if (!gameData.GetBattleCharacterData(1002).isDead)
                        {
                            EventCenter.Instance.EventTrigger("InputChooseCharacter", 1002);

                        }
                        else if(!gameData.GetBattleCharacterData(1001).isDead)
                        {
                            EventCenter.Instance.EventTrigger("InputChooseCharacter", 1001);
                        }
                    }
                    else if (info.type == BattleUnitType.Character && info.keyID == 1002)
                    {
                        if (!gameData.GetBattleCharacterData(1001).isDead)
                        {
                            EventCenter.Instance.EventTrigger("InputChooseCharacter", 1001);
                        }
                        else if (!gameData.GetBattleCharacterData(1002).isDead)
                        {
                            EventCenter.Instance.EventTrigger("InputChooseCharacter", 1002);

                        }
                    }
                    else
                    {
                        List<BattleCharacterData> listCharacter = PublicTool.GetGameData().listCharacter;
                        for (int i = 0; i < listCharacter.Count; i++)
                        {
                            if (!listCharacter[i].isDead)
                            {
                                EventCenter.Instance.EventTrigger("InputChooseCharacter", listCharacter[i].keyID);
                                break;
                            }
                        }
                    }
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
