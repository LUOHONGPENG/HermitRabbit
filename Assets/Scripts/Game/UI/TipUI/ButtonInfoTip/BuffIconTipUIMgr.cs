using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffIconTipUIMgr : ButtonInfoTipUIMgr
{
    [Header("BasicInfo")]
    public TextMeshProUGUI codeName;
    public TextMeshProUGUI codeDesc;
    public Image imgIcon;

    public void ShowTip(int buffID, Vector2 mousePos)
    {
        if (!objPopup.activeSelf || recordID != buffID)
        {
            BuffExcelItem buffItem = PublicTool.GetBuffExcelItem(buffID);

            imgIcon.sprite = Resources.Load("Sprite/Buff/" + buffItem.iconUrl, typeof(Sprite)) as Sprite;

            codeName.text = buffItem.GetName();
            codeDesc.text = buffItem.GetDesc();
        }

        ShowTipSetPos(mousePos);

    }
}
