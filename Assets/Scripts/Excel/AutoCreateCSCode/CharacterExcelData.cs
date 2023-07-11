/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class CharacterExcelItem : ExcelItemBase
{
	public string name;
	public int HP;
	public int STR;
	public int CON;
	public int INT;
	public int WIS;
	public int MOV;
	public int AP;
	public string pixelUrl;
	public List<int> startPos;
}

[CreateAssetMenu(fileName = "CharacterExcelData", menuName = "Excel To ScriptableObject/Create CharacterExcelData", order = 1)]
public partial class CharacterExcelData : ExcelDataBase<CharacterExcelItem>
{
}

#if UNITY_EDITOR
public class CharacterAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		CharacterExcelItem[] items = new CharacterExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new CharacterExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].name = allItemValueRowList[i]["name"];
			items[i].HP = Convert.ToInt32(allItemValueRowList[i]["HP"]);
			items[i].STR = Convert.ToInt32(allItemValueRowList[i]["STR"]);
			items[i].CON = Convert.ToInt32(allItemValueRowList[i]["CON"]);
			items[i].INT = Convert.ToInt32(allItemValueRowList[i]["INT"]);
			items[i].WIS = Convert.ToInt32(allItemValueRowList[i]["WIS"]);
			items[i].MOV = Convert.ToInt32(allItemValueRowList[i]["MOV"]);
			items[i].AP = Convert.ToInt32(allItemValueRowList[i]["AP"]);
			items[i].pixelUrl = allItemValueRowList[i]["pixelUrl"];
			items[i].startPos = new List<int>(Array.ConvertAll((allItemValueRowList[i]["startPos"]).Split(';'), int.Parse));
		}
		CharacterExcelData excelDataAsset = ScriptableObject.CreateInstance<CharacterExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(CharacterExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


