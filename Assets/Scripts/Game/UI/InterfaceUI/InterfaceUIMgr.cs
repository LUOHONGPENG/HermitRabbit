using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceUIMgr : MonoBehaviour
{
    public BattleOptionUIMgr battleOptionUIMgr;

    public void Init()
    {
        battleOptionUIMgr.Init();
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.AddEventListener("RefreshCharacterInfo", RefreshCharacterInfoEvent);
        EventCenter.Instance.AddEventListener("CharacterActionStart", CharacterActionStartEvent);
        EventCenter.Instance.AddEventListener("CharacterActionEnd", CharacterActionEndEvent);
        EventCenter.Instance.AddEventListener("CharacterPhaseStart", CharacterPhaseStartEvent);
        EventCenter.Instance.AddEventListener("CharacterPhaseEnd", CharacterPhaseEndEvent);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.RemoveEventListener("RefreshCharacterInfo", RefreshCharacterInfoEvent);
        EventCenter.Instance.RemoveEventListener("CharacterActionStart", CharacterActionStartEvent);
        EventCenter.Instance.RemoveEventListener("CharacterActionEnd", CharacterActionEndEvent);
        EventCenter.Instance.RemoveEventListener("CharacterPhaseStart", CharacterPhaseStartEvent);
        EventCenter.Instance.RemoveEventListener("CharacterPhaseEnd", CharacterPhaseEndEvent);

    }

    private void InputChooseCharacterEvent(object arg0)
    {
        BattleCharacterData characterData = (BattleCharacterData)PublicTool.GetGameData().GetDataFromUnitInfo(new UnitInfo(BattleUnitType.Character, (int)arg0));
        battleOptionUIMgr.ShowBattleOptionPage(characterData);
    }

    private void RefreshCharacterInfoEvent(object arg0)
    {
        battleOptionUIMgr.RefreshCharacterInfo();
    }

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

    }

    private void CharacterPhaseEndEvent(object arg0)
    {
        battleOptionUIMgr.HideBattleOptionPage();
    }



}
