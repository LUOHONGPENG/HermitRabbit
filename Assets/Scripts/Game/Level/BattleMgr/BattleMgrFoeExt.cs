using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{
    public Stack<int> stackFoe = new Stack<int>();
    private bool isInFoeSkill = false;

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

    /// <summary>
    /// Execute the foe turn
    /// </summary>
    /// <returns></returns>
    private IEnumerator IE_ExecuteFoeTurn()
    {
        while (stackFoe.Count>0)
        {
            //SetCurUnit
            int foeKeyID = stackFoe.Pop();            
            BattleFoeData foeData = gameData.GetBattleFoeData(foeKeyID);
            gameData.SetCurUnitInfo(new UnitInfo(BattleUnitType.Foe, foeKeyID));

            //Move Camera to the cur unit
            PublicTool.EventCameraGoPosID(foeData.posID);
            yield return new WaitForSeconds(0.5f);

            //Scan and get the target Pos
            IEnumerator itorReturn = null;
            itorReturn = IE_ExecuteFoeScan(foeData);
            yield return itorReturn;
            Vector2Int targetPos = ((Vector2Int)itorReturn.Current);
            if (targetPos.x >= 0)
            {
                yield return StartCoroutine(IE_ExecuteFoeMove(foeData, targetPos));
            }
            else
            {
                continue;
            }

            //Spell skill and aim at the target first
            if (CheckPossibleSkill(foeData))
            {
                if (foeData.listValidSkill.Contains(targetPos))
                {
                    yield return StartCoroutine(IE_ExecuteFoeSkill(foeData,targetPos));
                }
                else
                {
                    yield return StartCoroutine(IE_ExecuteFoeSkill(foeData, foeData.listValidSkill[0]));
                }
            }
        }
        EndTurnPhase();
    }

    /// <summary>
    /// Check whether the skill 
    /// </summary>
    /// <param name="foeData"></param>
    /// <returns></returns>
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
        //Get Target for Foe
        List<FindPathNode> listFoeTargetNode = new List<FindPathNode>();
        List<Vector2Int> listFoeTargetPos = PublicTool.GetGameData().GetFoeTargetPos();
        //Create the node according to the data
        foreach(Vector2Int pos in listFoeTargetPos)
        {
            if (foeData.dicBFSAllNode.ContainsKey(pos))
            {
                listFoeTargetNode.Add(foeData.dicBFSAllNode[pos]);
            }
        }
        //Find Who is the cloest to this Foe (In following version will become a part of the hate value)
        PublicTool.FindPathNodeSortLowestGCost(listFoeTargetNode);
        if (listFoeTargetNode.Count > 0)
        {
            yield return listFoeTargetNode[0].pos;
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

    private IEnumerator IE_ExecuteFoeSkill(BattleFoeData foeData,Vector2Int targetPos)
    {
        //        SkillActionRequest(foeData.listValidSkill[0]);
        SkillActionRequest(targetPos);
        isInFoeSkill = true;
        yield return new WaitUntil(() => !isInFoeSkill);
    }

}
