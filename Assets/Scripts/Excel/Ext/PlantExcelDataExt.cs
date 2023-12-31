using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlantExcelData
{
    public List<int> GetAllPlantID()
    {
        List<int> listTemp = new List<int>();
        for (int i = 0; i < items.Length; i++)
        {
            listTemp.Add(items[i].id);
        }
        return listTemp;
    }

}
public partial class PlantExcelItem
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