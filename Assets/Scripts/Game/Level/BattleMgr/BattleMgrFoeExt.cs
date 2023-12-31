using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public partial class BattleMgr
{
    public Stack<int> stackFoe = new Stack<int>();
    private bool isInFoeSkill = false;

    private IEnumerator IE_WholeFoeTurn()
    {
        //Check Buff for Foes
        yield return StartCoroutine(IE_FoeBuffCheck());
        if (isBattleEnd)
        {
            yield break;
        }
        //Scan all the foes to generate the stack
        ScanFoeStack();
        yield return StartCoroutine(IE_ExecuteFoeTurn());
        yield break;
    }

    private IEnumerator IE_TurnStartCheckFoeTrait()
    {
        foreach (BattleFoeData foe in gameData.listFoe)
        {
            if (!foe.isDead)
            {
                if (foe.TraitID > 0)
                {
                    switch (foe.TraitID)
                    {
                        case 1001:
                            if(numTurn == 1)
                            {
                                BuffExcelItem buff3004 = PublicTool.GetBuffExcelItem(3004);
                                SkillBuffEffectDeal(foe, 3004, 8, buff3004.GetName(), buff3004.effectType);

                                BuffExcelItem buff3005 = PublicTool.GetBuffExcelItem(3005);
                                SkillBuffEffectDeal(foe, 3005, 8, buff3005.GetName(), buff3005.effectType);

                                BattleFoeView foeView = unitViewMgr.GetFoeView(foe.keyID);
                                foeView.RequestBattleText();
                                PublicTool.EventNormalCameraGoPosID(foe.posID);
                                yield return new WaitForSeconds(GameGlobal.waitTimeText);
                            }
                            break;
                        case 1002:

                            BuffExcelItem buff3003 = PublicTool.GetBuffExcelItem(3003);
                            SkillBuffEffectDeal(foe, 3003, 1, buff3003.GetName(), buff3003.effectType);

                            BattleFoeView foeView1002 = unitViewMgr.GetFoeView(foe.keyID);
                            foeView1002.RequestBattleText();
                            PublicTool.EventNormalCameraGoPosID(foe.posID);
                            yield return new WaitForSeconds(GameGlobal.waitTimeText);
                            break;

                    }
                }
            }
        }
        yield break;

    }


    private IEnumerator IE_FoeBuffCheck()
    {
        //If the buff such as burning is triggered 
        bool hasBuffInvoked = false;
        for (int i = gameData.listFoe.Count-1;i >= 0;i--)
        {
            BattleFoeData foeData = gameData.listFoe[i];
            bool isTriggered = foeData.CheckBuffTrigger();
            //Mark that buff is triggered
            if (isTriggered)
            {
                hasBuffInvoked = true;
                BattleFoeView foeView = unitViewMgr.GetFoeView(foeData.keyID);
                foeView.RequestBattleText();
            }
        }
        if (hasBuffInvoked)
        {
            yield return new WaitForSeconds(GameGlobal.waitTimeText);
            yield return StartCoroutine(IE_AfterSkill());
            yield return StartCoroutine(IE_CheckBattleOver());
        }
        yield break;
    }

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
            //BattleOver
            if (isBattleEnd)
            {
                break;
            }

            //SetCurUnit
            int foeKeyID = stackFoe.Pop();            
            BattleFoeData foeData = gameData.GetBattleFoeData(foeKeyID);

            if(foeData == null || foeData.isDead)
            {
                continue;
            }

            gameData.SetCurUnitInfo(new UnitInfo(BattleUnitType.Foe, foeKeyID));

            //Move Camera to the cur unit
            PublicTool.EventNormalCameraGoPosID(foeData.posID);
            yield return new WaitForSeconds(0.5f);

            List<FoeFindTargetInfo> listFoeTargetInfo = new List<FoeFindTargetInfo>();

            //3.Scan and get the distance of all target Pos
            IEnumerator itorReturn = null;
            itorReturn = IE_ExecuteFoeCalculateHate(foeData, listFoeTargetInfo);
            yield return itorReturn;
            Vector2Int targetPos = ((Vector2Int)itorReturn.Current);

            //4.Move
            if (targetPos.x >= 0)
            {
                yield return StartCoroutine(IE_ExecuteFoeMove(foeData, targetPos));
            }
            else
            {
                continue;
            }

            //5.Spell skill and aim at the target first
            if (CheckPossibleFoeSkill(foeData))
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

        if (!isBattleEnd)
        {
            EndTurnPhase();
        }
    }

    /// <summary>
    /// Check whether the skill 
    /// </summary>
    /// <param name="foeData"></param>
    /// <returns></returns>
    private bool CheckPossibleFoeSkill(BattleFoeData foeData)
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

    private IEnumerator IE_ExecuteFoeCalculateHate(BattleFoeData foeData, List<FoeFindTargetInfo> listInfo)
    {
        //1.Scan using BFS and calculate the Gcost
        PublicTool.RecalculateOccupancy();
        foeData.RefreshFoeMoveAllBFSNode();
        //2.Get Target for Foe
        //List<FindPathNode> listFoeTargetNode = new List<FindPathNode>();
        if(foeData.GetTrueFocusType() == FoeFocusType.Foe)
        {
            foreach(var unit in gameData.listFoe)
            {
                //When charming, ignore self.
                if (foeData.CheckBuffExist(1009))
                {
                    if(unit == foeData)
                    {
                        continue;
                    }
                }
                listInfo.Add(new FoeFindTargetInfo(foeData,unit));
            }
        }
        else if(foeData.GetTrueFocusType() == FoeFocusType.Friend)
        {
            foreach (var unit in gameData.listCharacter)
            {
                listInfo.Add(new FoeFindTargetInfo(foeData, unit));
            }
            foreach (var unit in gameData.listPlant)
            {
                listInfo.Add(new FoeFindTargetInfo(foeData, unit));
            }
        }

        //Create the node according to the data
        foreach(FoeFindTargetInfo info in listInfo)
        {
            if (foeData.dicBFSAllNode.ContainsKey(info.posID))
            {
                //listFoeTargetNode.Add(foeData.dicBFSAllNode[info.posID]);
                info.GCost = foeData.dicBFSAllNode[info.posID].gCost;
            }
        }

        PublicTool.FoeFindTargetInfoSortHighestHate(listInfo);
        if(listInfo.Count > 0)
        {
            yield return listInfo[0].posID;
        }
        else
        {
            Debug.Log(foeData.keyID + "Cant Find Target");
            yield return new Vector2Int(-1, -1);
        }

        /*        //Find Who is the cloest to this Foe (In following version will become a part of the hate value)
                PublicTool.FindPathNodeSortLowestGCost(listFoeTargetNode);
                if (listFoeTargetNode.Count > 0)
                {
                    yield return listFoeTargetNode[0].pos;
                }
                else
                {
                    Debug.Log(foeData.keyID + "Cant Find Friend");
                    yield return new Vector2Int(-1,-1);
                }*/
    }


    private IEnumerator IE_ExecuteFoeMove(BattleFoeData foeData,Vector2Int aimPos)
    {
        int touchRange = foeData.GetSkillTouchRange();
        foeData.RefreshDistanceFromAimNode(aimPos, touchRange);




        //Debug.Log(foeData.keyID + "dicValidCount " + foeData.dicValidMoveNode.Count +"CurMP " + foeData.curMOV);

        List<FindPathNode> listValidNode = new List<FindPathNode>();
        foreach (KeyValuePair<Vector2Int, FindPathNode> pair in foeData.dicValidMoveNode)
        {
            FindPathNode validNode = pair.Value;
            listValidNode.Add(validNode);

        }
        PublicTool.FindPathNodeSortLowestHCost(listValidNode);

        if (listValidNode.Count > 0)
        {
            FindPathNode tarNode = listValidNode[0];
            FindPathNode curNode = foeData.dicBFSAllNode[foeData.posID];

            if(tarNode.hCostReal <= curNode.hCostReal && curNode.hCostReal > 0)
            {
                //Debug.Log(foeData.keyID +" Move cur"+ curNode.hCostReal);

                moveTargetPos = tarNode.pos;
                moveSubjectData = gameData.GetCurUnitData();
                moveSubjectInfo = gameData.GetCurUnitInfo();

                yield return StartCoroutine(IE_InvokeMoveData(tarNode.path));
                yield return StartCoroutine(IE_InvokeMoveView(tarNode.path));
                AfterMove();
            }
            else
            {
                //Debug.Log(foeData.keyID + "DontMove cur" + curNode.hCostReal);
                yield break;
            }
        }
        yield break;
    }

    private IEnumerator IE_ExecuteFoeSkill(BattleFoeData foeData,Vector2Int targetPos)
    {
        //        SkillActionRequest(foeData.listValidSkill[0]);
        isInFoeSkill = true;
        SkillActionRequest(targetPos);
        yield return new WaitUntil(() => !isInFoeSkill);
    }

}
