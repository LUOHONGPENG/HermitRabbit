/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class DayExcelItem : ExcelItemBase
{
	public int dayCount;
	public int foeID;
	public int foeNum;
	public int dayExp;
	public int foeExp;
	public int totalFoeExp;
	public string Remark;
}

[CreateAssetMenu(fileName = "DayExcelData", menuName = "Excel To ScriptableObject/Create DayExcelData", order = 1)]
public partial class DayExcelData : ExcelDataBase<DayExcelItem>
{
}

#if UNITY_EDITOR
public class DayAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		DayExcelItem[] items = new DayExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new DayExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].dayCount = Convert.ToInt32(allItemValueRowList[i]["dayCount"]);
			items[i].foeID = Convert.ToInt32(allItemValueRowList[i]["foeID"]);
			items[i].foeNum = Convert.ToInt32(allItemValueRowList[i]["foeNum"]);
			items[i].dayExp = Convert.ToInt32(allItemValueRowList[i]["dayExp"]);
			items[i].foeExp = Convert.ToInt32(allItemValueRowList[i]["foeExp"]);
			items[i].totalFoeExp = Convert.ToInt32(allItemValueRowList[i]["totalFoeExp"]);
			items[i].Remark = allItemValueRowList[i]["Remark"];
		}
		DayExcelData excelDataAsset = ScriptableObject.CreateInstance<DayExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(DayExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


