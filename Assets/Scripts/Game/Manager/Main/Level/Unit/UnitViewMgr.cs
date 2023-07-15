using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitViewMgr : MonoBehaviour
{
    [Header("Character")]
    public Transform tfCharacter;
    public GameObject pfCharacter;
    //public List<BattleCharacterView>  
    public Dictionary<int, BattleCharacterView> dicCharacterView = new Dictionary<int, BattleCharacterView>();

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

    #endregion

    #region Character
    public void InitCharacterView()
    {
        dicCharacterView.Clear();
        PublicTool.ClearChildItem(tfCharacter);
        foreach(var character in PublicTool.GetLevelData().listCharacter)
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

    #endregion

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

    #region ActionDeal

    public void InvokeAction_SelfMove(Vector2Int targetPos)
    {
        LevelData levelData = PublicTool.GetLevelData();
        UnitInfo curUnitInfo = levelData.GetCurUnitInfo();
        //Data
        BattleUnitData unitData = levelData.GetCurUnitData();
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
                EventCenter.Instance.EventTrigger("RefreshTileInfo", null);
                EventCenter.Instance.EventTrigger("RefreshCharacterInfo", null);
            }
        }
    }

    public void InvokeAction_Skill(int SkillID,Vector2Int targetPos)
    {

    }
    #endregion
}
