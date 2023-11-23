using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{
    #region StartCharacterTurn
    private IEnumerator IE_WholeStartCharacterTurn()
    {        
        ResetNewTurnBefore();
        //Show Animation Of new Turn
        yield return new WaitForSeconds(0.5f);

        //TileFire
        TileBurningExpand();
        yield return StartCoroutine(IE_TileApplyBurning());

        //PlantHeal
        yield return StartCoroutine(IE_FlowerTileHealPlant());

        //PlantTurnStart
        GeneratePlantTriggerDic();
        yield return StartCoroutine(IE_CheckPlantTurnStart());
        if (isBattleEnd)
        {
            yield break;
        }

        //HopeTile
        yield return StartCoroutine(IE_HopeApplyFragile());

        //BuffCheck
        yield return StartCoroutine(IE_FriendBuffCheck());
        if (isBattleEnd)
        {
            yield break;
        }

        //ResetAfter
        ResetNewTurnAfter();
        PublicTool.RecalculateOccupancy();
        yield return new WaitForEndOfFrame();
        yield return StartCoroutine(IE_StartCharacterTurnControl());
        yield break;
    }

    private IEnumerator IE_FriendBuffCheck()
    {        
        //If the buff such as burning is triggered 
        bool hasBuffInvoked = false;
        //Character
        for(int i = gameData.listCharacter.Count - 1; i >=0; i--)
        {
            BattleCharacterData characterData = gameData.listCharacter[i];
            bool isTriggered = characterData.CheckBuffTrigger();
            //Mark that buff is triggered
            if (isTriggered)
            {
                hasBuffInvoked = true;
                BattleCharacterView characterView = unitViewMgr.GetCharacterView(characterData.keyID);
                characterView.RequestBattleText();
            }

        }
        //Plant
        for (int i = gameData.listPlant.Count - 1; i >= 0; i--)
        {
            BattlePlantData plantData = gameData.listPlant[i];
            bool isTriggered = plantData.CheckBuffTrigger();
            //Mark that buff is triggered
            if (isTriggered)
            {
                hasBuffInvoked = true;
                BattlePlantView plantView = unitViewMgr.GetPlantView(plantData.keyID);
                plantView.RequestBattleText();
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

    private IEnumerator IE_StartCharacterTurnControl()
    {
        EventCenter.Instance.EventTrigger("CharacterPhaseStart", null);
        PublicTool.EventChangeInteract(InteractState.BattleNormal);
        //Auto Click
        List<BattleCharacterData> listCharacter = gameData.listCharacter;
        for (int i = 0; i < listCharacter.Count; i++)
        {
            if (!listCharacter[i].isDead)
            {
                EventCenter.Instance.EventTrigger("InputChooseCharacter", listCharacter[i].keyID);
                break;
            }
        }
        yield break;

    }
    #endregion

    public IEnumerator IE_CheckBuffConsume()
    {
        //Infinity Buff Check
        if(skillBattleInfo.activeSkillType == ActiveSkillType.SupportSkill || skillBattleInfo.activeSkillType == ActiveSkillType.DamageSkill)
        {
            if (skillSubject.CheckBuffExist(1003))
            {
                skillSubject.DecreaseBuff(1003);
            }
        }
        yield break;
    }


}
