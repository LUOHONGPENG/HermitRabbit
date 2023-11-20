using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MapClipExcelData
{
    public Dictionary<int, int> dicMapClipWeight = new Dictionary<int, int>();

    public List<int> GetAllMapClipID()
    {
        List<int> listTemp = new List<int>();
        for(int i = 0; i < items.Length; i++)
        {
            listTemp.Add(items[i].id);
        }
        return listTemp;
    }

    public void Init()
    {
        dicMapClipWeight.Clear();
        for(int i = 0; i < items.Length; i++)
        {
            MapClipExcelItem item = items[i];
            if (!dicMapClipWeight.ContainsKey(item.id))
            {
                dicMapClipWeight.Add(item.id, item.weight);
            }
        }
    }
}


public partial class MapClipExcelItem
{
    public List<MapTileType> listMapTile
    {
        get
        {
            List<MapTileType> listTemp = new List<MapTileType>();
            listTemp.Add(tile0);
            listTemp.Add(tile1);
            listTemp.Add(tile2);
            listTemp.Add(tile3);
            listTemp.Add(tile4);
            listTemp.Add(tile5);
            listTemp.Add(tile6);
            listTemp.Add(tile7);
            listTemp.Add(tile8);
            return listTemp;
        }
    }
}
