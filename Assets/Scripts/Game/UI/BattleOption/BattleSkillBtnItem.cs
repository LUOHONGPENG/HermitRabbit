using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSkillBtnItem : BattleBtnBase
{
    public Image imgIcon;

    private CharacterSkillExcelItem skillItem;


    public void Init(CharacterSkillExcelItem skill)
    {
        this.skillItem = skill;
        InitButton(delegate ()
        {
            PublicTool.EventChangeInteract(InteractState.CharacterSkill, skillItem.id);
        });
    }

    public CharacterSkillExcelItem GetSkillItem()
    {
        return skillItem;
    }
}
