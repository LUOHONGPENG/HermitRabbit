using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{
    private Vector2Int moveTargetPos;
    private BattleUnitData moveSubjectData;

    public void MoveActionRequest(Vector2Int targetPos)
    {
        moveTargetPos = targetPos;
        moveSubjectData = gameData.GetCurUnitData();

        UnitInfo curUnitInfo = gameData.GetCurUnitInfo();
        if (moveSubjectData.listValidMove.Contains(targetPos))
        {
            int costMOV = PublicTool.CalculateGlobalDis(moveSubjectData.posID, targetPos);
            //Data Move
            moveSubjectData.posID = targetPos;
            moveSubjectData.curMOV -= costMOV;

            //ViewMove
            BattleUnitView moveView = unitViewMgr.GetViewFromUnitInfo(curUnitInfo);
            if (moveView != null)
            {
                moveView.MoveToPos(targetPos);
            }
            PublicTool.EventRefreshOccupancy();
            PublicTool.EventRefreshCharacterUI();
        }

    }
}
