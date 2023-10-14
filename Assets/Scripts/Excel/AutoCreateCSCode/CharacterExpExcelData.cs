/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class CharacterExpExcelItem : ExcelItemBase
{
	public int Level;
	public int EXP;
	public int SP;
	public int HP1;
	public int HP2;
}

[CreateAssetMenu(fileName = "CharacterExpExcelData", menuName = "Excel To ScriptableObject/Create CharacterExpExcelData", order = 1)]
public partial class CharacterExpExcelData : ExcelDataBase<CharacterExpExcelItem>
{
}

#if UNITY_EDITOR
public class CharacterExpAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		CharacterExpExcelItem[] items = new CharacterExpExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new CharacterExpExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].Level = Convert.ToInt32(allItemValueRowList[i]["Level"]);
			items[i].EXP = Convert.ToInt32(allItemValueRowList[i]["EXP"]);
			items[i].SP = Convert.ToInt32(allItemValueRowList[i]["SP"]);
			items[i].HP1 = Convert.ToInt32(allItemValueRowList[i]["HP1"]);
			items[i].HP2 = Convert.ToInt32(allItemValueRowList[i]["HP2"]);
		}
		CharacterExpExcelData excelDataAsset = ScriptableObject.CreateInstance<CharacterExpExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(CharacterExpExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


