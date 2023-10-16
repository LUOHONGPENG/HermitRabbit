using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleUnitData
{
    #region About Data Type
    /// <summary>
    ///  Basic Data
    /// </summary>
    public BattleUnitType battleUnitType;
    public int typeID = -1;
    public int keyID = -1;
    #endregion


    //The current HP of this unit
    public float curHP;
    //The maximum HP of this unit
    public float maxHP;

    public int curMOV = 0;
    public int maxMOV = 0;

    public int curAP = 1;
    public int maxAP = 1;

    public virtual int curATK { get; }
    public virtual int curDEF { get; }
    public virtual int curRES { get; }
    public virtual int curMaxMOV { get; }

    public virtual int buffATK 
    {
        get
        {
            int temp = 0;
            if (CheckBuffExist(1004))
            {
                temp++;
            }
            return temp;
        }
    }

    public virtual int buffDEF
    {
        get
        {
            int temp = 0;
            if (CheckBuffExist(1004))
            {
                temp++;
            }
            return temp;
        }
    }

    public virtual int buffRES
    {
        get
        {
            int temp = 0;
            if (CheckBuffExist(1004))
            {
                temp++;
            }
            return temp;
        }
    }

    public virtual int buffMOV
    {
        get
        {
            int temp = 0;
            if (CheckBuffExist(1004))
            {
                temp++;
            }
            return temp;
        }
    }

    public int GetTypeID()
    {
        return typeID;
    }

    public UnitInfo GetUnitInfo()
    {
        return new UnitInfo(battleUnitType, keyID);
    }




    #region BattleTextQueue

    private Queue<EffectBattleTextInfo> queueBattleText = new Queue<EffectBattleTextInfo>();

    public void ClearBattleTextQueue()
    {
        queueBattleText.Clear();
    }

    public void EnqueueBattleText(EffectBattleTextInfo info)
    {
        queueBattleText.Enqueue(info);
    }

    public Queue<EffectBattleTextInfo> GetQueueBattleText()
    {
        return queueBattleText;
    }
    #endregion
}
