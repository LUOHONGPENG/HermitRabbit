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

            yield return StartCoroutine(IE_ExecuteFoeMove(foeData));
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

    private IEnumerator IE_ExecuteFoeMove(BattleFoeData foeData)
    {
        yield break;
    }

    private IEnumerator IE_ExecuteFoeSkill(BattleFoeData foeData)
    {
        SkillActionRequest(foeData.listValidSkill[0]);
        yield return new WaitForSeconds(1f);
    }

}
