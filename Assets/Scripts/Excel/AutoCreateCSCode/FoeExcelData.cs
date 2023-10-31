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
	public int skillID;
	public FoeFindTargetType findTargetType;
	public FoeFocusType focusType;
	public FoeGenerateType generateType;
	public int pos0;
	public int pos1;
	public string pixelUrl;
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
			items[i].skillID = Convert.ToInt32(allItemValueRowList[i]["skillID"]);
			items[i].findTargetType = (FoeFindTargetType) Enum.Parse(typeof(FoeFindTargetType), allItemValueRowList[i]["findTargetType"], true);
			items[i].focusType = (FoeFocusType) Enum.Parse(typeof(FoeFocusType), allItemValueRowList[i]["focusType"], true);
			items[i].generateType = (FoeGenerateType) Enum.Parse(typeof(FoeGenerateType), allItemValueRowList[i]["generateType"], true);
			items[i].pos0 = Convert.ToInt32(allItemValueRowList[i]["pos0"]);
			items[i].pos1 = Convert.ToInt32(allItemValueRowList[i]["pos1"]);
			items[i].pixelUrl = allItemValueRowList[i]["pixelUrl"];
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


