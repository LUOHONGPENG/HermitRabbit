using System;
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
                if (skillSpecial.timeType == SpecialEffectTimeType.BeforeDamage && skillSpecial.effectType == effectType)
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
                    SkillBuffEffectDeal(target, buffItem.id, skillBattleInfo.listBuffDelta[i], buffItem.name, effectType);
                }
            }
        }

        //Damage Effect
        if (skillBattleInfo.damageDeltaFloat > 0)
        {
            SkillDamageLikeRequest(source, target, effectType);
        }

        //Special Effect
        if (skillBattleInfo.listSpecialEffect.Count > 0)
        {
            for (int i = 0; i < skillBattleInfo.listSpecialEffect.Count; i++)
            {
                SkillSpecialExcelItem skillSpecial = skillBattleInfo.listSpecialEffect[i];
                if (skillSpecial.timeType == SpecialEffectTimeType.AfterDamage && skillSpecial.effectType == effectType)
                {
                    SkillSpecialEffectDeal(source, target, skillSpecial.id, skillBattleInfo.listSpecialDelta[i]);
                }
            }
        }
    }

    private void SkillDamageLikeRequest(BattleUnitData source, BattleUnitData target, SkillEffectType effectType)
    {
        float realDamage = 0;
        float damageSource = 0;
        //Calculate the source damage
        switch (skillBattleInfo.damageDeltaStd)
        {
            case SkillDamageDeltaStd.CONST:
                damageSource = skillBattleInfo.damageDeltaFloat;
                break;
            case SkillDamageDeltaStd.ATK:
                damageSource = source.curATK * skillBattleInfo.damageDeltaFloat;
                break;
            case SkillDamageDeltaStd.ATKMOV:
                damageSource = source.curATK * skillBattleInfo.damageDeltaFloat;
                if (source.curMOV >= target.curMOV)
                {
                    damageSource += 2 * (source.curMOV - target.curMOV);
                }
                break;
            case SkillDamageDeltaStd.MAXHP:
                damageSource = source.curMaxHP * skillBattleInfo.damageDeltaFloat;
                break;
            case SkillDamageDeltaStd.COSTHP:
                damageSource = skillBattleInfo.costHP * skillBattleInfo.damageDeltaFloat;
                skillBattleInfo.damageModifier = gameData.RefreshBattleSkillForCostHP();
                break;
            case SkillDamageDeltaStd.DEF:
                damageSource = source.curDEF * skillBattleInfo.damageDeltaFloat;
                break;
            case SkillDamageDeltaStd.RES:
                damageSource = source.curRES * skillBattleInfo.damageDeltaFloat;
                break;
            case SkillDamageDeltaStd.BURN:
                int burnLevel = target.GetBuffLevel(4001);
                if (skillBattleInfo.radius > 0)
                {
                    List<Vector2Int> listPos = PublicTool.GetTargetCircleRange(target.posID, skillBattleInfo.radius);
                    foreach(Vector2Int pos in listPos)
                    {
                        if (dicPosEffectTarget.ContainsKey(pos))
                        {
                            BattleUnitData unitData = dicPosEffectTarget[pos];
                            if (unitData.GetBuffLevel(4001) > burnLevel)
                            {
                                burnLevel = unitData.GetBuffLevel(4001);
                            }
                        }
                    }
                }
                damageSource = burnLevel * skillBattleInfo.damageDeltaFloat;
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

            if (target.GetBuffLevel(1006)>0)
            {
                target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Special, "Dodge", target.posID));
                target.DecreaseBuff(1006);
            }
            else
            {
                realDamage += target.buffAddHurt;
                int NormalizedDamage = NormalizeRealDamage(realDamage);
                int FinalDamage = NormalizeRealDamage(target.GetHurt(NormalizedDamage));
                target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Damage, (-FinalDamage).ToString(), target.posID));

                //Absorb
                if (source.GetBuffLevel(1007) > 0)
                {
                    int absorbHP = (FinalDamage + 1) / 2;
                    source.GetHeal(absorbHP);
                    source.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Heal, (absorbHP).ToString(), source.posID));
                    source.DecreaseBuff(1007);
                }
            }

            //Counter
            if (target.curCounter > 0)
            {
                int counterHurt = target.curCounter + source.buffAddHurt;
                source.GetHurt(counterHurt);
                source.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Damage, (-counterHurt).ToString(), source.posID));
            }
        }
        else if (effectType == SkillEffectType.Help)
        {
            realDamage = damageSource;
            int NormalizedDamage = NormalizeRealDamage(realDamage);
            target.GetHeal(NormalizedDamage);
            target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Heal, (NormalizedDamage).ToString(), target.posID));
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

    private void SkillBuffEffectDeal(BattleUnitData target, int buffID, int delta, string buffName, SkillEffectType effect)
    {
        bool firstTimeBuff = target.AddBuff(buffID, delta);
        if(effect == SkillEffectType.Help)
        {
            target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Buff, buffName, target.posID));
        }
        else if(effect == SkillEffectType.Harm)
        {
            target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Debuff, buffName, target.posID));
        }

        //FirstTimeBuffSpecialEffect
        if (firstTimeBuff)
        {
            BuffExcelItem buff = PublicTool.GetBuffExcelItem(buffID);
            if (buff.firstSpecialEffect > 0)
            {
                SkillSpecialEffectDeal(null, target, buff.firstSpecialEffect, buff.firstSpecialDelta);
            }
        }
    }

    private void SkillSpecialEffectDeal(BattleUnitData source, BattleUnitData target,int specialEffectID,int delta)
    {
        switch (specialEffectID)
        {
            case 1001:
                target.curAP = target.curAP + delta;
                target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Buff, string.Format("AP+{0}", delta), target.posID));
                break;
            case 1002:
                target.curMOV = target.curMOV + delta;
                if (target.curMOV > target.curMaxMOV)
                {
                    target.curMOV = target.curMaxMOV;
                }
                target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Buff, string.Format("MOV+{0}", delta), target.posID));
                break;
            case 1003:
                target.curAP = target.curAP + delta;
                break;
            case 1004:
                target.curMOV = target.curMOV + delta;
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
                Vector2Int posDelta = target.posID - skillTargetPos;
                Vector2Int realTelePos = skillTargetPosExtra + posDelta;
                if (gameData.listTempEmptyPos.Contains(realTelePos))
                {
                    target.posID = realTelePos;
                    target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Special, "Teleport!", target.posID));
                    BattleUnitView moveView = unitViewMgr.GetViewFromUnitInfo(new UnitInfo(target.battleUnitType, target.keyID));
                    moveView.MoveToPos();
                }
                break;
            case 3002:
                target.AddBuff(4001, delta);
                BuffExcelItem burnBuffItem4001 = PublicTool.GetBuffExcelItem(4001);
                target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Debuff, burnBuffItem4001.name, target.posID));
                break;
            case 3003:
                source.AddBuff(1007,delta);
                BuffExcelItem burnBuffItem1007 = PublicTool.GetBuffExcelItem(1007);
                source.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Buff, burnBuffItem1007.name, source.posID));
                break;
            case 5001:
                target.DoubleAllBuff();
                target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Buff, string.Format("Buff Extend", delta), target.posID));
                break;
            case 5002:
                target.ClearFirstDebuff();
                target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Buff, string.Format("Clear Debuff", delta), target.posID));
                break;
            case 6001:
                target.DoubleAllDebuff();
                target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Debuff, string.Format("Debuff Extend", delta), target.posID));
                break;
            case 6002:
                target.ClearFirstBuff();
                target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Debuff, string.Format("Clear Buff", delta), target.posID));
                break;
            case 9001:
                BattleFoeData newFoeData = gameData.GenerateFoeData(delta);
                FoeExcelItem foeItemData = PublicTool.GetFoeExcelItem(delta);
                List<Vector2Int> listPos = PublicTool.GetEmptyPosFromRowRange(3 - foeItemData.pos0, 3);
                if (listPos.Count > 0)
                {
                    int ran = UnityEngine.Random.Range(0, listPos.Count);
                    newFoeData.posID = listPos[ran];
                }
                unitViewMgr.GenerateFoeView(newFoeData);
                break;
            case 9998:
                source.GetHurt(99);
                break;
            case 9999:
                source.GetHurt(99);
                break;


        }

    }
}
