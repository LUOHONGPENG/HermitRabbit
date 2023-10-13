using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{
    private void SkillEffectRequest(BattleUnitData source, BattleUnitData target, SkillEffectType effectType)
    {
        if (skillBattleInfo.damageDeltaFloat > 0)
        {
            SkillDamageLikeRequest(source, target, effectType);
        }

        if (skillBattleInfo.listSpecialEffect.Count > 0)
        {
            for(int i = 0; i < skillBattleInfo.listSpecialEffect.Count; i++)
            {
                SkillSpecialExcelItem skillSpecial = skillBattleInfo.listSpecialEffect[i];

                if(skillSpecial.effectType == effectType)
                {
                    SkillSpecialEffectDeal(source, target, skillSpecial.id);
                }
            }
        }
    }

    private void SkillDamageLikeRequest(BattleUnitData source, BattleUnitData target, SkillEffectType effectType)
    {
        float realDamage = 0;
        float damageSource = 0;
        switch (skillBattleInfo.damageDeltaStd)
        {
            case SkillDamageDeltaStd.ATK:
                damageSource = source.curATK * skillBattleInfo.damageDeltaFloat;
                break;
            case SkillDamageDeltaStd.MAXHP:
                damageSource = source.maxHP * skillBattleInfo.damageDeltaFloat;
                break;
        }

        switch (skillBattleInfo.damageType)
        {
            case SkillDamageType.Physical:
                realDamage = damageSource - target.curDEF;
                break;
            case SkillDamageType.Magic:
                realDamage = damageSource - target.curRES;
                break;
            case SkillDamageType.Real:
                realDamage = damageSource;
                break;
        }

        if (realDamage < 0)
        {
            realDamage = 0;
        }
        realDamage = Mathf.RoundToInt(realDamage);

        if(effectType == SkillEffectType.Harm)
        {
            target.GetHurt(realDamage);
            target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Damage, (-realDamage).ToString(),target.posID));
        }
        else if(effectType == SkillEffectType.Help)
        {
            target.GetHeal(realDamage);
            target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Heal, (realDamage).ToString(), target.posID));
        }
    }

    private void SkillSpecialEffectDeal(BattleUnitData source, BattleUnitData target,int specialEffectID)
    {
        switch (specialEffectID)
        {
            case 1001:
                target.curAP = target.curAP + 1;
                target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Special, "AP+1", target.posID));
                break;
            case 3001:
                target.posID = skillTargetPosExtra;
                target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Special, "Teleport!", target.posID));
                BattleUnitView moveView = unitViewMgr.GetViewFromUnitInfo(new UnitInfo(target.battleUnitType, target.keyID));
                moveView.MoveToPos();
                break;
        }

    }
}
