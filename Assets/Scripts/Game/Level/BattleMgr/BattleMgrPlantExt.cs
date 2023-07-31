using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{
    private Dictionary<PlantTriggerType, List<int>> dicPlantTrigger = new Dictionary<PlantTriggerType, List<int>>();
    private Queue<PlantSkillRequestInfo> queueSkillRequest = new Queue<PlantSkillRequestInfo>();
    private bool isInPlantSkill = false;
    private BattleCharacterData plantRecordCharacter;
    private int beforeSkillID;

    private void GeneratePlantTriggerDic()
    {
        dicPlantTrigger.Clear();

        foreach(BattlePlantData plantData in gameData.listPlant)
        {
            PlantTriggerType triggerCondition = plantData.GetItem().triggerCondition;
            if (!plantData.isDead)
            {
                if (dicPlantTrigger.ContainsKey(triggerCondition))
                {
                    dicPlantTrigger[triggerCondition].Add(plantData.keyID);
                }
                else
                {
                    List<int> listPlantID = new List<int>();
                    listPlantID.Add(plantData.keyID);
                    dicPlantTrigger.Add(triggerCondition, listPlantID);
                }
            }
        }
    }

    /// <summary>
    /// Check whether the range cover the possible target
    /// </summary>
    /// <param name="plantTriggerType"></param>
    /// <param name="unitInfo"></param>
    private void CheckPlantSkillRequest(PlantTriggerType plantTriggerType,UnitInfo unitInfo)
    {
        if (!dicPlantTrigger.ContainsKey(plantTriggerType))
        {
            return;
        }

        List<int> listCheck = dicPlantTrigger[plantTriggerType];
        for (int i = 0; i < listCheck.Count; i++)
        {
            BattleUnitData tarUnit = gameData.GetDataFromUnitInfo(unitInfo);
            BattlePlantData curPlant = (BattlePlantData)gameData.GetDataFromUnitInfo(new UnitInfo(BattleUnitType.Plant,listCheck[i]));
            SkillExcelItem skillItem = PublicTool.GetSkillItem(curPlant.GetSkillID());
            if(PublicTool.CalculateGlobalDis(tarUnit.posID,curPlant.posID) <= skillItem.range)
            {
                queueSkillRequest.Enqueue(new PlantSkillRequestInfo(curPlant.keyID, skillItem.id, unitInfo));
            }
        }
    }

    private IEnumerator IE_CheckPlantBeforeSkill()
    {
        if (skillSubject.battleUnitType == BattleUnitType.Plant)
        {
            yield break;
        }

        yield return StartCoroutine(IE_ExecutePlantSkill());
    }

    private IEnumerator IE_CheckPlantAfterSkill()
    {
        if(skillSubject.battleUnitType == BattleUnitType.Plant)
        {
            yield break;
        }

        //Character Normal Attack
        if (skillBattleInfo.isNormalAttack && skillSubject.battleUnitType == BattleUnitType.Character && gameData.dicTempMapUnit.ContainsKey(skillTargetPos))
        {
            CheckPlantSkillRequest(PlantTriggerType.CharacterNormalAttack, gameData.dicTempMapUnit[skillTargetPos]);
        }

        yield return StartCoroutine(IE_ExecutePlantSkill());
    }

    private IEnumerator IE_ExecutePlantSkill()
    {
        if (battleTurnPhase == BattlePhase.CharacterPhase && queueSkillRequest.Count > 0)
        {
            plantRecordCharacter = (BattleCharacterData)gameData.GetCurUnitData();
            beforeSkillID = gameData.GetCurSkillBattleInfo().ID;
        }

        while (queueSkillRequest.Count > 0)
        {
            PlantSkillRequestInfo plantSkill = queueSkillRequest.Dequeue();

            gameData.SetCurUnitInfo(new UnitInfo(BattleUnitType.Plant, plantSkill.keyID));
            BattlePlantData plantData = (BattlePlantData)gameData.GetCurUnitData();
            gameData.SetCurBattleSkill(plantData.GetSkillID());
            PublicTool.RecalculateSkillCover();
            PublicTool.EventCameraGoPosID(plantData.posID);
            yield return new WaitForSeconds(0.5f);

            BattleUnitData tarUnitData = gameData.GetDataFromUnitInfo(plantSkill.tarUnit);
            if (tarUnitData != null && !tarUnitData.isDead)//Except for revive
            {
                isInPlantSkill = true;
                SkillActionRequest(tarUnitData.posID);
            }
            yield return new WaitUntil(() => !isInPlantSkill);
        }

        if (battleTurnPhase == BattlePhase.CharacterPhase && plantRecordCharacter!=null)
        {
            gameData.SetCurUnitInfo(new UnitInfo(BattleUnitType.Character, plantRecordCharacter.keyID));
            gameData.SetCurBattleSkill(beforeSkillID);
            PublicTool.EventCameraGoPosID(plantRecordCharacter.posID);
            plantRecordCharacter = null;
        }
        yield break;

    }
}
