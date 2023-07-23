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
            target.EnqueueBattleText(new BattleTextInfo(BattleTextType.Damage, (-realDamage).ToString()));
        }
        else if(effectType == SkillEffectType.Help)
        {
            target.GetHeal(realDamage);
            target.EnqueueBattleText(new BattleTextInfo(BattleTextType.Heal, (realDamage).ToString()));
        }
    }


}
