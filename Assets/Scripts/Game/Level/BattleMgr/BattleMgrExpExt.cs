using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{
    public int expTotal = 0;
    public Dictionary<int, int> dicCharacterExp = new Dictionary<int, int>();
    public int expOther = 0;

    public void AddCharacterExp(int exp)
    {
        if(battleTurnPhase == BattlePhase.CharacterPhase)
        {
            int characterID = gameData.GetCurUnitData().typeID;

            //If plant kill the enemy
            if (plantRecordCharacter != null)
            {
                characterID = plantRecordCharacter.typeID;
            }

            Debug.Log(characterID + " " + exp);

            if (characterID == 1001)
            {
                if (dicCharacterExp.ContainsKey(characterID))
                {
                    dicCharacterExp[characterID] += exp;
                }
                else
                {
                    dicCharacterExp.Add(characterID, exp);
                }
            }
            else if (characterID == 1002)
            {
                if (dicCharacterExp.ContainsKey(characterID))
                {
                    dicCharacterExp[characterID] += exp;
                }
                else
                {
                    dicCharacterExp.Add(characterID, exp);
                }
            }
            else
            {
                expOther += exp;
            }
        }
        else
        {
            //If enemy kill himself
            expOther += exp;
        }
    }

    public int GetCharacterExp(int characterID)
    {
        int temp = expTotal;
        if (dicCharacterExp.ContainsKey(characterID))
        {
            temp += dicCharacterExp[characterID];
        }
        temp += (expOther) / 2;
        return temp;
    }

    public void ResetCharacterExp()
    {
        if (ExcelDataMgr.Instance.dayExcelData.dicDayExp.ContainsKey(gameData.numDay))
        {
            expTotal = ExcelDataMgr.Instance.dayExcelData.dicDayExp[gameData.numDay];
        }
        else
        {
            expTotal = 0;
        }
        dicCharacterExp.Clear();
        expOther = 0;
    }
}
