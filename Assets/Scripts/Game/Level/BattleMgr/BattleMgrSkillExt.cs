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
    private Dictionary<int, BattlePlantData> dicPlantSkillTarget = new Dictionary<int, BattlePlantData>();

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
        EventCenter.Instance.EventTrigger("CharacterActionStart",null);
        PublicTool.EventChangeInteract(InteractState.WaitAction);
        yield return StartCoroutine(IE_ExecuteSkillCost());
        yield return StartCoroutine(IE_FindSkillTarget());
        yield return StartCoroutine(IE_InvokeSkillData());
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(InvokeSkillText());
        AfterSkill();
        EventCenter.Instance.EventTrigger("CharacterActionEnd", null);
    }

    private IEnumerator IE_ExecuteSkillCost()
    {
        if(skillBattleInfo.skillSubjectType == BattleUnitType.Character && skillSubject is BattleCharacterData)
        {
            skillSubject.curAP -= skillBattleInfo.costAP;
        }
        yield break;
    }


    private IEnumerator IE_FindSkillTarget()
    {

        //Calculate the position that will be affected
        List<Vector2Int> listPos = new List<Vector2Int>();
        switch (skillBattleInfo.regionType)
        {
            case SkillRegionType.Circle:
                listPos = PublicTool.GetTargetCircleRange(skillTargetPos, skillBattleInfo.radius);
                break;
            case SkillRegionType.Square:
                listPos = PublicTool.GetTargetSquareRange(skillTargetPos, skillBattleInfo.radius);
                break;
        }

        dicFoeSkillTarget.Clear();
        dicCharacterSkillTarget.Clear();
        dicPlantSkillTarget.Clear();

        //Find Foe
        foreach (var pos in listPos)
        {
            if (gameData.dicTempMapUnit.ContainsKey(pos))
            {
                if (skillBattleInfo.isTargetFoe && gameData.dicTempMapUnit[pos].type == BattleUnitType.Foe)
                {
                    BattleFoeData foeData = (BattleFoeData)gameData.GetDataFromUnitInfo(gameData.dicTempMapUnit[pos]);
                    foeData.ClearBattleTextQueue();
                    dicFoeSkillTarget.Add(foeData.keyID, foeData);
                }

                if (skillBattleInfo.isTargetCharacter && gameData.dicTempMapUnit[pos].type == BattleUnitType.Character)
                {
                    BattleCharacterData characterData = (BattleCharacterData)gameData.GetDataFromUnitInfo(gameData.dicTempMapUnit[pos]);
                    characterData.ClearBattleTextQueue();
                    dicCharacterSkillTarget.Add(characterData.keyID, characterData);
                }

                if (skillBattleInfo.isTargetPlant && gameData.dicTempMapUnit[pos].type == BattleUnitType.Plant)
                {
                    BattlePlantData plantData = (BattlePlantData)gameData.GetDataFromUnitInfo(gameData.dicTempMapUnit[pos]);
                    plantData.ClearBattleTextQueue();
                    dicPlantSkillTarget.Add(plantData.keyID, plantData);
                }
            }
        }

        

        yield break;
    }

    private IEnumerator IE_InvokeSkillData()
    {
        //Deal Effect
        foreach (var item in dicFoeSkillTarget)
        {
            BattleFoeData foeData = item.Value;
            SkillEffectRequest(skillSubject, foeData, skillBattleInfo.foeEffect);
        }

        foreach (var item in dicCharacterSkillTarget)
        {
            BattleCharacterData characterData = item.Value;
            SkillEffectRequest(skillSubject, characterData, skillBattleInfo.characterEffect);
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

        foreach (var item in dicCharacterSkillTarget)
        {
            BattleCharacterData characterData = item.Value;
            BattleCharacterView characterView = unitViewMgr.GetCharacterView(characterData.keyID);
            characterView.RequestBattleText();
        }

        foreach (var item in dicPlantSkillTarget)
        {
            BattlePlantData plantData = item.Value;
            BattlePlantView plantView = unitViewMgr.GetPlantView(plantData.keyID);
            plantView.RequestBattleText();
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

        PublicTool.RecalculateOccupancy();
        PublicTool.RecalculateSkillCover();
        PublicTool.EventRefreshCharacterUI();

        if (battleTurnPhase == BattlePhase.CharacterPhase)
        {
            PublicTool.EventChangeInteract(InteractState.CharacterSkill);
        }
    }

    private bool CheckSkillCondition()
    {
        if (!skillSubject.listValidSkill.Contains(skillTargetPos))
        {
            EventCenter.Instance.EventTrigger("EffectWarningText", new EffectWarningTextInfo("No Target", skillTargetPos));
            return false;
        }
        else if (skillSubject.curAP < skillBattleInfo.costAP)
        {
            EventCenter.Instance.EventTrigger("EffectWarningText", new EffectWarningTextInfo("AP not enough", skillTargetPos));
            return false;
        }
        else
        {
            return true;
        }
    }
}
