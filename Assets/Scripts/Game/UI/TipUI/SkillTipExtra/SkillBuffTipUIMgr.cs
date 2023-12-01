using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillBuffTipUIMgr : MonoBehaviour
{
    public Image imgIcon;
    public TextMeshProUGUI codeName;
    public TextMeshProUGUI codeDesc;

    public void Init(BuffExcelItem buffItem)
    {
        imgIcon.sprite = Resources.Load("Sprite/Buff/" + buffItem.iconUrl, typeof(Sprite)) as Sprite;

        codeName.text = buffItem.GetName();
        codeDesc.text = buffItem.GetDesc();
    }
}
