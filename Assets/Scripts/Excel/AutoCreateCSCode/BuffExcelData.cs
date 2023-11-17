/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class BuffExcelItem : ExcelItemBase
{
	public string name;
	public SkillEffectType effectType;
	public int maxLevel;
	public BuffCounterType counterType;
	public BuffCountDownType countDownType;
	public bool canBeRemoved;
	public bool canBeDouble;
	public string desc;
	public int firstSpecialEffect;
	public int firstSpecialDelta;
	public string iconUrl;
}

[CreateAssetMenu(fileName = "BuffExcelData", menuName = "Excel To ScriptableObject/Create BuffExcelData", order = 1)]
public partial class BuffExcelData : ExcelDataBase<BuffExcelItem>
{
}

#if UNITY_EDITOR
public class BuffAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		BuffExcelItem[] items = new BuffExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new BuffExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].name = allItemValueRowList[i]["name"];
			items[i].effectType = (SkillEffectType) Enum.Parse(typeof(SkillEffectType), allItemValueRowList[i]["effectType"], true);
			items[i].maxLevel = Convert.ToInt32(allItemValueRowList[i]["maxLevel"]);
			items[i].counterType = (BuffCounterType) Enum.Parse(typeof(BuffCounterType), allItemValueRowList[i]["counterType"], true);
			items[i].countDownType = (BuffCountDownType) Enum.Parse(typeof(BuffCountDownType), allItemValueRowList[i]["countDownType"], true);
			items[i].canBeRemoved = Convert.ToBoolean(allItemValueRowList[i]["canBeRemoved"]);
			items[i].canBeDouble = Convert.ToBoolean(allItemValueRowList[i]["canBeDouble"]);
			items[i].desc = allItemValueRowList[i]["desc"];
			items[i].firstSpecialEffect = Convert.ToInt32(allItemValueRowList[i]["firstSpecialEffect"]);
			items[i].firstSpecialDelta = Convert.ToInt32(allItemValueRowList[i]["firstSpecialDelta"]);
			items[i].iconUrl = allItemValueRowList[i]["iconUrl"];
		}
		BuffExcelData excelDataAsset = ScriptableObject.CreateInstance<BuffExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(BuffExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


