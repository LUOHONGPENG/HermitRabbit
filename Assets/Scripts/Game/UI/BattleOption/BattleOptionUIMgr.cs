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
    private List<BattleSkillBtnItem> listSkillBtn = new List<BattleSkillBtnItem>();

    private BattleCharacterData curCharacterData;

    public void Init()
    {
        btnMove.Init(BattleBasicBtnItem.BattleBasicBtnType.Move);
        btnMove.InitButton(delegate ()
        {
            EventCenter.Instance.EventTrigger("InputChangeSkill", null);
            PublicTool.EventChangeInteract(InteractState.CharacterMove);
        });
        btnAttack.Init(BattleBasicBtnItem.BattleBasicBtnType.Attack);
        btnAttack.InitButton(delegate ()
        {
            EventCenter.Instance.EventTrigger("InputChangeSkill", null);
            PublicTool.EventChangeInteract(InteractState.CharacterSkill, curCharacterData.GetItem().AttackID);
        });

        infoBarHealth.Init(BarResourceType.Health);
        infoBarSkill.Init(BarResourceType.Skill);
        infoBarMove.Init(BarResourceType.Move);


    }

    private void Update()
    {
        if (objPopup.activeSelf)
        {
            RefreshButton();
        }
    }

    #region Show&Hide

    public void ShowBattleOptionPage(BattleCharacterData characterData)
    {
        //Set Character
        curCharacterData = characterData;
        //Portrait Part
        imgPortrait.sprite = Resources.Load(curCharacterData.GetItem().portraitUrl, typeof(Sprite)) as Sprite;
        RefreshBarInfo();
        //Skill Part
        PublicTool.ClearChildItem(tfSkillButton);
        listSkillBtn.Clear();
        List<SkillExcelItem> listSkill = ExcelDataMgr.Instance.skillExcelData.dicAllCharacterSkill[curCharacterData.typeID];
        for (int i = 0; i < listSkill.Count; i++)
        {
            GameObject objSkill = GameObject.Instantiate(pfSkillButton, tfSkillButton);
            BattleSkillBtnItem itemSkill = objSkill.GetComponent<BattleSkillBtnItem>();
            itemSkill.Init(listSkill[i]);
            listSkillBtn.Add(itemSkill);
        }
        ShowAction();
        objPopup.SetActive(true);
    }

    public void HideBattleOptionPage()
    {
        objPopup.SetActive(false);
    }

    public void ShowAction()
    {
        groupActionButton.interactable = true;
        groupActionButton.DOFade(1, 0.5f);
    }

    public void HideAction()
    {
        groupActionButton.interactable = false;
        groupActionButton.DOFade(0, 0.5f);
    }

    #endregion

    #region Refresh
    public void RefreshCharacterInfo()
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

    public void RefreshButton()
    {


        switch (InputMgr.Instance.GetInteractState())
        {
            case InteractState.CharacterMove:
                btnMove.RefreshOnSelect();
                btnAttack.RefreshOffSelect();
                foreach(BattleSkillBtnItem item in listSkillBtn)
                {
                    item.RefreshOffSelect();
                }
                break;
            case InteractState.CharacterSkill:
                btnMove.RefreshOffSelect();
                if (PublicTool.GetGameData().GetCurSkillBattleInfo().isNormalAttack)
                {
                    btnAttack.RefreshOnSelect();
                    foreach (BattleSkillBtnItem item in listSkillBtn)
                    {
                        item.RefreshOffSelect();
                    }
                }
                else
                {
                    btnAttack.RefreshOffSelect();
                    foreach (BattleSkillBtnItem item in listSkillBtn)
                    {
                        if (PublicTool.GetGameData().GetCurSkillBattleInfo().ID == item.GetSkillItem().id)
                        {
                            item.RefreshOnSelect();
                        }
                        else
                        {
                            item.RefreshOffSelect();
                        }
                    }
                }
                break;
        }
    }

    #endregion

}
