using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkillNodeExcelData
{
    public Dictionary<int, List<SkillNodeExcelItem>> dicAllCharacterSkillNode = new Dictionary<int, List<SkillNodeExcelItem>>();

    public void Init()
    {
        dicAllCharacterSkillNode.Clear();

        for(int i = 0; i < items.Length; i++)
        {
            SkillNodeExcelItem nodeItem = items[i];
            if (dicAllCharacterSkillNode.ContainsKey(nodeItem.characterID))
            {
                List<SkillNodeExcelItem> listNode = dicAllCharacterSkillNode[nodeItem.characterID];
                listNode.Add(nodeItem);
            }
            else
            {
                List<SkillNodeExcelItem> listNode = new List<SkillNodeExcelItem>{ nodeItem };
                dicAllCharacterSkillNode.Add(nodeItem.characterID, listNode);
            }
        }
    }

}
