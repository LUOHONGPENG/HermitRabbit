/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class TalkExcelItem : ExcelItemBase
{
	public string name;
}

[CreateAssetMenu(fileName = "TalkExcelData", menuName = "Excel To ScriptableObject/Create TalkExcelData", order = 1)]
public partial class TalkExcelData : ExcelDataBase<TalkExcelItem>
{
}

#if UNITY_EDITOR
public class TalkAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		TalkExcelItem[] items = new TalkExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new TalkExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].name = allItemValueRowList[i]["name"];
		}
		TalkExcelData excelDataAsset = ScriptableObject.CreateInstance<TalkExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(TalkExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


