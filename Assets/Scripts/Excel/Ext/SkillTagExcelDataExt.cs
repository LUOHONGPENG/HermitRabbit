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
    public string GetName()
    {
        if (GameGlobal.languageType == LanguageType.CN)
        {
            return name_CN;
        }
        else
        {
            return name_EN;
        }
    }

    public string GetDesc()
    {
        if (GameGlobal.languageType == LanguageType.CN)
        {
            return desc_CN;
        }
        else
        {
            return desc_EN;
        }
    }
}