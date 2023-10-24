using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameData
{
    //FoeData
    public List<BattleFoeData> listFoe = new List<BattleFoeData>();
    public Dictionary<int, BattleFoeData> dicFoe = new Dictionary<int, BattleFoeData>();
    private int curFoeKeyID = -1;

    #region Basic-Foe
    public void NewGameFoeData()
    {
        listFoe.Clear();
        dicFoe.Clear();
        curFoeKeyID = -1;
    }

    public BattleFoeData GenerateFoeData(int typeID)
    {
        curFoeKeyID++;
        BattleFoeData foeData = new BattleFoeData(typeID, curFoeKeyID);
        listFoe.Add(foeData);
        dicFoe.Add(curFoeKeyID, foeData);
        return foeData;
    }

    public BattleFoeData GetBattleFoeData(int ID)
    {
        if (dicFoe.ContainsKey(ID))
        {
            return dicFoe[ID];
        }
        else
        {
            return null;
        }
    }


    public void RemoveFoeData(int keyID)
    {
        if (dicFoe.ContainsKey(keyID))
        {
            BattleFoeData foeData = dicFoe[keyID];
            listFoe.Remove(foeData);
            dicFoe.Remove(keyID);
        }
    }
    #endregion

}
