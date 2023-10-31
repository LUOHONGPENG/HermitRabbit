/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class SoundExcelItem : ExcelItemBase
{
	public SoundType name;
	public string soundUrl;
}

[CreateAssetMenu(fileName = "SoundExcelData", menuName = "Excel To ScriptableObject/Create SoundExcelData", order = 1)]
public partial class SoundExcelData : ExcelDataBase<SoundExcelItem>
{
}

#if UNITY_EDITOR
public class SoundAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		SoundExcelItem[] items = new SoundExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new SoundExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].name = (SoundType) Enum.Parse(typeof(SoundType), allItemValueRowList[i]["name"], true);
			items[i].soundUrl = allItemValueRowList[i]["soundUrl"];
		}
		SoundExcelData excelDataAsset = ScriptableObject.CreateInstance<SoundExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(SoundExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


