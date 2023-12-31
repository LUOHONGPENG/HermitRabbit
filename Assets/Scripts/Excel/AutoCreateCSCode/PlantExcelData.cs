/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class PlantExcelItem : ExcelItemBase
{
	public string name_EN;
	public string name_CN;
	public Rarity rarity;
	public int HP;
	public int ATK;
	public int DEF;
	public int RES;
	public PlantTriggerType triggerCondition;
	public PlantFindTargetType findTargetType;
	public int findTargetDelta;
	public int skillID;
	public int essence;
	public string pixelUrl;
	public string desc_EN;
	public string desc_CN;
}

[CreateAssetMenu(fileName = "PlantExcelData", menuName = "Excel To ScriptableObject/Create PlantExcelData", order = 1)]
public partial class PlantExcelData : ExcelDataBase<PlantExcelItem>
{
}

#if UNITY_EDITOR
public class PlantAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		PlantExcelItem[] items = new PlantExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new PlantExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].name_EN = allItemValueRowList[i]["name_EN"];
			items[i].name_CN = allItemValueRowList[i]["name_CN"];
			items[i].rarity = (Rarity) Enum.Parse(typeof(Rarity), allItemValueRowList[i]["rarity"], true);
			items[i].HP = Convert.ToInt32(allItemValueRowList[i]["HP"]);
			items[i].ATK = Convert.ToInt32(allItemValueRowList[i]["ATK"]);
			items[i].DEF = Convert.ToInt32(allItemValueRowList[i]["DEF"]);
			items[i].RES = Convert.ToInt32(allItemValueRowList[i]["RES"]);
			items[i].triggerCondition = (PlantTriggerType) Enum.Parse(typeof(PlantTriggerType), allItemValueRowList[i]["triggerCondition"], true);
			items[i].findTargetType = (PlantFindTargetType) Enum.Parse(typeof(PlantFindTargetType), allItemValueRowList[i]["findTargetType"], true);
			items[i].findTargetDelta = Convert.ToInt32(allItemValueRowList[i]["findTargetDelta"]);
			items[i].skillID = Convert.ToInt32(allItemValueRowList[i]["skillID"]);
			items[i].essence = Convert.ToInt32(allItemValueRowList[i]["essence"]);
			items[i].pixelUrl = allItemValueRowList[i]["pixelUrl"];
			items[i].desc_EN = allItemValueRowList[i]["desc_EN"];
			items[i].desc_CN = allItemValueRowList[i]["desc_CN"];
		}
		PlantExcelData excelDataAsset = ScriptableObject.CreateInstance<PlantExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(PlantExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


