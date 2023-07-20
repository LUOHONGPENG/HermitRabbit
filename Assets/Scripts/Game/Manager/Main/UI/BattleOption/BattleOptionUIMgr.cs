using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleOptionUIMgr : MonoBehaviour
{
    public GameObject objPopup;

    [Header("CharacterInfo")]
    public Image imgPortrait;
    public BattleInfoBarItem infoBarHealth;
    public BattleInfoBarItem infoBarSkill;
    public BattleInfoBarItem infoBarMove;

    [Header("Action")]
    public Button btnMove;
    public Button btnAttack;

    public BattleCharacterData curCharacterData;

    public void Init()
    {
        btnMove.onClick.RemoveAllListeners();
        btnMove.onClick.AddListener(delegate ()
        {
            PublicTool.EventChangeInteract(InteractState.Move);
        });

        btnAttack.onClick.RemoveAllListeners();
        btnAttack.onClick.AddListener(delegate ()
        {
            PublicTool.EventChangeInteract(InteractState.Skill, curCharacterData.GetItem().AttackID);
        });

        infoBarHealth.Init(BarResourceType.Health);
        infoBarSkill.Init(BarResourceType.Skill);
        infoBarMove.Init(BarResourceType.Move);
    }

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.AddEventListener("RefreshCharacterInfo", RefreshCharacterInfoEvent);
    }


    public void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.RemoveEventListener("RefreshCharacterInfo", RefreshCharacterInfoEvent);
    }

    public void InputChooseCharacterEvent(object arg0)
    {
        curCharacterData = (BattleCharacterData)PublicTool.GetGameData().GetDataFromUnitInfo(new UnitInfo(BattleUnitType.Character, (int)arg0));

        imgPortrait.sprite = Resources.Load(curCharacterData.GetItem().portraitUrl, typeof(Sprite)) as Sprite;

        RefreshBarInfo();
        objPopup.SetActive(true);
    }

    private void RefreshCharacterInfoEvent(object arg0)
    {
        RefreshBarInfo();
    }

    public void RefreshBarInfo()
    {
        if (curCharacterData != null)
        {
            infoBarHealth.UpdateData(curCharacterData.curHP, curCharacterData.maxHP);
            infoBarSkill.UpdateData(curCharacterData.curAP, curCharacterData.maxAP);
            infoBarMove.UpdateData(curCharacterData.curMOV, curCharacterData.maxMOV);
        }
    }
}
