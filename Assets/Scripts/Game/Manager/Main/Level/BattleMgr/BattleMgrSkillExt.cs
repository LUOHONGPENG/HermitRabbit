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
        PublicTool.EventChangeInteract(InteractState.WaitAction);
        yield return StartCoroutine(BeforeSkill(targetPos,skillBattleInfo,skillMaster));
        yield return new WaitForSeconds(1f);
        AfterSkill();
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
                    foeData.GetHurt(100);
                    dicFoeSkillTarget.Add(foeData.keyID, foeData);
                }
            }
        }

        yield break;
    }

    private void AfterSkill()
    {
        //ShowDamage
        foreach (var item in dicFoeSkillTarget)
        {
            BattleFoeData foeData = item.Value;
            BattleFoeView foeView = unitViewMgr.GetFoeView(foeData.keyID);
            EventCenter.Instance.EventTrigger("EffectUIText", new EffectUITextInfo(EffectUITextType.Damage, foeData.posID, 100));
        }


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
        EventCenter.Instance.EventTrigger("RefreshSkillRange", null);
        EventCenter.Instance.EventTrigger("RefreshCharacterInfo", null);

        if(battleTurnPhase == BattlePhase.CharacterPhase)
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
