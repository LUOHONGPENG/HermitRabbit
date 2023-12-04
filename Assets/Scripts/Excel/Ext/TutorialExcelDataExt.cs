using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TutorialExcelData
{
    public Dictionary<TutorialGroup, List<TutorialExcelItem>> dicTutorial = new Dictionary<TutorialGroup, List<TutorialExcelItem>>();

    public void Init()
    {
        dicTutorial.Clear();

        for (int i = 0; i < items.Length; i++)
        {
            TutorialExcelItem tempItem = items[i];

            if (dicTutorial.ContainsKey(tempItem.group))
            {
                List<TutorialExcelItem> listTemp = dicTutorial[tempItem.group];
                listTemp.Add(tempItem);
            }
            else
            {
                List<TutorialExcelItem> listTemp = new List<TutorialExcelItem>();
                listTemp.Add(tempItem);
                dicTutorial.Add(tempItem.group, listTemp);
            }
        }
    }

    public List<TutorialExcelItem> GetTutorialGroup(TutorialGroup tutorialGroup)
    {
        if (dicTutorial.ContainsKey(tutorialGroup))
        {
            return dicTutorial[tutorialGroup];
        }
        else
        {
            return null;
        }
    }

}

public partial class TutorialExcelItem
{
    public string GetTitle()
    {
        if (GameGlobal.languageType == LanguageType.CN)
        {
            return strTitle_CN;
        }
        else
        {
            return strTitle_EN;
        }
    }

    public string GetContent()
    {

        if (GameGlobal.languageType == LanguageType.CN)
        {
            return strContent_CN;
        }
        else
        {
            return strContent_EN;
        }
    }
}
