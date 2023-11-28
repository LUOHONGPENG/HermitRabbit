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

        int calculateATK = source.curATK;

        //Calculate the source damage
        switch (skillBattleInfo.damageDeltaStd)
        {
            case SkillDamageDeltaStd.CONST:
                damageSource = skillBattleInfo.damageDeltaFloat;
                break;
            case SkillDamageDeltaStd.ATK:
                damageSource = calculateATK * skillBattleInfo.damageDeltaFloat;
                break;
            case SkillDamageDeltaStd.ATKMOV:
                damageSource = calculateATK * skillBattleInfo.damageDeltaFloat;
                if (source.curMOV >= target.curMOV)
                {
                    damageSource += 2 * (source.curMOV - target.curMOV);
                }
                break;
            case SkillDamageDeltaStd.ATKDISD1:
                damageSource = calculateATK * skillBattleInfo.damageDeltaFloat;
                damageSource -= (PublicTool.CalculateGlobalDis(skillTargetPos, target.posID) / 1);
                break;
            case SkillDamageDeltaStd.ATKDISD2:
                damageSource = calculateATK * skillBattleInfo.damageDeltaFloat;
                damageSource -= (PublicTool.CalculateGlobalDis(skillTargetPos, target.posID) / 2);
                break;
            case SkillDamageDeltaStd.ATKDISD3:
                damageSource = calculateATK * skillBattleInfo.damageDeltaFloat;
                damageSource -= (PublicTool.CalculateGlobalDis(skillTargetPos, target.posID) / 3);
                break;
            case SkillDamageDeltaStd.MAXHP:
                damageSource = source.curMaxHP * skillBattleInfo.damageDeltaFloat;
                break;
            case SkillDamageDeltaStd.COSTHP:
                damageSource = skillBattleInfo.costHP * skillBattleInfo.damageDeltaFloat;
                skillBattleInfo.damageModifier = gameData.RefreshBattleSkillForCostHP();
                break;
            case SkillDamageDeltaStd.CURHP:
                damageSource = source.curHP * skillBattleInfo.damageDeltaFloat;
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
        damageSource *= (1 + skillBattleInfo.damageExtraBonus);
        //Make sure the damage is not lower than 0
        if (damageSource <= 0)
        {
            damageSource = 0;
        }

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
                realDamage += realDamage * target.buffAddHurtRate;
                realDamage += target.buffAddHurt;

                int NormalizedDamage = NormalizeRealDamage(realDamage);
                int FinalDamage = NormalizeRealDamage(target.GetHurt(NormalizedDamage, true));
                target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Damage, (-FinalDamage).ToString(), target.posID));

                //Absorb
                if (source.GetBuffLevel(1007) > 0)
                {
                    int absorbHP = (FinalDamage + 1) / 2;
                    int FinalHeal = NormalizeRealDamage(source.GetHeal(absorbHP));
                    source.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Heal, (FinalHeal).ToString(), source.posID));
                    source.DecreaseBuff(1007);
                }
            }

            //Counter
            if (target.curCounter > 0)
            {
                int counterHurt = target.curCounter + source.buffAddHurt;
                source.GetHurt(counterHurt, true);
                source.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Damage, (-counterHurt).ToString(), source.posID));
            }
        }
        else if (effectType == SkillEffectType.Help)
        {
            realDamage = damageSource;
            int NormalizedDamage = NormalizeRealDamage(realDamage);
            int FinalHeal = NormalizeRealDamage(target.GetHeal(NormalizedDamage));
            target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Heal, (FinalHeal).ToString(), target.posID));
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
        if (target == null)
        {
            return;
        }

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
                SkillBuffEffectDeal(target, 4001, delta, PublicTool.GetBuffExcelItem(4001).name, SkillEffectType.Harm);
/*                target.AddBuff(4001, delta);
                BuffExcelItem buffItem4001 = PublicTool.GetBuffExcelItem(4001);
                target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Debuff, buffItem4001.name, target.posID));*/
                break;
            case 3003:
                SkillBuffEffectDeal(source, 1007, delta, PublicTool.GetBuffExcelItem(1007).name, SkillEffectType.Help);
