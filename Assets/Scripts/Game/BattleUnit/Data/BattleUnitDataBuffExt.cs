using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleUnitData
{
    public List<Buff> listBuff = new List<Buff>();
    public Dictionary<int,Buff> dicBuff = new Dictionary<int,Buff>();

    public void AddBuff(int id,int level)
    {
        if (!dicBuff.ContainsKey(id))
        {
            Buff buff = new Buff(id, level);
            listBuff.Add(buff);
            dicBuff.Add(id, buff);
        }
    }

    public void RemoveBuff(int id)
    {
        if (dicBuff.ContainsKey(id))
        {
            Buff buff = dicBuff[id];
            listBuff.Remove(buff);
            dicBuff.Remove(id);
        }
    }

}

public class Buff
{
    public int id;
    public int level;

    public Buff(int id, int level)
    {
        this.id = id;
        this.level = level;
    }

    public int GetLevel()
    {
        return level;
    }
}
