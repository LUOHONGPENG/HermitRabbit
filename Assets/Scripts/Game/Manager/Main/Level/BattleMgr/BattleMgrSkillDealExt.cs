using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{
    private void SkillHarmRequest(BattleUnitData source,BattleUnitData target)
    {
        //Damage
        if (skillBattleInfo.damageDeltaFloat > 0)
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

            realDamage = Mathf.RoundToInt(realDamage);
            
            //damage - target.curDEF;
            target.GetHurt(realDamage);
            target.EnqueueBattleText(new BattleTextInfo(BattleTextType.Damage, (-realDamage).ToString()));
        }



    }
}
