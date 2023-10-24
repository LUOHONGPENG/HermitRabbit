using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{

    private IEnumerator IE_WholeStartCharacterTurn()
    {
        yield return StartCoroutine(IE_FriendBuffCheck());
        if (isBattleEnd)
        {
            yield break;
        }
        ResetNewTurn();
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
            }
        }
        yield break;

    }
}
