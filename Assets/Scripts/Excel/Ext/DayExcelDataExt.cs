using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DayExcelData
{
    public Dictionary<int, List<Vector3Int>> dicDayFoe = new Dictionary<int, List<Vector3Int>>();
    public Dictionary<int, int> dicDayExp = new Dictionary<int, int>();

    public void Init()
    {
        dicDayFoe.Clear();
        dicDayExp.Clear();

        for (int i = 0;i < items.Length; i++)
        {
            DayExcelItem thisItem = items[i];
            //FoeNumData
            Vector3Int foeNumData = new Vector3Int(thisItem.foeID, thisItem.foeNum,thisItem.foeExp);
            if (dicDayFoe.ContainsKey(thisItem.dayCount))
            {
                List<Vector3Int> list = dicDayFoe[thisItem.dayCount];
                list.Add(foeNumData);
            }
            else
            {
                List<Vector3Int> list = new List<Vector3Int>();
                list.Add(foeNumData);
                dicDayFoe.Add(thisItem.dayCount, list);
            }
            //Exp
            if (dicDayExp.ContainsKey(thisItem.dayCount))
            {
                dicDayExp[thisItem.dayCount] += thisItem.dayExp;
            }
            else
            {
                dicDayExp.Add(thisItem.dayCount, thisItem.dayExp);
            }
        }

    }

}
