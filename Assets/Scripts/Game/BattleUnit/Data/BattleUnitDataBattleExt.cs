using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleUnitData
{
    public bool isDead = false;

    public virtual void ResetNewTurn() { }

    public void GetHurt(float damage)
    {
        curHP -= damage;
        if (curHP <= 0)
        {
            isDead = true;
        }
    }

    public void GetHeal(float healPoint)
    {
        curHP += healPoint;
        if (curHP >= maxHP)
        {
            curHP = maxHP;
        }
    }
}
