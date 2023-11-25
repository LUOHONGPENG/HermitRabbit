using ICSharpCode.SharpZipLib.Tar;
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

    public int GetBuffLevel(int id)
    {
        if (dicBuff.ContainsKey(id))
        {
            return dicBuff[id].level;
        }
        else
        {
            return 0;
        }
    }

    public bool AddBuff(int id,int level)
    {
        if (!CheckBuffExist(id))
        {
            Buff buff = new Buff(id, level);
            listBuff.Add(buff);
            dicBuff.Add(id, buff);
            EventCenter.Instance.EventTrigger("UnitUIRefresh", null);
            return true;
        }
        else
        {
            Buff buff = dicBuff[id];
            buff.AddLevel(level);
            EventCenter.Instance.EventTrigger("UnitUIRefresh", null);
            return false;
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
            EventCenter.Instance.EventTrigger("UnitUIRefresh", null);
        }
    }
    public void DecreaseBuffNum(int id,int num)
    {
        if (CheckBuffExist(id))
        {
            Buff buff = dicBuff[id];
            buff.level-= num;
            if (buff.level <= 0)
            {
                RemoveBuff(id);
            }
            EventCenter.Instance.EventTrigger("UnitUIRefresh", null);
        }
    }

    public void HalfBuff(int id)
    {
        if (CheckBuffExist(id))
        {
            Buff buff = dicBuff[id];
            if(buff.level <= 1)
            {
                RemoveBuff(id);
            }
            else
            {
                buff.level = buff.level/2;
            }
            EventCenter.Instance.EventTrigger("UnitUIRefresh", null);
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

    public void TurnBuffDecreaseBefore()
    {
        for(int i = listBuff.Count - 1; i >= 0; i--)
        {
            Buff tarBuff = listBuff[i];
            if(tarBuff.countDownType == BuffCountDownType.BeforeReset)
            {
                switch (tarBuff.counterType)
                {
                    case BuffCounterType.TurnDecrease:
                        DecreaseBuff(tarBuff.id);
                        break;
                    case BuffCounterType.TurnHalf:
                        HalfBuff(tarBuff.id);
                        break;
                    case BuffCounterType.TurnRemove:
                        RemoveBuff(tarBuff.id);
                        break;
                }
            }
        }
    }

    public void TurnBuffDecreaseAfter()
    {
        for (int i = listBuff.Count - 1; i >= 0; i--)
        {
            Buff tarBuff = listBuff[i];
            if (tarBuff.countDownType == BuffCountDownType.AfterReset)
            {
                switch (tarBuff.counterType)
                {
                    case BuffCounterType.TurnDecrease:
                        DecreaseBuff(tarBuff.id);
                        break;
                    case BuffCounterType.TurnHalf:
                        HalfBuff(tarBuff.id);
                        break;
                    case BuffCounterType.TurnRemove:
                        RemoveBuff(tarBuff.id);
                        break;
                }
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
    #endregion


    #region BuffExtend

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
                    case 3001:
                        int FinalHeal = Mathf.RoundToInt(GetHeal(buffInfo.level));
                        EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Heal, FinalHeal.ToString(), posID));
                        isTriggered = true;
                        break;
                    case 4001:
                        int FinalBurn = Mathf.RoundToInt(GetHurt(buffInfo.level,true));
                        EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Damage, (-FinalBurn).ToString(), posID));
                        isTriggered = true;
                        break;
                }
            }
        }
        return isTriggered;
    }

    public void DoubleAllBuff()
    {
        for(int i = 0; i < listBuff.Count; i++)
        {
            if (listBuff[i].effectType == SkillEffectType.Help)
            {
                listBuff[i].DoubleLevel();
            }
        }
        EventCenter.Instance.EventTrigger("UnitUIRefresh", null);

    }

    public void DoubleAllDebuff()
    {
        for (int i = 0; i < listBuff.Count; i++)
        {
            if (listBuff[i].effectType == SkillEffectType.Harm)
            {
                listBuff[i].DoubleLevel();
            }
        }
        EventCenter.Instance.EventTrigger("UnitUIRefresh", null);
    }

    public void ClearFirstBuff()
    {
        for (int i = 0; i < listBuff.Count; i++)
        {
            if (listBuff[i].effectType == SkillEffectType.Help)
            {
                RemoveBuff(listBuff[i].id);
                break;
            }
        }
        EventCenter.Instance.EventTrigger("UnitUIRefresh", null);
    }
    public void ClearFirstDebuff()
    {
        for (int i = 0; i < listBuff.Count; i++)
        {
            if (listBuff[i].effectType == SkillEffectType.Harm)
            {
                RemoveBuff(listBuff[i].id);
                break;
            }
        }
        EventCenter.Instance.EventTrigger("UnitUIRefresh", null);
    }
    #endregion


}

public class Buff
{
    public int id;
    public int level;
    public int maxLevel;
    public BuffCounterType counterType;
    public BuffCountDownType countDownType;
    public SkillEffectType effectType;

    public Buff(int id, int level)
    {
        this.id = id;
        this.level = level;
        BuffExcelItem buffItem = PublicTool.GetBuffExcelItem(id);
        this.maxLevel = buffItem.maxLevel;
        this.counterType = buffItem.counterType;
        this.countDownType = buffItem.countDownType;
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
