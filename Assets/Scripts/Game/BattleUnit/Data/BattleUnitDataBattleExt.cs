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

                if (CheckBuffExist(3002))
                {
                    if(damage > GetBuffLevel(3002))
                    {
                        damage -= GetBuffLevel(3002);
                        RemoveBuff(3002);
                    }
                    else
                    {
                        DecreaseBuffNum(3002,Mathf.CeilToInt(damage));
                        damage = 0;
                    }
                }
            }

            if (CheckBuffExist(1001))
            {
                if(damage >= curHP)
                {
                    //Final Guard
                    damage = curHP - 1;
                }
            }
            //Curse //Fragile
            if (damage > 0 && CheckBuffExist(2001))
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

    public float GetHeal(float healPoint)
    {
        float RealHealPoint = healPoint + buffHeal;

        if(RealHealPoint > 0)
        {
            curHP += RealHealPoint;
            EventCenter.Instance.EventTrigger("UnitUIRefresh", null);
            if (curHP >= curMaxHP)
            {
                curHP = curMaxHP;
            }
            return RealHealPoint;
        }
        else
        {
            return 0;
        }

    }
}
