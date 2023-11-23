using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkillTagExcelData
{
    public Dictionary<SkillTag, SkillTagExcelItem> dicSkillTag = new Dictionary<SkillTag, SkillTagExcelItem>();

    public void Init()
    {
        dicSkillTag.Clear();

        for(int i = 0;i < items.Length;i++)
        {
            dicSkillTag.Add(items[i].tag, items[i]);
        }
    }

    public SkillTagExcelItem GetItem(SkillTag tag)
    {
        if (dicSkillTag.ContainsKey(tag))
        {
            return dicSkillTag[tag];
        }
        else
        {
            return null;
        }
    }
}

public partial class SkillTagExcelItem
{
    


}