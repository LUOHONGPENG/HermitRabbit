using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class BattleMgr
{
    //A dictionary that sort the plant according to their trigger type
    private Dictionary<PlantTriggerType, List<int>> dicPlantTrigger = new Dictionary<PlantTriggerType, List<int>>();
    private Queue<PlantSkillRequestInfo> queueSkillRequest = new Queue<PlantSkillRequestInfo>();
    private bool isInPlantSkill = false;
    private BattleCharacterData plantRecordCharacter;
    private BattleFoeData plantRecordFoe;
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
    private void CheckAddPlantSkillRequest(PlantTriggerType plantTriggerType,UnitInfo unitInfo)
    {
        if (!dicPlantTrigger.ContainsKey(plantTriggerType))
        {
            return;
        }

        //Get all plants in this trigger type
        List<int> listCheck = dicPlantTrigger[plantTriggerType];
        for (int i = 0; i < listCheck.Count; i++)
        {
            if(plantTriggerType == PlantTriggerType.CharacterNormalAttack)
            {
                BattleUnitData tarUnit = gameData.GetDataFromUnitInfo(unitInfo);
                if (!tarUnit.isDead)
                {
                    BattlePlantData curPlant = (BattlePlantData)gameData.GetDataFromUnitInfo(new UnitInfo(BattleUnitType.Plant, listCheck[i]));
                    SkillExcelItem skillItem = PublicTool.GetSkillItem(curPlant.GetSkillID());
                    if (PublicTool.CalculateGlobalDis(tarUnit.posID, curPlant.posID) <= skillItem.RealRange)
                    {
                        queueSkillRequest.Enqueue(new PlantSkillRequestInfo(curPlant.keyID, skillItem.id, unitInfo));
                    }
                }
            }
            else if(plantTriggerType == PlantTriggerType.ActionEndDead)
            {
                BattlePlantData curPlant = (BattlePlantData)gameData.GetDataFromUnitInfo(new UnitInfo(BattleUnitType.Plant, listCheck[i]));
                if (curPlant != null && !curPlant.isDead)
                {
                    SkillExcelItem skillItem = PublicTool.GetSkillItem(curPlant.GetSkillID());
                    List<BattleCharacterData> listCharacter = gameData.listCharacter;
                    for (int j = 0; j < listCharacter.Count; j++)
                    {
                        BattleCharacterData checkCharacter = listCharacter[j];
                        if (checkCharacter.isDead && PublicTool.CalculateGlobalDis(checkCharacter.posID, curPlant.posID) <= skillItem.RealRange)
                        {
                            queueSkillRequest.Enqueue(new PlantSkillRequestInfo(curPlant.keyID, skillItem.id, new UnitInfo(BattleUnitType.Character, checkCharacter.keyID)));
                            break;
                        }
                    }
                }
            }
        }
    }

    private IEnumerator IE_CheckPlantTurnStart()
    {
        //TurnStart
        yield return StartCoroutine(IE_CheckAllPlantInDic(PlantTriggerType.TurnStart));
        //FirstTurn
        if (numTurn == 1)
        {
            yield return StartCoroutine(IE_CheckAllPlantInDic(PlantTriggerType.FirstTurn));
        }
        else if(numTurn == 2)
        {
            yield return StartCoroutine(IE_CheckAllPlantInDic(PlantTriggerType.SecondTurn));
        }
        //For
        yield break;
    }

    private IEnumerator IE_CheckAllPlantInDic(PlantTriggerType type)
    {
        List<int> listTemp = new List<int>();
        if (dicPlantTrigger.ContainsKey(type))
        {
            listTemp = dicPlantTrigger[type];
            foreach (var keyID in listTemp)
            {
                gameData.SetCurUnitInfo(new UnitInfo(BattleUnitType.Plant, keyID));
                BattlePlantData plantData = (BattlePlantData)gameData.GetCurUnitData();
                if (CheckPossiblePlantSkill(plantData))
                {
                    int ran = UnityEngine.Random.Range(0, plantData.listValidSkill.Count);
                    yield return StartCoroutine(IE_ExecutePlantSkillFindTarget(plantData, plantData.listValidSkill[ran]));
                }
            }
        }
        yield break;
    }



    private IEnumerator IE_CheckPlantBeforeSkill()
    {
        if (skillSubject.battleUnitType == BattleUnitType.Plant)
        {
            yield break;
        }

        yield return StartCoroutine(IE_ExecutePlantSkillFixedTarget());
    }

    private IEnumerator IE_CheckPlantAfterSkill()
    {
        if(skillSubject.battleUnitType != BattleUnitType.Plant)
        {
            //Character Normal Attack Request Plant Attack
            if (skillBattleInfo.activeSkillType == ActiveSkillType.NormalAttack && skillSubject.battleUnitType == BattleUnitType.Character && gameData.dicTempMapUnit.ContainsKey(skillTargetPos))
            {
                CheckAddPlantSkillRequest(PlantTriggerType.CharacterNormalAttack, gameData.dicTempMapUnit[skillTargetPos]);
            }

            CheckAddPlantSkillRequest(PlantTriggerType.ActionEndDead, new UnitInfo(BattleUnitType.None, -1));

            yield return StartCoroutine(IE_ExecutePlantSkillFixedTarget());
        }
        else
        {
            CheckAddPlantSkillRequest(PlantTriggerType.ActionEndDead, new UnitInfo(BattleUnitType.None, -1));
        }
    }

    private IEnumerator IE_ExecutePlantSkillFixedTarget()
    {
        if (battleTurnPhase == BattlePhase.CharacterPhase && gameData.GetCurUnitData() != null && gameData.GetCurUnitData().battleUnitType == BattleUnitType.Character && queueSkillRequest.Count > 0)
        {
            plantRecordCharacter = (BattleCharacterData)gameData.GetCurUnitData();
            beforeSkillID = gameData.GetCurSkillBattleInfo().ID;
        }
        

        while (queueSkillRequest.Count > 0)
        {
            PlantSkillRequestInfo plantSkillInfo = queueSkillRequest.Dequeue();

            gameData.SetCurUnitInfo(new UnitInfo(BattleUnitType.Plant, plantSkillInfo.keyID));
            BattlePlantData plantData = (BattlePlantData)gameData.GetCurUnitData();
            gameData.SetCurBattleSkill(plantData.GetSkillID());
            PublicTool.RecalculateSkillCover();

            BattleUnitData tarUnitData = gameData.GetDataFromUnitInfo(plantSkillInfo.tarUnit);
            if (tarUnitData != null && ((!tarUnitData.isDead && plantData.GetSkillID() != 3401) || (tarUnitData.isDead && plantData.GetSkillID()==3401)))//Except for revive
            {
                //When the plant will execute the skill, camera can be moved.
                PublicTool.EventNormalCameraGoPosID(plantData.posID);
                yield return new WaitForSeconds(0.5f);
                isInPlantSkill = true;
                SkillActionRequest(tarUnitData.posID);
            }
            else
            {

            }
            yield return new WaitUntil(() => !isInPlantSkill);
        }

        if (battleTurnPhase == BattlePhase.CharacterPhase && plantRecordCharacter!=null)
        {
            gameData.SetCurUnitInfo(new UnitInfo(BattleUnitType.Character, plantRecordCharacter.keyID));
            gameData.SetCurBattleSkill(beforeSkillID);
            PublicTool.EventNormalCameraGoPosID(plantRecordCharacter.posID);
            plantRecordCharacter = null;
        }
        else if(battleTurnPhase == BattlePhase.FoePhase)
        {
            gameData.SetCurUnitInfo(new UnitInfo(BattleUnitType.None, -1));
        }

        yield break;
    }

    private IEnumerator IE_ExecutePlantSkillFindTarget(BattlePlantData plantData, Vector2Int targetPos)
    {
        isInPlantSkill = true;
        SkillActionRequest(targetPos);
        yield return new WaitUntil(() => !isInPlantSkill);
        yield break;
    }

    private bool CheckPossiblePlantSkill(BattlePlantData plantData)
    {
        if(plantData==null || plantData.isDead)
        {
            return false;
        }
        gameData.SetCurBattleSkill(plantData.GetSkillID());
        PublicTool.RecalculateSkillCover();
        if (plantData.listValidSkill.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
