using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitViewMgr : MonoBehaviour
{
    [Header("Character")]
    public Transform tfCharacter;
    public GameObject pfCharacter;
    public Dictionary<int, BattleCharacterView> dicCharacterView = new Dictionary<int, BattleCharacterView>();

    [Header("Plant")]
    public Transform tfPlant;
    public GameObject pfPlant;

    [Header("Foe")]
    public Transform tfFoe;
    public GameObject pfFoe;
    public Dictionary<int, BattleFoeView> dicFoeView = new Dictionary<int, BattleFoeView>();

    private bool isInit = false;

    #region Bind
    public void Init()
    {
        InitCharacterView();
        this.isInit = true;
    }

    #endregion

    #region Character
    public void InitCharacterView()
    {
        dicCharacterView.Clear();
        PublicTool.ClearChildItem(tfCharacter);
        foreach(var character in PublicTool.GetGameData().listCharacter)
        {
            GenerateCharacterView(character);
        }
    }

    public void GenerateCharacterView(BattleCharacterData characterData)
    {
        GameObject objCharacter = GameObject.Instantiate(pfCharacter, tfCharacter);
        BattleCharacterView characterView = objCharacter.GetComponent<BattleCharacterView>();
        characterView.Init(characterData);
        if (!dicCharacterView.ContainsKey(characterView.characterData.keyID))
        {
            dicCharacterView.Add(characterView.characterData.keyID,characterView);
        }
        PublicTool.EventRefreshOccupancy();
    }

    public BattleCharacterView GetCharacterView(int keyID)
    {
        if (dicCharacterView.ContainsKey(keyID))
        {
            return dicCharacterView[keyID];
        }
        else
        {
            return null;
        }
    }
    public BattleUnitView GetViewFromUnitInfo(UnitInfo unitInfo)
    {
        Debug.Log(unitInfo.type + " " + unitInfo.keyID);
        switch (unitInfo.type)
        {
            case BattleUnitType.Character:
                return GetCharacterView(unitInfo.keyID);
            case BattleUnitType.Foe:
                return GetFoeView(unitInfo.keyID);
        }
        return null;
    }

    #endregion

    #region Foe
    public void GenerateFoeView(BattleFoeData foeData)
    {
        GameObject objFoe = GameObject.Instantiate(pfFoe, tfFoe);
        BattleFoeView foeView = objFoe.GetComponent<BattleFoeView>();
        foeView.Init(foeData);
        if (!dicFoeView.ContainsKey(foeView.foeData.keyID))
        {
            dicFoeView.Add(foeView.foeData.keyID, foeView);
        }
        PublicTool.EventRefreshOccupancy();
    }

    public BattleFoeView GetFoeView(int keyID)
    {
        if (dicFoeView.ContainsKey(keyID))
        {
            return dicFoeView[keyID];
        }
        else
        {
            return null;
        }
    }

    public void RemoveFoeView(int keyID)
    {
        if (dicFoeView.ContainsKey(keyID))
        {
            BattleFoeView view = dicFoeView[keyID];
            dicFoeView.Remove(keyID);
            view.SelfDestroy();
        }
    }
    #endregion

    #region ActionDeal

    public void InvokeAction_SelfMove(Vector2Int targetPos)
    {
        GameData gameData = PublicTool.GetGameData();
        UnitInfo curUnitInfo = gameData.GetCurUnitInfo();
        //Data
        BattleUnitData unitData = gameData.GetCurUnitData();
        if (unitData.listValidMove.Contains(targetPos))
        {
            int cost = PublicTool.CalculateGlobalDis(unitData.posID, targetPos);
            //View
            BattleUnitView unitView = GetViewFromUnitInfo(curUnitInfo);
            if (unitView != null)
            {
                //Data Move
                unitData.posID = targetPos;
                unitData.curMOV -= cost;
                //View Move
                unitView.MoveToPos(targetPos);
                PublicTool.EventRefreshOccupancy();
                EventCenter.Instance.EventTrigger("RefreshCharacterInfo", null);
            }
        }
    }

    public void InvokeAction_Skill(int SkillID,Vector2Int targetPos)
    {

    }
    #endregion
}
