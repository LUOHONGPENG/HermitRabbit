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

    public virtual float curMaxHP
    {
        get
        {
            return maxHP;
        }
    }

    public float HPrate
    {
        get
        {
            return 1.0f * curHP / curMaxHP;
        }
    }

    public int curMOV = 0;
    public int maxMOV = 0;

    public int curAP = 1;
    public int maxAP = 1;

    public virtual int curATK { get; }
    public virtual int curDEF { get; }
    public virtual int curRES { get; }
    public virtual int curMaxMOV
    {
        get
        {
            int temp = maxMOV + buffMaxMOV;
            if (temp < 0)
            {
                temp = 0;
            }
            return temp;
        }
    }

    public virtual int regenMOV
    {
        get
        {
            int temp = curMaxMOV + buffRegenMOV;
            if (CheckBuffExist(1005))
            {
                temp = 0;
            }
            if (temp > curMaxMOV)
            {
                temp = curMaxMOV;
            }
            return temp;
        }
    }

    public virtual int curMaxAP 
    {
        get
        {
            int temp = maxAP + buffMaxAP;
            if (temp < 0)
            {
                temp = 0;
            }
            return temp;
        }
    }

    public virtual int regenAP
    {
        get
        {
            int temp = curMaxAP + buffRegenAP;
            if (temp > curMaxAP)
            {
                temp = curMaxAP;
            }
            return temp; 
        }
    }


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

    public virtual int buffMaxMOV
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

    public virtual int buffRegenMOV
    {
        get
        {
            int temp = 0;
            return temp;
        }
    }


    public virtual int buffMaxAP
    {
        get
        {
            int temp = 0;
            if (CheckBuffExist(1002))
            {
                temp++;
            }
            if (CheckBuffExist(1005))
            {
                temp++;
            }
            return temp;
        }
    }

    public virtual int buffRegenAP
    {
        get
        {
            int temp = 0;
            return temp;
        }
    }

    public virtual int buffAddHurt
    {
        get
        {
            int temp = 0;
            if (CheckBuffExist(4002))
            {
                temp+=GetBuffLevel(4002);
            }
            return temp;
        }
    }

    public virtual int curCounter
    {
        get
        {
            int temp = buffCounter;
            return temp;
        }
    }

    public virtual int buffCounter
    {
        get
        {
            int temp = 0;
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
