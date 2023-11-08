using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkillEffectViewExcelData
{
    public Dictionary<EffectViewType, SkillEffectViewExcelItem> dicSkillEffectView = new Dictionary<EffectViewType, SkillEffectViewExcelItem>();

    public void Init()
    {
        dicSkillEffectView.Clear();
        for(int i = 0; i < items.Length; i++)
        {
            SkillEffectViewExcelItem excelItem = items[i];
            if (!dicSkillEffectView.ContainsKey(excelItem.effectViewType))
            {
                dicSkillEffectView.Add(excelItem.effectViewType, excelItem);
            }
        }
    }

}
