using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{
    private Vector2Int moveTargetPos;
    private BattleUnitData moveSubjectData;
    private UnitInfo moveSubjectInfo;

    public void MoveActionRequest(Vector2Int targetPos)
    {
        moveTargetPos = targetPos;
        moveSubjectData = gameData.GetCurUnitData();
        moveSubjectInfo = gameData.GetCurUnitInfo();

        if (moveSubjectData.listValidMove.Contains(targetPos))
        {
            StartCoroutine(IE_InvokeMoveAction());
        }
    }

    private IEnumerator IE_InvokeMoveAction()
    {
        EventCenter.Instance.EventTrigger("CharacterSkillStart", null);
        PublicTool.EventChangeInteract(InteractState.WaitAction);
        yield return StartCoroutine(IE_InvokeMoveData());
        yield return StartCoroutine(IE_InvokeMoveView());
        AfterMove();
        EventCenter.Instance.EventTrigger("CharacterSkillEnd", null);
    }

    private IEnumerator IE_InvokeMoveData()
    {
        int costMOV = PublicTool.CalculateGlobalDis(moveSubjectData.posID, moveTargetPos);
        //Data Move
        moveSubjectData.posID = moveTargetPos;
        moveSubjectData.curMOV -= costMOV;
        yield break;
    }

    private IEnumerator IE_InvokeMoveView()
    {
        //ViewMove
        BattleUnitView moveView = unitViewMgr.GetViewFromUnitInfo(moveSubjectInfo);
        if (moveView != null)
        {
            moveView.MoveToPos(moveTargetPos, true);
        }
        yield return new WaitForSeconds(0.5f);
    }

    private void AfterMove()
    {
        PublicTool.EventRefreshOccupancy();
        PublicTool.EventRefreshCharacterUI();

        if (battleTurnPhase == BattlePhase.CharacterPhase)
        {
            PublicTool.EventChangeInteract(InteractState.CharacterMove);
        }
    }
}
