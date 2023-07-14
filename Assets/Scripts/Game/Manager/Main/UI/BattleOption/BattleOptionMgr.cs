using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleOptionMgr : MonoBehaviour
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

    //public Stack<BattleOptionPage> stackPage = new Stack<BattleOptionPage>();

    public BattleCharacterData curCharacterData;

    public void Init()
    {
        btnMove.onClick.RemoveAllListeners();
        btnMove.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("ChangeInteract", InteractState.Move);
        });

        btnAttack.onClick.RemoveAllListeners();
        btnAttack.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("ChangeInteract", InteractState.Target);
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
        curCharacterData = (BattleCharacterData)PublicTool.GetLevelData().GetDataFromUnitInfo(new UnitInfo(BattleUnitType.Character, (int)arg0));

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
            infoBarSkill.UpdateData(curCharacterData.curSP, curCharacterData.maxSP);
            infoBarMove.UpdateData(curCharacterData.curMOV, curCharacterData.maxMOV);
        }
    }
}
