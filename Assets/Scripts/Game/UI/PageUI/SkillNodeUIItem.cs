using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SkillNodeUIItem : MonoBehaviour
{
    public Image imgFrame;
    public Image imgIcon;

    public Button btnNode;

    private SkillNodeExcelItem nodeItem;

    public void Init(SkillNodeExcelItem nodeItem)
    {
        this.nodeItem = nodeItem;

        if(nodeItem.nodeType == SkillNodeType.Active)
        {
            imgFrame.sprite = Resources.Load("Sprite/Skill/imgIconSkill_Active", typeof(Sprite)) as Sprite;
        }
        else if(nodeItem.nodeType == SkillNodeType.Passive)
        {
            imgFrame.sprite = Resources.Load("Sprite/Skill/imgIconSkill_Passive", typeof(Sprite)) as Sprite;
        }
        imgIcon.sprite = Resources.Load("Sprite/Skill/" + nodeItem.iconUrl, typeof(Sprite)) as Sprite;

/*        btnNode.onClick.RemoveAllListeners();
        btnNode.onClick.AddListener(delegate ()
        {

        });*/
    }

}
