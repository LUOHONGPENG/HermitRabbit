using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{
    Dictionary<int, BattleFoeData> dicFoeSkillTarget = new Dictionary<int, BattleFoeData>();
    Dictionary<int, BattleCharacterData> dicCharacterSkillTarget = new Dictionary<int, BattleCharacterData>();

    public void SkillActionRequest(Vector2Int targetPos)
    {
        SkillBattleInfo skillBattleInfo = gameData.GetCurSkillBattleInfo();
        BattleUnitData skillMaster = gameData.GetCurUnitData();

        //Check Skill Condition
        if (!CheckSkillCondition(targetPos, skillBattleInfo, skillMaster))
        {
            return;
        }

        //StartTheSkill IEnumerator
        StartCoroutine(IE_InvokeSkill(targetPos, skillBattleInfo, skillMaster));
    }

    private IEnumerator IE_InvokeSkill(Vector2Int targetPos, SkillBattleInfo skillBattleInfo, BattleUnitData skillMaster)
    {
        EventCenter.Instance.EventTrigger("CharacterSkillStart",null);
        PublicTool.EventChangeInteract(InteractState.WaitAction);
        yield return StartCoroutine(BeforeSkill(targetPos,skillBattleInfo,skillMaster));
        yield return StartCoroutine(InvokeSkillData(targetPos, skillBattleInfo, skillMaster));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(InvokeSkillText());
        AfterSkill();
        EventCenter.Instance.EventTrigger("CharacterSkillEnd", null);
    }

    private IEnumerator BeforeSkill(Vector2Int targetPos, SkillBattleInfo skillBattleInfo, BattleUnitData skillMaster)
    {
        //Cost AP
        skillMaster.curAP--;

        //Calculate the target that will be affected
        List<Vector2Int> listPos = new List<Vector2Int>();
        switch (skillBattleInfo.regionType)
        {
            case SkillRegionType.Circle:
                listPos = PublicTool.GetTargetCircleRange(targetPos, skillBattleInfo.radius);
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
                    //foeData.GetHurt(100);
                    foeData.ClearBattleTextQueue();
                    dicFoeSkillTarget.Add(foeData.keyID, foeData);
                }
            }
        }
        yield break;
    }

    private IEnumerator InvokeSkillData(Vector2Int targetPos, SkillBattleInfo skillBattleInfo, BattleUnitData skillMaster)
    {
        //Deal Effect
        foreach (var item in dicFoeSkillTarget)
        {
            BattleFoeData foeData = item.Value;
            SkillDamageRequest(skillMaster, foeData, 100);
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

    private bool CheckSkillCondition(Vector2Int targetPos, SkillBattleInfo skillBattleInfo, BattleUnitData skillMaster)
    {
        if (!skillMaster.listValidSkill.Contains(targetPos))
        {
            EventCenter.Instance.EventTrigger("EffectUIText", new EffectUITextInfo(EffectUITextType.Warning, targetPos, -1, "No target"));
            Debug.Log("No target");
            return false;
        }
        else if (skillMaster.curAP <= 0)
        {
            EventCenter.Instance.EventTrigger("EffectUIText", new EffectUITextInfo(EffectUITextType.Warning, targetPos, -1, "SP not enough"));
            Debug.Log("AP not enough");
            return false;
        }
        else
        {
            return true;
        }
    }
}
