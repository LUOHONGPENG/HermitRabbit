using System;
using System.Collections;
using System.Collections.Generic;
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
    public Text codeATK;
    public Text codeDEF;
    public Text codeRES;

    [Header("SkillNode")]
    public List<Transform> listTfNode;
    public GameObject pfNode;

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
    }

    public void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("ShowStatusPage", ShowStatusEvent);
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
            InitSkillTree(characterData.typeID);
        }
    }

    #region Skill Tree

    public void InitSkillTree(int characterID)
    {
        //Clear
        foreach(Transform tf in listTfNode)
        {
            PublicTool.ClearChildItem(tf);
        }

        List<SkillNodeExcelItem> listSkillNode = ExcelDataMgr.Instance.skillNodeExcelData.dicAllCharacterSkillNode[characterID];

        for(int i = 0; i < listSkillNode.Count; i++)
        {
            SkillNodeExcelItem nodeItem = listSkillNode[i];
            GameObject objNode = GameObject.Instantiate(pfNode, listTfNode[nodeItem.rowID]);
            SkillNodeUIItem itemNode = objNode.GetComponent<SkillNodeUIItem>();
            itemNode.Init(nodeItem);

        }

    }
    #endregion
}
