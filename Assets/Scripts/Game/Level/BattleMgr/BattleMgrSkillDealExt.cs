using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{
    private void SkillEffectRequest(BattleUnitData source, BattleUnitData target, SkillEffectType effectType)
    {
        //Special Effect
        if (skillBattleInfo.listSpecialEffect.Count > 0)
        {
            for (int i = 0; i < skillBattleInfo.listSpecialEffect.Count; i++)
            {
                SkillSpecialExcelItem skillSpecial = skillBattleInfo.listSpecialEffect[i];
                if (skillSpecial.effectType == effectType)
                {
                    SkillSpecialEffectDeal(source, target, skillSpecial.id, skillBattleInfo.listSpecialDelta[i]);
                }
            }
        }

        //Buff Effect
        if (skillBattleInfo.listBuffEffect.Count > 0)
        {
            for (int i = 0; i < skillBattleInfo.listBuffEffect.Count; i++)
            {
                BuffExcelItem buffItem = skillBattleInfo.listBuffEffect[i];
                if (buffItem.effectType == effectType)
                {
                    SkillBuffEffectDeal(target, buffItem.id, skillBattleInfo.listBuffDelta[i]);
                }
            }
        }

        //Damage Effect
        if (skillBattleInfo.damageDeltaFloat > 0)
        {
            SkillDamageLikeRequest(source, target, effectType);
        }
    }

    private void SkillDamageLikeRequest(BattleUnitData source, BattleUnitData target, SkillEffectType effectType)
    {
        float realDamage = 0;
        float damageSource = 0;
        //Calculate the source damage
        switch (skillBattleInfo.damageDeltaStd)
        {
            case SkillDamageDeltaStd.Const:
                damageSource = skillBattleInfo.damageDeltaFloat;
                break;
            case SkillDamageDeltaStd.ATK:
                damageSource = source.curATK * skillBattleInfo.damageDeltaFloat;
                break;
            case SkillDamageDeltaStd.ATKMOV:
                damageSource = source.curATK * skillBattleInfo.damageDeltaFloat;
                if (source.curMOV >= target.curMOV)
                {
                    damageSource += (source.curMOV - target.curMOV);
                }
                break;
            case SkillDamageDeltaStd.MAXHP:
                damageSource = source.maxHP * skillBattleInfo.damageDeltaFloat;
                break;
            case SkillDamageDeltaStd.RES:
                damageSource = source.curRES * skillBattleInfo.damageDeltaFloat;
                break;
        }
        damageSource += skillBattleInfo.damageModifier;

        if (effectType == SkillEffectType.Harm)
        {
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
            int NormalizedDamage = NormalizeRealDamage(realDamage);
            target.GetHurt(realDamage);
            target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Damage, (-realDamage).ToString(), target.posID));
        }
        else if (effectType == SkillEffectType.Help)
        {
            realDamage = damageSource;
            int NormalizedDamage = NormalizeRealDamage(realDamage);
            target.GetHeal(realDamage);
            target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Heal, (realDamage).ToString(), target.posID));
        }
    }

    public int NormalizeRealDamage(float damage)
    {
        if (damage < 0)
        {
            damage = 0;
        }
        int NormalizedDamage = Mathf.RoundToInt(damage);
        return NormalizedDamage;
    }

    private void SkillBuffEffectDeal(BattleUnitData target, int buffID, int delta)
    {
        target.AddBuff(buffID, delta);
    }

    private void SkillSpecialEffectDeal(BattleUnitData source, BattleUnitData target,int specialEffectID,int delta)
    {
        switch (specialEffectID)
        {
            case 1001:
                target.curAP = target.curAP + delta;
                target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Special, string.Format("AP+{0}", delta), target.posID));
                break;
            case 2001:
                target.curMOV -= delta;
                if (target.curMOV < 0)
                {
                    target.curMOV = 0;
                }
                target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Debuff, string.Format("MOV-{0}", delta), target.posID));
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
