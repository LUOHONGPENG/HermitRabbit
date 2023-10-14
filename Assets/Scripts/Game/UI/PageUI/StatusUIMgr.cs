using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class StatusUIMgr : MonoBehaviour
{
    [Header("Basic")]
    public GameObject objPopup;
    public Button btnChoose1001;
    public Button btnChoose1002;
    public Button btnClose;

    [Header("Status")]
    public Text codeLevel;
    public Text codeSPLeft;
    public Text codeEXP;
    public Image imgFillEXP;
    public Text codeHP;
    public Text codeATK;
    public Text codeDEF;
    public Text codeRES;

    [Header("SkillNode")]
    public List<Transform> listTfNode;
    public GameObject pfNode;
    public List<SkillNodeUIItem> listNodeUI = new List<SkillNodeUIItem>();

    private BattleCharacterData characterData;

    public void Init() 
    {
        characterData = null;

        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(delegate ()
        {
            HidePopup();
        });

        btnChoose1001.onClick.RemoveAllListeners();
        btnChoose1001.onClick.AddListener(delegate ()
        {
            characterData = PublicTool.GetGameData().GetBattleCharacterData(1001);
            RefreshCharacterInfo();
        });

        btnChoose1002.onClick.RemoveAllListeners();
        btnChoose1002.onClick.AddListener(delegate ()
        {
            characterData = PublicTool.GetGameData().GetBattleCharacterData(1002);
            RefreshCharacterInfo();
        });
    }

    #region BasicShowHide
    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("ShowStatusPage", ShowStatusEvent);
        EventCenter.Instance.AddEventListener("RefreshSkillTreeUI", UpdateSkillTreeEvent);
    }

    public void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("ShowStatusPage", ShowStatusEvent);
        EventCenter.Instance.RemoveEventListener("RefreshSkillTreeUI", UpdateSkillTreeEvent);

    }

    private void ShowStatusEvent(object arg0)
    {
        ShowPopup();
    }

    public void ShowPopup()
    {
        objPopup.SetActive(true);
        characterData = PublicTool.GetGameData().GetBattleCharacterData(1001);
        RefreshCharacterInfo();
    }

    public void HidePopup()
    {
        objPopup.SetActive(false);
    }
    #endregion

    public void RefreshCharacterInfo()
    {
        if (characterData != null)
        {
            codeLevel.text = characterData.Level.ToString();
            codeSPLeft.text = characterData.SPLeft.ToString();
            if (characterData.CheckWhetherMaxLevel())
            {
                codeEXP.text = "MAX";
                imgFillEXP.fillAmount = 1;
            }
            else
            {
                codeEXP.text = string.Format("{0}/{1}", characterData.curEXP, characterData.requiredEXP);
                imgFillEXP.fillAmount = 1f* characterData.curEXP / characterData.requiredEXP;

            }
            codeHP.text = characterData.maxHP.ToString();
            codeATK.text = characterData.ATK.ToString();
            codeDEF.text = characterData.DEF.ToString();
            codeRES.text = characterData.RES.ToString();

            InitSkillTree(characterData.typeID);
        }
    }

    #region Skill Tree

    public void InitSkillTree(int characterID)
    {
        //Clear
        foreach (Transform tf in listTfNode)
        {
            PublicTool.ClearChildItem(tf);
        }

        listNodeUI.Clear();
        List<SkillNodeExcelItem> listSkillNode = ExcelDataMgr.Instance.skillNodeExcelData.GetSkillNodeList(characterID);

        for(int i = 0; i < listSkillNode.Count; i++)
        {
            SkillNodeExcelItem nodeItem = listSkillNode[i];
            GameObject objNode = GameObject.Instantiate(pfNode, listTfNode[nodeItem.rowID]);
            SkillNodeUIItem itemNode = objNode.GetComponent<SkillNodeUIItem>();
            itemNode.Init(nodeItem);
            listNodeUI.Add(itemNode);
            itemNode.UpdateNodeUI();
        }
    }

    private void UpdateSkillTreeEvent(object arg0)
    {
        codeSPLeft.text = characterData.SPLeft.ToString();
        for (int i = 0; i < listNodeUI.Count; i++)
        {
            listNodeUI[i].UpdateNodeUI();
        }
    }
    #endregion
}
