/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class FoeExcelItem : ExcelItemBase
{
	public string name;
	public int HP;
	public int ATK;
	public int DEF;
	public int RES;
	public int MOV;
}

[CreateAssetMenu(fileName = "FoeExcelData", menuName = "Excel To ScriptableObject/Create FoeExcelData", order = 1)]
public partial class FoeExcelData : ExcelDataBase<FoeExcelItem>
{
}

#if UNITY_EDITOR
public class FoeAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		FoeExcelItem[] items = new FoeExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new FoeExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].name = allItemValueRowList[i]["name"];
			items[i].HP = Convert.ToInt32(allItemValueRowList[i]["HP"]);
			items[i].ATK = Convert.ToInt32(allItemValueRowList[i]["ATK"]);
			items[i].DEF = Convert.ToInt32(allItemValueRowList[i]["DEF"]);
			items[i].RES = Convert.ToInt32(allItemValueRowList[i]["RES"]);
			items[i].MOV = Convert.ToInt32(allItemValueRowList[i]["MOV"]);
		}
		FoeExcelData excelDataAsset = ScriptableObject.CreateInstance<FoeExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(FoeExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif

