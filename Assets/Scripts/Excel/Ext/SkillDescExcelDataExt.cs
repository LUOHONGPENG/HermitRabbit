using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public partial class SkillDescExcelData
{
    public Dictionary<int, List<SkillTag>> dicSkillTag = new Dictionary<int, List<SkillTag>>();


    public void Init()
    {
        dicSkillTag.Clear();

        for (int i = 0; i < items.Length; i++)
        {
            SkillDescExcelItem item = items[i];

            List<string> listStrTag = item.listTagType;
            List<SkillTag> listTag = new List<SkillTag>();

            if(listStrTag[0] != "0")
            {
                for (int j = 0; j < listStrTag.Count; j++)
                {
                    if (listStrTag[0] == "0")
                    {
                        break;
                    }
                    SkillTag tempTag = (SkillTag)System.Enum.Parse(typeof(SkillTag), listStrTag[j]);
                    listTag.Add(tempTag);
                }
            }

            dicSkillTag.Add(item.id, listTag);
        }
    }




}

public partial class SkillDescExcelItem
{


    public string GetName()
    {
        if(GameGlobal.languageType == LanguageType.CN)
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
