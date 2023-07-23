using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{
    /// <summary>
    /// Character Skill
    /// </summary>

    private Vector2Int skillTargetPos;
    private SkillBattleInfo skillBattleInfo;
    private BattleUnitData skillSubject;
    private Dictionary<int, BattleFoeData> dicFoeSkillTarget = new Dictionary<int, BattleFoeData>();
    private Dictionary<int, BattleCharacterData> dicCharacterSkillTarget = new Dictionary<int, BattleCharacterData>();

    public void SkillActionRequest(Vector2Int targetPos)
    {
        //Set initial Data
        skillTargetPos = targetPos;
        skillBattleInfo = gameData.GetCurSkillBattleInfo();
        skillSubject = gameData.GetCurUnitData();

        //Check Skill Condition
        if (!CheckSkillCondition())
        {
            return;
        }

        //StartTheSkill IEnumerator
        StartCoroutine(IE_InvokeSkillAction());
    }

    private IEnumerator IE_InvokeSkillAction()
    {
        EventCenter.Instance.EventTrigger("CharacterSkillStart",null);
        PublicTool.EventChangeInteract(InteractState.WaitAction);
        yield return StartCoroutine(BeforeSkill());
        yield return StartCoroutine(InvokeSkillData());
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(InvokeSkillText());
        AfterSkill();
        EventCenter.Instance.EventTrigger("CharacterSkillEnd", null);
    }

    private IEnumerator BeforeSkill()
    {
        //Cost AP
        skillSubject.curAP -= skillBattleInfo.costAP;

        //Calculate the position that will be affected
        List<Vector2Int> listPos = new List<Vector2Int>();
        switch (skillBattleInfo.regionType)
        {
            case SkillRegionType.Circle:
                listPos = PublicTool.GetTargetCircleRange(skillTargetPos, skillBattleInfo.radius);
                break;
        }

        dicFoeSkillTarget.Clear();
        dicCharacterSkillTarget.Clear();

        //Find Foe
        foreach (var pos in listPos)
        {
            if (gameData.dicTempMapUnit.ContainsKey(pos))
            {
                if (gameData.dicTempMapUnit[pos].type == BattleUnitType.Foe && skillBattleInfo.isTargetFoe)
                {
                    BattleFoeData foeData = (BattleFoeData)gameData.GetDataFromUnitInfo(gameData.dicTempMapUnit[pos]);
                    foeData.ClearBattleTextQueue();
                    dicFoeSkillTarget.Add(foeData.keyID, foeData);
                }
            }
        }
        yield break;
    }

    private IEnumerator InvokeSkillData()
    {
        //Deal Effect
        foreach (var item in dicFoeSkillTarget)
        {
            BattleFoeData foeData = item.Value;
            if(skillBattleInfo.foeEffect == SkillEffectType.Harm)
            {
                SkillHarmRequest(skillSubject, foeData);
            }
        }
        yield break;
    }

    private IEnumerator InvokeSkillText()
    {
        //ShowDamage
        foreach (var item in dicFoeSkillTarget)
        {
            BattleFoeData foeData = item.Value;
            BattleFoeView foeView = unitViewMgr.GetFoeView(foeData.keyID);
            foeView.RequestBattleText();
        }
        yield break;
    }

    private void AfterSkill()
    {
        //Clear Foe
        foreach (var item in dicFoeSkillTarget)
        {
            if (item.Value.isDead)
            {
                unitViewMgr.RemoveFoeView(item.Value.keyID);
            }
        }
        gameData.CheckClearFoe();

        PublicTool.EventRefreshOccupancy();
        PublicTool.EventRefreshSkill();
        PublicTool.EventRefreshCharacterUI();

        if (battleTurnPhase == BattlePhase.CharacterPhase)
        {
            PublicTool.EventChangeInteract(InteractState.Skill);
        }
    }

    private bool CheckSkillCondition()
    {
        if (!skillSubject.listValidSkill.Contains(skillTargetPos))
        {
            EventCenter.Instance.EventTrigger("EffectUIText", new EffectUITextInfo(EffectUITextType.Warning, skillTargetPos, -1, "No target"));
            Debug.Log("No target");
            return false;
        }
        else if (skillSubject.curAP < skillBattleInfo.costAP)
        {
            EventCenter.Instance.EventTrigger("EffectUIText", new EffectUITextInfo(EffectUITextType.Warning, skillTargetPos, -1, "SP not enough"));
            Debug.Log("AP not enough");
            return false;
        }
        else
        {
            return true;
        }
    }
}
