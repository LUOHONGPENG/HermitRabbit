using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TalkExcelData
{
    public Dictionary<TalkGroup, List<TalkExcelItem>> dicTalk = new Dictionary<TalkGroup, List<TalkExcelItem>>();
    
    public void Init()
    {
        dicTalk.Clear();

        for (int i = 0; i < items.Length; i++)
        {
            TalkExcelItem talk = items[i];

            if (dicTalk.ContainsKey(talk.group))
            {
                dicTalk[talk.group].Add(talk);
            }
            else
            {
                List<TalkExcelItem> listTalk = new List<TalkExcelItem>();
                listTalk.Add(talk);
                dicTalk.Add(talk.group, listTalk);
            }

        }
    }

    public List<TalkExcelItem> GetTalkGroup(TalkGroup talkGroup)
    {
        if (dicTalk.ContainsKey(talkGroup))
        {
            return dicTalk[talkGroup];
        }
        else
        {
            return null;
        }
    }
}
public partial class TalkExcelItem
{
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
