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
        InitButton(delegate ()
        {
            PublicTool.EventChangeInteract(InteractState.CharacterSkill, skillItem.id);
        });
    }

    public SkillExcelItem GetSkillItem()
    {
        return skillItem;
    }

    
}
