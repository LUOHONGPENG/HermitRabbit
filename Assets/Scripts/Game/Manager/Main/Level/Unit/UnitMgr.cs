using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMgr : MonoBehaviour
{
    [Header("Character")]
    public Transform tfCharacter;
    public GameObject pfCharacter;
    //public List<BattleCharacterView>  
    public Dictionary<int, BattleCharacterView> dicCharacter = new Dictionary<int, BattleCharacterView>();

    [Header("Plant")]
    public Transform tfPlant;
    public GameObject pfPlant;

    [Header("Foe")]
    public Transform tfFoe;
    public GameObject pfFoe;

    private LevelMgr parent;
    private bool isInit = false;

    #region Bind

    public void Init(LevelMgr parent)
    {
        this.parent = parent;

        InitCharacterView();

        this.isInit = true;
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.AddEventListener("InputMoveAction", InputMoveActionEvent);

    }



    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.RemoveEventListener("InputMoveAction", InputMoveActionEvent);

    }

    #endregion

    #region Character
    public void InitCharacterView()
    {
        dicCharacter.Clear();
        PublicTool.ClearChildItem(tfCharacter);
        foreach(var character in parent.GetLevelData().listCharacter)
        {
            GenerateCharacterView(character);
        }
    }

    public void GenerateCharacterView(BattleCharacterData characterData)
    {
        GameObject objCharacter = GameObject.Instantiate(pfCharacter, tfCharacter);
        BattleCharacterView characterView = objCharacter.GetComponent<BattleCharacterView>();
        characterView.Init(characterData);
        if (!dicCharacter.ContainsKey(characterView.characterData.keyID))
        {
            dicCharacter.Add(characterView.characterData.keyID,characterView);
        }
    }

    public BattleCharacterView GetCharacterView(int keyID)
    {
        if (dicCharacter.ContainsKey(keyID))
        {
            return dicCharacter[keyID];
        }
        else
        {
            return null;
        }
    }

    #endregion

    #region Choose Unit
    /// <summary>
    /// Recording curActionUnit
    /// </summary>
    private UnitInfo curBattleUnitInfo = new UnitInfo(BattleUnitType.Character, -1);

    private void InputChooseCharacterEvent(object arg0)
    {
        SetCurUnitInfo(BattleUnitType.Character, (int)arg0);
    }

    public UnitInfo GetCurUnitInfo()
    {
        return curBattleUnitInfo;
    }
    public void SetCurUnitInfo(BattleUnitType type, int keyID)
    {
        curBattleUnitInfo.type = type;
        curBattleUnitInfo.keyID = keyID;
    }

    public BattleUnitData GetCurUnitData()
    {
        LevelData levelData = parent.GetLevelData();
        return levelData.GetDataFromUnitInfo(curBattleUnitInfo);
    }

    public BattleUnitView GetViewFromUnitInfo(UnitInfo unitInfo)
    {
        Debug.Log(unitInfo.type + " " + unitInfo.keyID);
        switch (unitInfo.type)
        {
            case BattleUnitType.Character:
                return GetCharacterView(unitInfo.keyID);
        }
        return null;
    }
    #endregion

    #region MoveAction
    private void InputMoveActionEvent(object arg0)
    {
        Vector2Int targetPos = (Vector2Int)arg0;
        InvokeAction_MoveUnit(curBattleUnitInfo, targetPos);
    }

    public void InvokeAction_MoveUnit(UnitInfo unitInfo,Vector2Int targetPos)
    {
        LevelData levelData = parent.GetLevelData();
        BattleUnitData unitData = levelData.GetDataFromUnitInfo(unitInfo);
        if (unitData.listValidMove.Contains(targetPos))
        {
            //Data
            unitData.posID = targetPos;
            //View
            BattleUnitView unitView = GetViewFromUnitInfo(unitInfo);
            if (unitView != null)
            {
                unitView.MoveToPos(targetPos);
                EventCenter.Instance.EventTrigger("RefreshPosInfo", null);
            }
        }
    }
    #endregion
}
