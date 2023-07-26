using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{
    public Stack<int> stackFoe = new Stack<int>();

    private void ScanFoeStack()
    {
        stackFoe.Clear();
        foreach(var foe in gameData.listFoe)
        {
            if (!foe.isDead)
            {
                stackFoe.Push(foe.keyID);
            }
        }
    }

    private IEnumerator IE_ExecuteFoeTurn()
    {
        while (stackFoe.Count>0)
        {
            int foeKeyID = stackFoe.Pop();            
            BattleFoeData foeData = gameData.GetBattleFoeData(foeKeyID);
            //SetCurUnit
            gameData.SetCurUnitInfo(new UnitInfo(BattleUnitType.Foe, foeKeyID));
            PublicTool.EventCameraGoPosID(foeData.posID);
            yield return new WaitForSeconds(0.5f);

            IEnumerator itorReturn = null;
            itorReturn = IE_ExecuteFoeScan(foeData);
            yield return itorReturn;
            if (((Vector2Int)itorReturn.Current).x >= 0)
            {
                Debug.Log(foeData.keyID + "target " + ((Vector2Int)itorReturn.Current));
                yield return StartCoroutine(IE_ExecuteFoeMove(foeData, ((Vector2Int)itorReturn.Current)));
            }
            else
            {

            }

            if (CheckPossibleSkill(foeData))
            {
                yield return StartCoroutine(IE_ExecuteFoeSkill(foeData));
            }
        }
        EndTurnPhase();
    }

    private bool CheckPossibleSkill(BattleFoeData foeData)
    {
        gameData.SetCurBattleSkill(foeData.GetSkillID());
        PublicTool.RecalculateSkillCover();
        if (foeData.listValidSkill.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator IE_ExecuteFoeScan(BattleFoeData foeData)
    {
        //Scan
        PublicTool.RecalculateOccupancy();
        foeData.RefreshFoeMoveAllBFSNode();
        //Get Friend
        List<FindPathNode> listFriendNode = new List<FindPathNode>();
        List<Vector2Int> listFriendPos = PublicTool.GetGameData().listTempFriendPos;

        foreach(Vector2Int pos in listFriendPos)
        {
            listFriendNode.Add(foeData.dicBFSAllNode[pos]);
        }
        PublicTool.FindPathNodeSortLowestGCost(listFriendNode);

        if (listFriendNode.Count > 0)
        {
            yield return listFriendNode[0].pos;
        }
        else
        {
            Debug.Log(foeData.keyID + "Cant Find Friend");
            yield return new Vector2Int(-1,-1);
        }
    }

    private IEnumerator IE_ExecuteFoeMove(BattleFoeData foeData,Vector2Int aimPos)
    {
        int touchRange = foeData.GetSkillTouchRange();
        foeData.RefreshDistanceFromAimNode(aimPos, touchRange);


        Debug.Log(foeData.keyID + "dicValidCount " + foeData.dicValidMoveNode.Count +"CurMP " + foeData.curMOV);
        List<FindPathNode> listValidNode = new List<FindPathNode>();
        foreach (KeyValuePair<Vector2Int, FindPathNode> pair in foeData.dicValidMoveNode)
        {
            listValidNode.Add(pair.Value);
        }
        PublicTool.FindPathNodeSortLowestHCost(listValidNode);


        if (listValidNode.Count > 0)
        {
            FindPathNode tarNode = listValidNode[0];

            FindPathNode curNode = foeData.dicBFSAllNode[foeData.posID];

            if(tarNode.hCostReal <= curNode.hCostReal && curNode.hCostReal > 0)
            {
                Debug.Log(foeData.keyID +" Move cur"+ curNode.hCostReal);

                moveTargetPos = tarNode.pos;
                moveSubjectData = gameData.GetCurUnitData();
                moveSubjectInfo = gameData.GetCurUnitInfo();

                yield return StartCoroutine(IE_InvokeMoveData(tarNode.path));
                yield return StartCoroutine(IE_InvokeMoveView(tarNode.path));
                AfterMove();
            }
            else
            {
                Debug.Log(foeData.keyID + "DontMove cur" + curNode.hCostReal);


                yield break;
            }
        }
        yield break;
    }

    private IEnumerator IE_ExecuteFoeSkill(BattleFoeData foeData)
    {
        SkillActionRequest(foeData.listValidSkill[0]);
        yield return new WaitForSeconds(1f);
    }

}
