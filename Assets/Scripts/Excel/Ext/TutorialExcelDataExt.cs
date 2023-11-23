using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TutorialExcelData 
{
    public Dictionary<TutorialGroup, List<TutorialExcelItem>> dicTutorial = new Dictionary<TutorialGroup, List<TutorialExcelItem>>();

    public void Init()
    {
        dicTutorial.Clear();

        for(int i = 0; i < items.Length; i++)
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

}
