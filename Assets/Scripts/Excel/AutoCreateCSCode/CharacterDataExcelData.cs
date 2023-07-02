/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class CharacterDataExcelItem : ExcelItemBase
{
	public int HP;
	public int STR;
	public int CON;
	public int INT;
	public int WIS;
	public int MOV;
	public int AP;
}

[CreateAssetMenu(fileName = "CharacterDataExcelData", menuName = "Excel To ScriptableObject/Create CharacterDataExcelData", order = 1)]
public partial class CharacterDataExcelData : ExcelDataBase<CharacterDataExcelItem>
{
}

#if UNITY_EDITOR
public class CharacterDataAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		CharacterDataExcelItem[] items = new CharacterDataExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new CharacterDataExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].HP = Convert.ToInt32(allItemValueRowList[i]["HP"]);
			items[i].STR = Convert.ToInt32(allItemValueRowList[i]["STR"]);
			items[i].CON = Convert.ToInt32(allItemValueRowList[i]["CON"]);
			items[i].INT = Convert.ToInt32(allItemValueRowList[i]["INT"]);
			items[i].WIS = Convert.ToInt32(allItemValueRowList[i]["WIS"]);
			items[i].MOV = Convert.ToInt32(allItemValueRowList[i]["MOV"]);
			items[i].AP = Convert.ToInt32(allItemValueRowList[i]["AP"]);
		}
		CharacterDataExcelData excelDataAsset = ScriptableObject.CreateInstance<CharacterDataExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(CharacterDataExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


