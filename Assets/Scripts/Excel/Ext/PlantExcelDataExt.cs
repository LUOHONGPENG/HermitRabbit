using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlantExcelData
{
    public List<int> GetAllPlantID()
    {
        List<int> listTemp = new List<int>();
        for (int i = 0; i < items.Length; i++)
        {
            listTemp.Add(items[i].id);
        }
        return listTemp;
    }

}
