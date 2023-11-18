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


    public float GetHurt(float damage)
    {
        if (damage > 0)
        {
            if (CheckBuffExist(1001) && damage > curHP)
            {
                damage = curHP - 1;
                Debug.Log("ReduceDamage");
            }
            if (CheckBuffExist(2001))
            {
                AddBuff(4002,1);
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
