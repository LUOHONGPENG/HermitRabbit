using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSkillBtnItem : BattleBtnBase
{
    public Image imgIcon;

    private SkillExcelItem skillItem;


    public void Init(SkillExcelItem skill)
    {
        this.skillItem = skill;

        imgIcon.sprite  = Resources.Load("Sprite/Skill/" + skill.iconUrl, typeof(Sprite)) as Sprite;


        InitButton(delegate ()
        {
            EventCenter.Instance.EventTrigger("InputChangeSkill", null);
            PublicTool.EventChangeInteract(InteractState.CharacterSkill, skillItem.id);
        });
    }

    public SkillExcelItem GetSkillItem()
    {
        return skillItem;
    }

    
}
