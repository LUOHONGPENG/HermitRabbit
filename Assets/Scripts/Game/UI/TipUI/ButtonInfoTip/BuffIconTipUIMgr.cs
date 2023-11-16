using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffIconTipUIMgr : ButtonInfoTipUIMgr
{
    [Header("BasicInfo")]
    public Text codeName;
    public Text codeDesc;

    public void ShowTip(int buffID, Vector2 mousePos)
    {
        if (!objPopup.activeSelf || recordID != buffID)
        {
            BuffExcelItem buffItem = PublicTool.GetBuffExcelItem(buffID);

            codeName.text = buffItem.name;
            codeDesc.text = buffItem.desc;

        }

        ShowTipSetPos(mousePos);

    }
}
