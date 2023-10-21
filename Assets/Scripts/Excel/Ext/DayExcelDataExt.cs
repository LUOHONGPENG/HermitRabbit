using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DayExcelData
{
    public Dictionary<int, List<Vector2Int>> dicDayFoe = new Dictionary<int, List<Vector2Int>>();

    public void Init()
    {
        dicDayFoe.Clear();

        for(int i = 0;i < items.Length; i++)
        {
            DayExcelItem thisItem = items[i];
            Vector2Int foeNumData = new Vector2Int(thisItem.foeID, thisItem.foeNum);
            if (dicDayFoe.ContainsKey(thisItem.dayCount))
            {
                List<Vector2Int> list = dicDayFoe[thisItem.dayCount];
                list.Add(foeNumData);
            }
            else
            {
                List<Vector2Int> list = new List<Vector2Int>();
                list.Add(foeNumData);
                dicDayFoe.Add(thisItem.dayCount, list);
            }
        }

    }

}
