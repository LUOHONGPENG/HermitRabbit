using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleUnitData
{
    public bool isDead = false;

    public virtual void ResetNewTurn() { }
    public virtual void ResetBattleEnd() { }

    public virtual void InvokeDead() { }


    public void GetHurt(float damage)
    {
        curHP -= damage;
        EventCenter.Instance.EventTrigger("UnitUIRefresh", null);
        if (curHP <= 0)
        {
            curHP = 0;
            isDead = true;
            InvokeDead();
        }
    }

    public void GetHeal(float healPoint)
    {
        curHP += healPoint;
        EventCenter.Instance.EventTrigger("UnitUIRefresh", null);
        if (curHP >= maxHP)
        {
            curHP = maxHP;
        }
    }
}
