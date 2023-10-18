using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceUIMgr : MonoBehaviour
{
    public BattleInterfaceUIMgr battleInterfaceUIMgr;
    public BattleOptionUIMgr battleOptionUIMgr;
    public PeaceInterfaceUIMgr peaceInterfaceUIMgr;
    public PeacePlantUIMgr peacePlantUIMgr;
    public PeaceMapClipUIMgr peaceMapClipUIMgr;

    public Text txPhase;
    public Text txDayInfo;

    private bool isInit = false;

    public void Init()
    {
        battleOptionUIMgr.Init();
        battleInterfaceUIMgr.Init();
        peaceInterfaceUIMgr.Init();


        if (InputMgr.Instance.GetInteractState() == InteractState.PeaceNormal)
        {
            peaceInterfaceUIMgr.ShowPopup();
            battleOptionUIMgr.HideBattleOptionPage();
            battleInterfaceUIMgr.HidePopup();
        }
        RefreshPhaseUI();
        isInit = true;
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.AddEventListener("RefreshCharacterInfo", RefreshCharacterInfoEvent);
        EventCenter.Instance.AddEventListener("InputCancelCharacter", InputCancelCharacterEvent);
        //Battle
        EventCenter.Instance.AddEventListener("BattleStart", BattleStartEvent);
        EventCenter.Instance.AddEventListener("BattleEnd", BattleEndEvent);

        EventCenter.Instance.AddEventListener("CharacterActionStart", CharacterActionStartEvent);
        EventCenter.Instance.AddEventListener("CharacterActionEnd", CharacterActionEndEvent);
        EventCenter.Instance.AddEventListener("CharacterPhaseStart", CharacterPhaseStartEvent);
        EventCenter.Instance.AddEventListener("CharacterPhaseEnd", CharacterPhaseEndEvent);
        //Peace
        EventCenter.Instance.AddEventListener("PeacePlantStart", PeacePlantStartEvent);
        EventCenter.Instance.AddEventListener("PeacePlantEnd", PeacePlantEndEvent);

    }



    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.RemoveEventListener("RefreshCharacterInfo", RefreshCharacterInfoEvent);
        EventCenter.Instance.RemoveEventListener("InputCancelCharacter", InputCancelCharacterEvent);
        //Battle
        EventCenter.Instance.RemoveEventListener("BattleStart", BattleStartEvent);
        EventCenter.Instance.RemoveEventListener("BattleEnd", BattleEndEvent);

        EventCenter.Instance.RemoveEventListener("CharacterActionStart", CharacterActionStartEvent);
        EventCenter.Instance.RemoveEventListener("CharacterActionEnd", CharacterActionEndEvent);
        EventCenter.Instance.RemoveEventListener("CharacterPhaseStart", CharacterPhaseStartEvent);
        EventCenter.Instance.RemoveEventListener("CharacterPhaseEnd", CharacterPhaseEndEvent);

        EventCenter.Instance.RemoveEventListener("PeacePlantStart", PeacePlantStartEvent);
        EventCenter.Instance.RemoveEventListener("PeacePlantEnd", PeacePlantEndEvent);

    }



    private void RefreshCharacterInfoEvent(object arg0)
    {
        battleOptionUIMgr.RefreshCharacterInfo();
        battleInterfaceUIMgr.RefreshCharacterInfo();
    }

    #region BattleRelated Event
    /// <summary>
    /// When player choose a character
    /// </summary>
    /// <param name="arg0"></param>
    private void InputChooseCharacterEvent(object arg0)
    {
        BattleCharacterData characterData = (BattleCharacterData)PublicTool.GetGameData().GetDataFromUnitInfo(new UnitInfo(BattleUnitType.Character, (int)arg0));
        battleOptionUIMgr.ShowBattleOptionPage(characterData);
    }

    private void InputCancelCharacterEvent(object arg0)
    {
        battleOptionUIMgr.HideBattleOptionPage();
    }

    /// <summary>
    /// When a character's action end
    /// </summary>
    /// <param name="arg0"></param>
    private void CharacterActionEndEvent(object arg0)
    {
        battleOptionUIMgr.ShowAction();
    }

    private void CharacterActionStartEvent(object arg0)
    {
        battleOptionUIMgr.HideAction();
    }

    private void CharacterPhaseStartEvent(object arg0)
    {
        battleInterfaceUIMgr.ShowEndTurnBtn();
        RefreshPhaseUI();
    }

    private void CharacterPhaseEndEvent(object arg0)
    {
        battleOptionUIMgr.HideBattleOptionPage();
    }

    private void BattleStartEvent(object arg0)
    {
        peaceInterfaceUIMgr.HidePopup();

        battleInterfaceUIMgr.ShowPopup();
        battleInterfaceUIMgr.BindCharacterData();

        RefreshPhaseUI();
    }

    private void BattleEndEvent(object arg0)
    {
        peaceInterfaceUIMgr.ShowPopup();

        battleInterfaceUIMgr.HidePopup();
        battleOptionUIMgr.HideBattleOptionPage();

        RefreshPhaseUI();
    }
    #endregion


    #region Peace
    private void PeacePlantStartEvent(object arg0)
    {
        peaceInterfaceUIMgr.ScrollHide();
        peacePlantUIMgr.ShowPopup();
    }

    private void PeacePlantEndEvent(object arg0)
    {
        peaceInterfaceUIMgr.ScrollShow();
        peacePlantUIMgr.HidePopup();
    }
    #endregion

    private void RefreshPhaseUI()
    {
        if (PublicTool.GetGameData().gamePhase == GamePhase.Peace)
        {
            txPhase.text = "Peace";
            txDayInfo.text = String.Format("Day {0}", PublicTool.GetGameData().numDay.ToString());
        }
        else if (PublicTool.GetGameData().gamePhase == GamePhase.Battle)
        {
            txPhase.text = "Battle";
            txDayInfo.text = String.Format("Turn {0}", BattleMgr.Instance.numTurn.ToString());
        }
    }
}
