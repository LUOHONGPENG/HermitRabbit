using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FoeExcelItem
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
