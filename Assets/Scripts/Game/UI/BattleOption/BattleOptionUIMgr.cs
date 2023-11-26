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
    public BattleSkillBtnItem btnAttack;

    [Header("Skill")]
    public Image imgSkillText;
    public Transform tfSkillButton;
    public Transform tfSkillButtonExtra;
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
        imgPortrait.sprite = Resources.Load("Sprite/Portrait/"+curCharacterData.GetItem().portraitUrl, typeof(Sprite)) as Sprite;
        imgPortrait.SetNativeSize();
        RefreshBarInfo();
        //Attack Part
        SkillExcelItem attackItem = PublicTool.GetSkillItem(curCharacterData.GetItem().AttackID);
        btnAttack.Init(attackItem);
/*        btnAttack.InitButton(delegate ()
        {
            EventCenter.Instance.EventTrigger("InputChangeSkill", null);
            PublicTool.EventChangeInteract(InteractState.CharacterSkill, curCharacterData.GetItem().AttackID);
        });*/

        //Skill Part
        PublicTool.ClearChildItem(tfSkillButton);
        PublicTool.ClearChildItem(tfSkillButtonExtra);

        listSkillBtn.Clear();
        List<SkillExcelItem> listSkill = ExcelDataMgr.Instance.skillExcelData.dicAllCharacterSkill[curCharacterData.typeID];
        int counter = 0;
        for (int i = 0; i < listSkill.Count; i++)
        {
            SkillExcelItem skill = listSkill[i];
            if (PublicTool.CheckWhetherCharacterUnlockSkill(skill.characterID, skill.unlockNodeID))
            {
                Transform tf;
                if(counter % 2 == 0)
                {
                    tf = tfSkillButton;
                }
                else
                {
                    tf = tfSkillButtonExtra;
                }
                GameObject objSkill = GameObject.Instantiate(pfSkillButton, tf);
                BattleSkillBtnItem itemSkill = objSkill.GetComponent<BattleSkillBtnItem>();
                itemSkill.Init(listSkill[i]);
                listSkillBtn.Add(itemSkill);
                counter++;
            }
        }
        if (listSkill.Count > 0)
        {
            imgSkillText.gameObject.SetActive(true);
        }
        else
        {
            imgSkillText.gameObject.SetActive(false);
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
            infoBarHealth.UpdateData(curCharacterData.curHP, curCharacterData.curMaxHP);
            infoBarSkill.UpdateData(curCharacterData.curAP, curCharacterData.curMaxAP);
            infoBarMove.UpdateData(curCharacterData.curMOV, curCharacterData.curMaxMOV);
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
                if (PublicTool.GetGameData().GetCurSkillBattleInfo().activeSkillType == ActiveSkillType.NormalAttack)
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
