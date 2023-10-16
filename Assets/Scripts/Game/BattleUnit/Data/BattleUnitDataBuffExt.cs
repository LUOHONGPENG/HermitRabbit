using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleUnitData
{
    public List<Buff> listBuff = new List<Buff>();
    public Dictionary<int,Buff> dicBuff = new Dictionary<int,Buff>();

    public bool CheckBuffExist(int id)
    {
        if (dicBuff.ContainsKey(id))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddBuff(int id,int level)
    {
        if (!CheckBuffExist(id))
        {
            Buff buff = new Buff(id, level);
            listBuff.Add(buff);
            dicBuff.Add(id, buff);
        }
    }

    public void DecreaseBuff(int id)
    {
        if (CheckBuffExist(id))
        {
            Buff buff = dicBuff[id];
            buff.level--;
            if (buff.level <= 0)
            {
                RemoveBuff(id);
            }
        }
    }

    public void RemoveBuff(int id)
    {
        if (CheckBuffExist(id))
        {
            Buff buff = dicBuff[id];
            listBuff.Remove(buff);
            dicBuff.Remove(id);
        }
    }

    public void TurnBuffDecrease()
    {
        for(int i = listBuff.Count - 1; i >= 0; i--)
        {
            Buff tarBuff = listBuff[i];
            switch (tarBuff.counterType)
            {
                case BuffCounterType.TurnDecrease:
                    DecreaseBuff(tarBuff.id);
                    break;
                case BuffCounterType.TurnRemove:
                    RemoveBuff(tarBuff.id);
                    break;
            }
        }
    }

    public void ClearAllBuff()
    {
        for (int i = listBuff.Count - 1; i >= 0; i--)
        {
            Buff tarBuff = listBuff[i];
            RemoveBuff(tarBuff.id);
        }
    }

}

public class Buff
{
    public int id;
    public int level;
    public BuffCounterType counterType;
    public SkillEffectType effectType;

    public Buff(int id, int level)
    {
        this.id = id;
        this.level = level;
        BuffExcelItem buffItem = PublicTool.GetBuffExcelItem(id);
        this.counterType = buffItem.counterType;
        this.effectType = buffItem.effectType;
    }

    public int GetLevel()
    {
        return level;
    }
}
