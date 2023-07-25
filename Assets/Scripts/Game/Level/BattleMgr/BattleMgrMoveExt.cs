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

        if (moveSubjectData.dicValidMoveNode.ContainsKey(targetPos))
        {
            StartCoroutine(IE_InvokeMoveAction(moveSubjectData.dicValidMoveNode[targetPos]));
        }
    }

    private IEnumerator IE_InvokeMoveAction(FindPathNode findPathNode)
    {
        EventCenter.Instance.EventTrigger("CharacterActionStart", null);
        PublicTool.EventChangeInteract(InteractState.WaitAction);
        yield return StartCoroutine(IE_InvokeMoveData(findPathNode.path));
        yield return StartCoroutine(IE_InvokeMoveView(findPathNode.path));
        AfterMove();
        EventCenter.Instance.EventTrigger("CharacterActionEnd", null);
    }

    private IEnumerator IE_InvokeMoveData(List<Vector2Int> path)
    {
        int costMOV = path.Count - 1;
        //int costMOV = PublicTool.CalculateGlobalDis(moveSubjectData.posID, moveTargetPos);
        //Data Move
        moveSubjectData.posID = moveTargetPos;
        moveSubjectData.curMOV -= costMOV;
        yield break;
    }

    private IEnumerator IE_InvokeMoveView(List<Vector2Int> path)
    {
        //ViewMove
        BattleUnitView moveView = unitViewMgr.GetViewFromUnitInfo(moveSubjectInfo);
        if (moveView != null)
        {
            yield return StartCoroutine(moveView.IE_MovePath(path));
        }
        yield return new WaitForSeconds(0.1f);
    }

    private void AfterMove()
    {
        PublicTool.RecalculateOccupancy();
        PublicTool.EventRefreshCharacterUI();

        if (battleTurnPhase == BattlePhase.CharacterPhase)
        {
            PublicTool.EventChangeInteract(InteractState.CharacterMove);
        }
    }
}
