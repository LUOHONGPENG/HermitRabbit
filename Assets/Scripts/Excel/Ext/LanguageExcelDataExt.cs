using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LanguageExcelData
{
    public Dictionary<string, string> dicKeyEN = new Dictionary<string, string>();
    public Dictionary<string, string> dicKeyCN = new Dictionary<string, string>();

    public void Init()
    {
        dicKeyEN.Clear();
        dicKeyCN.Clear();

        for(int i = 0; i < items.Length; i++)
        {
            LanguageExcelItem languageItem = items[i];
            string key = languageItem.key;
            if (!dicKeyEN.ContainsKey(key))
            {
                dicKeyEN.Add(key, languageItem.descEN);
            }
            if (!dicKeyCN.ContainsKey(key))
            {
                dicKeyCN.Add(key, languageItem.descCN);
            }
        }
    }


}
