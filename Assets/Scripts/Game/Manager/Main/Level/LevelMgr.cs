using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMgr : MonoBehaviour
{
    [Header("Manager")]
    public MapViewMgr mapViewMgr;
    public UnitViewMgr unitMgr;
    public BattleMgr battleMgr;

    private LevelData levelData;
    private bool isInit = false;

    [Header("StateInfo")]
    public LevelPhase levelPhase = LevelPhase.Peace;

    #region Basic & Bind
    public void Init()
    {
        levelData = new LevelData();
        //If New Game
        levelData.NewGameData();

        mapViewMgr.Init(this);
        unitMgr.Init(this);
        battleMgr = BattleMgr.Instance;
        battleMgr.Init(this);

        isInit = true;
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("StartBattle", StartBattleEvent);
        EventCenter.Instance.AddEventListener("EndTurn", EndTurnEvent);
        EventCenter.Instance.AddEventListener("ChangeInteract", ChangeInteractEvent);
        EventCenter.Instance.AddEventListener("RefreshTileInfo", RefreshTileInfoEvent);

        EventCenter.Instance.AddEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.AddEventListener("InputMoveAction", InputMoveActionEvent);

    }



    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("StartBattle", StartBattleEvent);
        EventCenter.Instance.RemoveEventListener("EndTurn", EndTurnEvent);
        EventCenter.Instance.RemoveEventListener("ChangeInteract", ChangeInteractEvent);
        EventCenter.Instance.RemoveEventListener("RefreshTileInfo", RefreshTileInfoEvent);

        EventCenter.Instance.RemoveEventListener("InputChooseCharacter", InputChooseCharacterEvent);
        EventCenter.Instance.RemoveEventListener("InputMoveAction", InputMoveActionEvent);
    }



    private void FixedUpdate()
    {
        if (!isInit)
        {
            return;
        }
        mapViewMgr.TimeGo();
    }
    #endregion

    #region EventDeal
    private void StartBattleEvent(object arg0)
    {
        levelPhase = LevelPhase.Battle;
        battleMgr.StartNewBattle(this);
    }

    private void EndTurnEvent(object arg0)
    {
        battleMgr.EndTurnPhase();
    }

    private void ChangeInteractEvent(object arg0)
    {
        InteractInfo info = (InteractInfo)arg0;
        InputMgr.Instance.SetInteractState(info.state);
        switch (info.state)
        {
            case InteractState.Skill:
                GetLevelData().SetCurBattleSkillID(info.data_0);
                GetLevelData().RefreshSkillTileInfo();
                break;
        }

    }

    private void RefreshTileInfoEvent(object arg0)
    {
        GetLevelData().RefreshTileInfo();
    }

    private void InputChooseCharacterEvent(object arg0)
    {
        GetLevelData().SetCurUnitInfo(BattleUnitType.Character, (int)arg0);
    }

    private void InputMoveActionEvent(object arg0)
    {
        Vector2Int targetPos = (Vector2Int)arg0;
        unitMgr.InvokeAction_SelfMove(targetPos);
    }
    #endregion

    #region GetInfo

    public LevelData GetLevelData()
    {
        return levelData;
    }
    #endregion
}
