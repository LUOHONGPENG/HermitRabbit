using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTagTipUIMgr : MonoBehaviour
{
    public Image imgIcon;
    public TextMeshProUGUI codeName;
    public TextMeshProUGUI codeDesc;

    public void Init(SkillTag tag)
    {
        SkillTagExcelItem tagItem = PublicTool.GetSkillTagItem(tag);

        imgIcon.sprite = Resources.Load("Sprite/SkillType/" + tagItem.iconUrl, typeof(Sprite)) as Sprite;
        codeName.text = tagItem.name;
        codeDesc.text = tagItem.desc;
    }


}
