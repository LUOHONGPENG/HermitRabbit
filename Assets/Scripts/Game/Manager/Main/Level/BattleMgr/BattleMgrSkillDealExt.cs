using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{
    private void SkillDamageRequest(BattleUnitData source,BattleUnitData target,float damage)
    {
        //Simple Calculation
        float realDamage = damage - target.curDEF;
        target.GetHurt(realDamage);
        target.EnqueueBattleText(new BattleTextInfo(BattleTextType.Damage, (-realDamage).ToString()));
    }
}
