using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleUnitData
{
    public List<Buff> listBuff = new List<Buff>();
    public Dictionary<int,Buff> dicBuff = new Dictionary<int,Buff>();

    #region Basic Buff
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
            EventCenter.Instance.EventTrigger("UnitUIRefresh", null);
        }
        else
        {
            Buff buff = dicBuff[id];
            buff.AddLevel(level);
            EventCenter.Instance.EventTrigger("UnitUIRefresh", null);
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
            EventCenter.Instance.EventTrigger("UnitUIRefresh", null);
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
        EventCenter.Instance.EventTrigger("UnitUIRefresh", null);
    }

    public void ClearAllBuff()
    {
        for (int i = listBuff.Count - 1; i >= 0; i--)
        {
            Buff tarBuff = listBuff[i];
            RemoveBuff(tarBuff.id);
        }
    }
    #endregion


    #region BuffTrigger

    public bool CheckBuffTrigger()
    {
        bool isTriggered = false;
        for(int i = 0; i < listBuff.Count; i++)
        {
            Buff buffInfo = listBuff[i];
            if (buffInfo.level > 0)
            {
                switch (listBuff[i].id)
                {
                    case 2001:
                        GetHurt(buffInfo.level);
                        EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Damage, (-buffInfo.level).ToString(), posID));
                        isTriggered = true;
                        break;
                }
            }
        }
        return isTriggered;
    }



    #endregion


}

public class Buff
{
    public int id;
    public int level;
    public int maxLevel;
    public BuffCounterType counterType;
    public SkillEffectType effectType;

    public Buff(int id, int level)
    {
        this.id = id;
        this.level = level;
        BuffExcelItem buffItem = PublicTool.GetBuffExcelItem(id);
        this.maxLevel = buffItem.maxLevel;
        this.counterType = buffItem.counterType;
        this.effectType = buffItem.effectType;

        CheckMaxLevel();
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int level)
    {
        this.level = level;
        CheckMaxLevel();
    }

    public void AddLevel(int level)
    {
        this.level += level;
        CheckMaxLevel();
    }

    public void DoubleLevel()
    {
        this.level = 2 * this.level;
        CheckMaxLevel();
    }

    private void CheckMaxLevel()
    {
        if (level > maxLevel)
        {
            this.level = maxLevel;
        }
    }
}
