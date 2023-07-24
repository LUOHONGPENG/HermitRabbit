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
            PublicTool.EventChangeInteract(InteractState.CharacterMove);
        });
        btnAttack.Init(BattleBasicBtnItem.BattleBasicBtnType.Attack);
        btnAttack.InitButton(delegate ()
        {
            PublicTool.EventChangeInteract(InteractState.CharacterSkill, curCharacterData.GetItem().AttackID);
        });

        infoBarHealth.Init(BarResourceType.Health);
        infoBarSkill.Init(BarResourceType.Skill);
        infoBarMove.Init(BarResourceType.Move);

        //When player load the game and the game is in Normal phase
        if(InputMgr.Instance.interactState == InteractState.Normal)
        {
            HideBattleOptionPage();
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
        List<CharacterSkillExcelItem> listSkill = ExcelDataMgr.Instance.characterSkillExcelData.dicAllCharacterSkill[curCharacterData.typeID];
        for (int i = 0; i < listSkill.Count; i++)
        {
            GameObject objSkill = GameObject.Instantiate(pfSkillButton, tfSkillButton);
            BattleSkillBtnItem itemSkill = objSkill.GetComponent<BattleSkillBtnItem>();
            itemSkill.Init(listSkill[i]);
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
    #endregion

}
