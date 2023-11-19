using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleUnitData
{
    public bool isDead = false;

    public virtual void ResetNewTurnBefore() { }

    public virtual void ResetNewTurnAfter() { }
    public virtual void ResetBattleEnd() { }

    public virtual void InvokeDead() { }


    public float GetHurt(float damage,bool canReduceDamages)
    {
        if (damage > 0)
        {
            //ReduceDamage
            if (canReduceDamages)
            {
                damage -= buffReduceHurt;
                damage -= damage * buffReduceHurtRate;
            }

            if (CheckBuffExist(1001) && damage > curHP)
            {
                //Final Guard
                damage = curHP - 1;
            }
            //Curse //Fragile
            if (CheckBuffExist(2001))
            {
                AddBuff(4002,1);
            }

            if (damage < 0)
            {
                damage = 0;
            }
            else
            {
                damage = Mathf.RoundToInt(damage);
            }
        }
        curHP -= damage;
        EventCenter.Instance.EventTrigger("UnitUIRefresh", null);
        if (curHP <= 0)
        {
            curHP = 0;
            isDead = true;
            InvokeDead();
        }
        return damage;
    }

    public void GetHeal(float healPoint)
    {
        curHP += healPoint;
        EventCenter.Instance.EventTrigger("UnitUIRefresh", null);
        if (curHP >= curMaxHP)
        {
            curHP = curMaxHP;
        }
    }
}