/*                source.AddBuff(1007,delta);
                BuffExcelItem buffItem1007 = PublicTool.GetBuffExcelItem(1007);
                source.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Buff, buffItem1007.name, source.posID));*/
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
            case 8001:
                int ranMilkTea = UnityEngine.Random.Range(0, 3);
                int tempMilkBuffID = -1;
                switch (ranMilkTea)
                {
                    case 0:
                        tempMilkBuffID = 5001;
                        //SkillBuffEffectDeal(target, 4001, 4, PublicTool.GetBuffExcelItem(4001).name, SkillEffectType.Harm);
                        /*                        target.AddBuff(4001, 3);
                                                BuffExcelItem burnBuffItem4001 = PublicTool.GetBuffExcelItem(4001);
                                                target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Debuff, burnBuffItem4001.name, target.posID));*/
                        break;
                    case 1:
                        tempMilkBuffID = 5002;
                        //SkillBuffEffectDeal(target, 4002, 3, PublicTool.GetBuffExcelItem(4002).name, SkillEffectType.Harm);
                        /*                        target.AddBuff(2001, 3);
                                                BuffExcelItem burnBuffItem4002 = PublicTool.GetBuffExcelItem(4002);
                                                target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Debuff, burnBuffItem4002.name, target.posID));*/
                        break;
                    case 2:
                        tempMilkBuffID = 5003;
                        break;
/*                        target.curMOV -= 2;
                        if (target.curMOV < 0)
                        {
                            target.curMOV = 0;
                        }
                        target.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Debuff, string.Format("MOV-{0}", 2), target.posID));
                        break;*/
                }
                SkillBuffEffectDeal(target, tempMilkBuffID, 2, PublicTool.GetBuffExcelItem(tempMilkBuffID).name, SkillEffectType.Help);
                break;
            case 9001:
                int ranFoe = UnityEngine.Random.Range(0, 3);
                int foeRandomID = -1;
                switch (ranFoe)
                {
                    case 0:
                        foeRandomID = 1022;
                        break;
                    case 1:
                        foeRandomID = 1098;
                        break;
                    case 2:
                        foeRandomID = 1097;
                        break;
                }
                BattleFoeData newFoeData = gameData.GenerateFoeData(foeRandomID);
                FoeExcelItem foeItemData = PublicTool.GetFoeExcelItem(foeRandomID);
                List<Vector2Int> listPos = PublicTool.GetEmptyPosFromRowRange(3 - foeItemData.pos0, 3);
                if (listPos.Count > 0)
                {
                    int ran = UnityEngine.Random.Range(0, listPos.Count);
                    newFoeData.posID = listPos[ran];
                }
                unitViewMgr.GenerateFoeView(newFoeData);
                break;
            case 9998:
                source.GetHurt(99,false);
                break;
            case 9999:
                source.GetHurt(99,false);
                break;
        }

    }


    private void SkillTileEffectDeal(Vector2Int posID, SkillTileEffectType tileEffectType)
    {
        MapTileData tileData;

        switch (tileEffectType)
        {
            case SkillTileEffectType.Burn:
            case SkillTileEffectType.BurnUnitOnly:
                if (gameData.GetMapTileData(posID) !=null)
                {
                    tileData = gameData.GetMapTileData(posID);
                    if(tileData.curMapTileStatus == MapTileStatus.CanBurn)
                    {
                        tileData.isBurning = true;
                    }
                }
                break;
            case SkillTileEffectType.Water:
                if (gameData.GetMapTileData(posID) != null)
                {
                    tileData = gameData.GetMapTileData(posID);
                    if (tileData.curMapTileStatus == MapTileStatus.Burning)
                    {
                        tileData.isBurning = false;
                    }
                }
                break;
            case SkillTileEffectType.WaterPlus:
                if (gameData.GetMapTileData(posID) != null)
                {
                    tileData = gameData.GetMapTileData(posID);
                    if (tileData.curMapTileStatus == MapTileStatus.Burning)
                    {
                        tileData.isBurning = false;
                    }
                    else if(tileData.curMapTileStatus == MapTileStatus.CanWet)
                    {
                        tileData.isWet = true;
                    }
                }
                break;
            case SkillTileEffectType.Break:
                if (gameData.GetMapTileData(posID) != null)
                {
                    tileData = gameData.GetMapTileData(posID);
                    if (tileData.curMapTileStatus == MapTileStatus.CanBreak)
                    {
                        tileData.isBroken = true;
                        int ranCanSpecial = UnityEngine.Random.Range(0, 100);
                        if (ranCanSpecial < 40)
                        {
                            int ran = UnityEngine.Random.Range(0, 5);
                            tileData.breakResultID = ran;
                        }
                    }
                }
                break;
        }
    }
}
