using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleOptionUIMgr : MonoBehaviour
{
    public GameObject objPopup;
    public CanvasGroup groupActionButton;

    [Header("CharacterInfo")]
    public Image imgPortrait;
    public BattleInfoBarItem infoBarHealth;
    public BattleInfoBarItem infoBarSkill;
    public BattleInfoBarItem infoBarMove;

    [Header("Action")]
    public BattleBasicBtnItem btnMove;
    public BattleBasicBtnItem btnAttack;
    public Transform tfSkillButton;
    public GameObject pfSkillButton;

    private BattleCharacterData curCharacterData;

    public void Init()
    {
        btnMove.Init(BattleBasicBtnItem.BattleBasicBtnType.Move);
        btnMove.InitButton(delegate ()
        {
            PublicTool.EventChangeInteract(InteractState.Move);
        });
        btnAttack.Init(BattleBasicBtnItem.BattleBasicBtnType.Attack);
        btnAttack.InitButton(delegate ()
        {
            PublicTool.EventChangeInteract(InteractState.Skill, curCharacterData.GetItem().AttackID);
        });

        infoBarHealth.Init(BarResourceType.Health);
        infoBarSkill.Init(BarResourceType.Skill);
        infoBarMove.Init(BarResourceType.Move);

        if(InputMgr.Instance.interactState == InteractState.Normal)
        {
            HidePopup();
        }
    }

    public void HidePopup()
    {
        objPopup.SetActive(false);
    }

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.AddEventListener("RefreshCharacterInfo", RefreshCharacterInfoEvent);
        EventCenter.Instance.AddEventListener("CharacterSkillStart", CharacterSkillStartEvent);
        EventCenter.Instance.AddEventListener("CharacterSkillEnd", CharacterSkillEndEvent);

    }

    public void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.RemoveEventListener("RefreshCharacterInfo", RefreshCharacterInfoEvent);
        EventCenter.Instance.RemoveEventListener("CharacterSkillStart", CharacterSkillStartEvent);
        EventCenter.Instance.RemoveEventListener("CharacterSkillEnd", CharacterSkillEndEvent);

    }

    private void CharacterSkillStartEvent(object arg0)
    {
        groupActionButton.interactable = false;
        groupActionButton.DOFade(0, 0.5f);
    }

    private void CharacterSkillEndEvent(object arg0)
    {
        groupActionButton.interactable = true;
        groupActionButton.DOFade(1, 0.5f);
    }

    public void InputChooseCharacterEvent(object arg0)
    {
        curCharacterData = (BattleCharacterData)PublicTool.GetGameData().GetDataFromUnitInfo(new UnitInfo(BattleUnitType.Character, (int)arg0));
        //Portrait Part
        imgPortrait.sprite = Resources.Load(curCharacterData.GetItem().portraitUrl, typeof(Sprite)) as Sprite;
        RefreshBarInfo();
        //Skill Part
        PublicTool.ClearChildItem(tfSkillButton);
        List<CharacterSkillExcelItem> listSkill = ExcelDataMgr.Instance.characterSkillExcelData.dicAllCharacterSkill[curCharacterData.typeID];
        for(int i = 0;i < listSkill.Count; i++)
        {
            GameObject objSkill = GameObject.Instantiate(pfSkillButton, tfSkillButton);
            BattleSkillBtnItem itemSkill = objSkill.GetComponent<BattleSkillBtnItem>();
            itemSkill.Init(listSkill[i]);
        }
        objPopup.SetActive(true);
    }

    #region Refresh
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
    #endregion

}
