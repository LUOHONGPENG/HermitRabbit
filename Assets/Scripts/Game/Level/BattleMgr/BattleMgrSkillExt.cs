using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{
    /// <summary>
    /// Character Skill
    /// </summary>

    public Vector2Int skillTargetPos;
    private SkillBattleInfo skillBattleInfo;
    private BattleUnitData skillSubject;
    private Dictionary<int, BattleFoeData> dicFoeSkillTarget = new Dictionary<int, BattleFoeData>();
    private Dictionary<int, BattleCharacterData> dicCharacterSkillTarget = new Dictionary<int, BattleCharacterData>();
    private Dictionary<int, BattlePlantData> dicPlantSkillTarget = new Dictionary<int, BattlePlantData>();
    //Contain all position of all kind of target
    private Dictionary<Vector2Int, BattleUnitData> dicPosEffectTarget = new Dictionary<Vector2Int, BattleUnitData>();

    #region BattleExtraTarget
    //true means have selected first target
    public bool isFirstTargetSelected = false;
    public Vector2Int skillTargetPosExtra;
    public void BattleSkillReset()
    {
        isFirstTargetSelected = false;
        RefreshSkillRange();
    }
    #endregion

    public void SkillActionRequest(Vector2Int targetPos)
    {
        //SetCheckCondition
        skillBattleInfo = gameData.GetCurSkillBattleInfo();
        skillSubject = gameData.GetCurUnitData();

        //Check Skill Condition
        if (!CheckSkillCondition(targetPos))
        {
            return;
        }

        //Set Skill Target
        if (skillBattleInfo.needExtraTarget)
        {
            if (isFirstTargetSelected)
            {
                skillTargetPosExtra = targetPos;
                isFirstTargetSelected = false;
            }
            else
            {
                skillTargetPos = targetPos;
                isFirstTargetSelected = true;
                RefreshSkillRange();
            }
        }
        else
        {
            skillTargetPos = targetPos;
            isFirstTargetSelected = false;
        }

        if (!isFirstTargetSelected)
        {
            //StartTheSkill IEnumerator
            StartCoroutine(IE_InvokeSkillAction());
        }
    }

    private IEnumerator IE_InvokeSkillAction()
    {
        EventCenter.Instance.EventTrigger("CharacterActionStart",null);
        PublicTool.EventChangeInteract(InteractState.WaitAction);
        yield return StartCoroutine(IE_CheckPlantBeforeSkill());
        yield return StartCoroutine(IE_ExecuteSkillCost());
        yield return StartCoroutine(IE_FindSkillTarget());
        EventCenter.Instance.EventTrigger("EffectSkillName", skillBattleInfo.name);
        yield return StartCoroutine(IE_InvokeSkillPerform());
        yield return StartCoroutine(IE_InvokeSkillData());
        yield return StartCoroutine(InvokeSkillText());
        //The 0.6 second is hard code and need to be mofidied
        yield return new WaitForSeconds(GameGlobal.waitTimeText);
        yield return StartCoroutine(IE_CheckBuffConsume());
        //Plant Skill Go first so that Alfven skill work
        yield return StartCoroutine(IE_CheckPlantAfterSkill());
        yield return StartCoroutine(IE_AfterSkill());
        yield return StartCoroutine(IE_CheckBattleOver());

        if (!isBattleEnd)
        {
            isInFoeSkill = false;
            isInPlantSkill = false;

            if (battleTurnPhase == BattlePhase.CharacterPhase && gameData.GetCurUnitInfo().type == BattleUnitType.Character)
            {
                PublicTool.EventChangeInteract(InteractState.CharacterSkill);
                EventCenter.Instance.EventTrigger("CharacterActionEnd", null);
            }
        }
    }

    private IEnumerator IE_ExecuteSkillCost()
    {
        if(skillBattleInfo.skillSubjectType == BattleUnitType.Character && skillSubject is BattleCharacterData)
        {
            skillSubject.curAP -= skillBattleInfo.costAP;
            skillSubject.curMOV -= skillBattleInfo.costMOV;
            skillSubject.GetHurt(skillBattleInfo.costHP);
            gameData.CostMemory(skillBattleInfo.costMemory);
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
            case SkillRegionType.Line:
                listPos = PublicTool.GetTargetLineRange(skillTargetPos, skillSubject.posID, skillBattleInfo.range, skillBattleInfo.radius);
                break;
            case SkillRegionType.Water:
                listPos = PublicTool.GetTargetCircleRange(skillTargetPos, skillBattleInfo.radius);
                listPos = BattleMgr.Instance.GetTargetWaterRange(listPos);
                break;
            case SkillRegionType.BurnUnit:
                listPos = PublicTool.GetTargetBurningRange();
                listSkillBurnEffectPos = new List<Vector2Int>(listPos);
                listPos = PublicTool.GetTargetBurningRadius(listPos,skillBattleInfo.radius);
                break;
        }
        listSkillRadiusEffectPos = listPos;

        dicFoeSkillTarget.Clear();
        dicCharacterSkillTarget.Clear();
        dicPlantSkillTarget.Clear();
        dicPosEffectTarget.Clear();

        //Find Foe
        foreach (var pos in listPos)
        {
            if (gameData.dicTempMapUnit.ContainsKey(pos))
            {
                //Check whether this skill aim at Foe and
                if (skillBattleInfo.isTargetFoe && gameData.dicTempMapUnit[pos].type == BattleUnitType.Foe)
                {
                    BattleFoeData foeData = (BattleFoeData)gameData.GetDataFromUnitInfo(gameData.dicTempMapUnit[pos]);
                    foeData.ClearBattleTextQueue();
                    dicFoeSkillTarget.Add(foeData.keyID, foeData);
                    dicPosEffectTarget.Add(pos,foeData);
                }

                if (skillBattleInfo.isTargetCharacter && gameData.dicTempMapUnit[pos].type == BattleUnitType.Character)
                {
                    BattleCharacterData characterData = (BattleCharacterData)gameData.GetDataFromUnitInfo(gameData.dicTempMapUnit[pos]);
                    characterData.ClearBattleTextQueue();
                    dicCharacterSkillTarget.Add(characterData.keyID, characterData);
                    dicPosEffectTarget.Add(pos, characterData);
                }

                if (skillBattleInfo.isTargetPlant && gameData.dicTempMapUnit[pos].type == BattleUnitType.Plant)
                {
                    BattlePlantData plantData = (BattlePlantData)gameData.GetDataFromUnitInfo(gameData.dicTempMapUnit[pos]);
                    plantData.ClearBattleTextQueue();
                    dicPlantSkillTarget.Add(plantData.keyID, plantData);
                    dicPosEffectTarget.Add(pos, plantData);
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

        foreach (var item in dicPlantSkillTarget)
        {
            BattlePlantData plantData = item.Value;
            SkillEffectRequest(skillSubject, plantData, skillBattleInfo.plantEffect);
        }
        yield break;
    }

    private IEnumerator InvokeSkillText()
    {
        //Show the Skill information on the top

        //Change dicFoeSkillTarget to gameData.dicFoe
        //Because maybe not only the targets are affected by skill
        foreach (var item in gameData.dicFoe)
        {
            BattleFoeData foeData = item.Value;
            BattleFoeView foeView = unitViewMgr.GetFoeView(foeData.keyID);
            foeView.RequestBattleText();
        }

        foreach (var item in gameData.dicCharacter)
        {
            BattleCharacterData characterData = item.Value;
            BattleCharacterView characterView = unitViewMgr.GetCharacterView(characterData.keyID);
            characterView.RequestBattleText();
        }

        foreach (var item in gameData.dicPlant)
        {
            BattlePlantData plantData = item.Value;
            BattlePlantView plantView = unitViewMgr.GetPlantView(plantData.keyID);
            plantView.RequestBattleText();
        }
        yield break;
    }

    private IEnumerator IE_AfterSkill()
    {
        //If there is no a foe that is not in the dic are hurt
        CheckClearDeadUnitView();
        RefreshSkillRange();
        RefreshWaterRange();
        yield break;
    }

    private void CheckClearDeadUnitView()
    {
        //Clear Foe
        for(int i = gameData.listFoe.Count-1; i >= 0; i--)
        {
            BattleFoeData foeData = gameData.listFoe[i];

            if (foeData.isDead)
            {
                unitViewMgr.RemoveFoeView(foeData.keyID);
                gameData.RemoveFoeData(foeData.keyID);
            }
        }

        //Clear Plant
        for (int i = gameData.listPlant.Count - 1; i >= 0; i--)
        {
            BattlePlantData plantData = gameData.listPlant[i];
            if (plantData.isDead)
            {
                unitViewMgr.RemovePlantView(plantData.keyID);
                gameData.RemovePlantData(plantData.keyID);
            }
        }
    }


    private void RefreshSkillRange()
    {
        PublicTool.RecalculateOccupancy();
        PublicTool.RecalculateSkillCover();
        PublicTool.EventRefreshCharacterUI();
    }

    private IEnumerator IE_CheckBattleOver()
    {
        bool allFoeDead = true;
        bool allCharacterDead = true;
        foreach (var foe in gameData.listFoe)
        {
            if (!foe.isDead)
            {
                allFoeDead = false;
                break;
            }
        }

        foreach (var character in gameData.listCharacter)
        {
            if (!character.isDead)
            {
                allCharacterDead = false;
                break;
            }
        }

        if (allFoeDead)
        {
            BattleOverWin();
        }
        else if (allCharacterDead)
        {
            BattleOverLose();
        }
        yield break;
    }



    private bool CheckSkillCondition(Vector2Int targetPos)
    {
        if (!skillSubject.listValidSkill.Contains(targetPos))
        {
            EventCenter.Instance.EventTrigger("EffectWarningText", new EffectWarningTextInfo("No Target", targetPos));
            return false;
        }
        else if (skillSubject.curAP < skillBattleInfo.costAP)
        {
            EventCenter.Instance.EventTrigger("EffectWarningText", new EffectWarningTextInfo("AP not enough", targetPos));
            return false;
        }
        else if (skillSubject.curMOV < skillBattleInfo.costMOV)
        {
            EventCenter.Instance.EventTrigger("EffectWarningText", new EffectWarningTextInfo("MOV not enough", targetPos));
            return false;
        }
        else if(skillSubject.curHP < skillBattleInfo.costHP + 1)
        {
            EventCenter.Instance.EventTrigger("EffectWarningText", new EffectWarningTextInfo("HP not enough", targetPos));
            return false;
        }
        else if(gameData.GetMemory() < skillBattleInfo.costMemory)
        {
            EventCenter.Instance.EventTrigger("EffectWarningText", new EffectWarningTextInfo("Memory not enough", targetPos));
            return false;
        }
        else
        {
            return true;
        }
    }
}
