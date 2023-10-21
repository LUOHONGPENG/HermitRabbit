using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CharacterExpExcelData
{
    public Dictionary<int, CharacterExpExcelItem> dicCharacterExpLevel = new Dictionary<int, CharacterExpExcelItem>();

    public void Init()
    {
        dicCharacterExpLevel.Clear();
        for (int i = 0; i < items.Length; i++)
        {
            CharacterExpExcelItem expItem = items[i];
            dicCharacterExpLevel.Add(expItem.Level, expItem);
        }
    }

    public CharacterExpExcelItem GetExpItem(int Level)
    {
        if (dicCharacterExpLevel.ContainsKey(Level))
        {
            return dicCharacterExpLevel[Level];
        }
        else
        {
            return null;
        }
    }

    public int GetLevelFromExp(int exp)
    {
        int tempLevel = 1;
        for(int i = 0; i < items.Length; i++)
        {
            CharacterExpExcelItem expItem = items[i];
            if (exp >= expItem.EXP)
            {
                tempLevel = expItem.Level;
            }
            else
            {
                break;
            }
        }
        return tempLevel;
    }

    public int GetRequiredExp(int level)
    {
        //ExcelDataMgr.Instance.characterExpExcelData.GetExpItem(Level+1).EXP - ExcelDataMgr.Instance.characterExpExcelData.GetExpItem(Level).EXP;
        if (GetExpItem(level + 1) != null && GetExpItem(level)!=null)
        {
            return (GetExpItem(level + 1).EXP - GetExpItem(level).EXP);
        }
        else
        {
            return -1;
        }

    }
}
