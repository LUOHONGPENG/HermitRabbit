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
    public Image imgBtn1001;
    public Image imgBtn1002;

    public Button btnChoose1001;
    public Button btnChoose1002;
    public Image imgSelect1001;
    public Image imgSelect1002;

    public Button btnClose;
    public Button btnReset;

    [Header("Status")]
    public Text codeName;
    public Text codeLevel;
    public Text codeSPLeft;
    public Text codeEXP;
    public Image imgFillEXP;
    public Text codeHP;
    public Text codeATK;
    public Text codeDEF;
    public Text codeRES;
    public Text codeMOV;

    [Header("SkillNode")]
    public Transform tfNode;
    public GameObject pfNode;
    public List<SkillNodeUIItem> listNodeUI = new List<SkillNodeUIItem>();
    public Dictionary<int, SkillNodeUIItem> dicNodeUI = new Dictionary<int, SkillNodeUIItem>();

    [Header("SkillLine")]
    public Transform tfLine;
    public GameObject pfLine;
    public GameObject pfConflict;
    public List<SkillLineUIItem> listLineUI = new List<SkillLineUIItem>();

    private BattleCharacterData characterData;

    public void Init() 
    {
        characterData = null;

        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(delegate ()
        {
            HidePopup();
        });

        imgBtn1001.sprite = Resources.Load("Sprite/CharacterIcon/" + PublicTool.GetCharacterExcelItem(1001).iconUrl, typeof(Sprite)) as Sprite;
        imgBtn1002.sprite = Resources.Load("Sprite/CharacterIcon/" + PublicTool.GetCharacterExcelItem(1002).iconUrl, typeof(Sprite)) as Sprite;

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

        btnReset.onClick.RemoveAllListeners();
        btnReset.onClick.AddListener(delegate ()
        {
            characterData.ResetSkillNode();
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

    public void RefreshSelect()
    {
        if(characterData.typeID == 1001)
        {
            imgSelect1001.gameObject.SetActive(true);
            imgSelect1002.gameObject.SetActive(false);
        }
        else if(characterData.typeID == 1002)
        {
            imgSelect1001.gameObject.SetActive(false);
            imgSelect1002.gameObject.SetActive(true);
        }
        else
        {
            imgSelect1001.gameObject.SetActive(false);
            imgSelect1002.gameObject.SetActive(false);
        }
    }

    public void HidePopup()
    {
        objPopup.SetActive(false);
    }
    #endregion

    #region Refresh
    public void RefreshCharacterInfo()
    {
        RefreshCharacterStatus();
        InitSkillTree();
        RefreshSelect();

    }

    public void RefreshCharacterStatus()
    {
        if (characterData != null)
        {
            codeName.text = PublicTool.GetCharacterExcelItem(characterData.typeID).name;
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
                imgFillEXP.fillAmount = 1f * characterData.curEXP / characterData.requiredEXP;

            }
            codeHP.text = characterData.curMaxHP.ToString();
            codeATK.text = characterData.curATK.ToString();
            codeDEF.text = characterData.curDEF.ToString();
            codeRES.text = characterData.curRES.ToString();
            codeMOV.text = characterData.curMOV.ToString();
        }
    }



    #endregion

    #region Skill Tree

    public void InitSkillTree()
    {
        //Check Character ID
        int characterID = -1;
        if (characterData == null)
        {
            return;
        }
        characterID = characterData.typeID;

        //ClearSkillNode
        PublicTool.ClearChildItem(tfNode);
        PublicTool.ClearChildItem(tfLine);
        listNodeUI.Clear();
        dicNodeUI.Clear();
        listLineUI.Clear();
        //Start Generating Skill Nodes
        List<SkillNodeExcelItem> listSkillNode = ExcelDataMgr.Instance.skillNodeExcelData.GetSkillNodeList(characterID);
        for(int i = 0; i < listSkillNode.Count; i++)
        {
            SkillNodeExcelItem nodeItem = listSkillNode[i];
            //Generate Skill Node and Set the position
            GameObject objNode = GameObject.Instantiate(pfNode, tfNode);
            objNode.transform.localPosition = new Vector2(nodeItem.columnID * GameGlobal.skillNodeSpacingX, 400f - nodeItem.rowID * GameGlobal.skillNodeSpacingY);
            SkillNodeUIItem itemNode = objNode.GetComponent<SkillNodeUIItem>();
            itemNode.Init(nodeItem);
            itemNode.UpdateNodeUI();
            //Store the information
            listNodeUI.Add(itemNode);
            dicNodeUI.Add(nodeItem.id, itemNode);

            //Generate Skill Line if there is a node condition
            if(nodeItem.conditionPreNode.Count>=0 && nodeItem.conditionPreNode[0] != 0)
            {
                for(int j = 0;j < nodeItem.conditionPreNode.Count; j++)
                {
                    int endID = nodeItem.conditionPreNode[j];
                    if (!dicNodeUI.ContainsKey(endID))
                    {
                        continue;
                    }
                    GameObject objLine = GameObject.Instantiate(pfLine, tfLine);
                    SkillLineUIItem itemLine = objLine.GetComponent<SkillLineUIItem>();
                    itemLine.Init(itemNode.transform.localPosition, dicNodeUI[endID].transform.localPosition);
                    itemLine.Record(characterID, nodeItem.id, endID);
                    itemLine.Refresh();
                    listLineUI.Add(itemLine);
                }
            }

            //Generate Red Line if there is a conflict
            if (nodeItem.conflictNode != 0)
            {
                int conflictID = nodeItem.conflictNode;
                if (dicNodeUI.ContainsKey(conflictID))
                {
                    GameObject objConflict = GameObject.Instantiate(pfConflict, tfLine);
                    SkillLineUIItem itemConflict = objConflict.GetComponent<SkillLineUIItem>();
                    itemConflict.Init(itemNode.transform.localPosition, dicNodeUI[conflictID].transform.localPosition);
                }
            }
        }
    }

    private void UpdateSkillTreeEvent(object arg0)
    {
        RefreshCharacterStatus();
        for (int i = 0; i < listNodeUI.Count; i++)
        {
            listNodeUI[i].UpdateNodeUI();
        }

        for(int i = 0; i< listLineUI.Count; i++)
        {
            listLineUI[i].Refresh();
        }
    }
    #endregion
}
