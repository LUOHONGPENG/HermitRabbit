/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class SkillSpecialExcelItem : ExcelItemBase
{
	public string name;
	public SkillEffectType effectType;
}

[CreateAssetMenu(fileName = "SkillSpecialExcelData", menuName = "Excel To ScriptableObject/Create SkillSpecialExcelData", order = 1)]
public partial class SkillSpecialExcelData : ExcelDataBase<SkillSpecialExcelItem>
{
}

#if UNITY_EDITOR
public class SkillSpecialAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		SkillSpecialExcelItem[] items = new SkillSpecialExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new SkillSpecialExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].name = allItemValueRowList[i]["name"];
			items[i].effectType = (SkillEffectType) Enum.Parse(typeof(SkillEffectType), allItemValueRowList[i]["effectType"], true);
		}
		SkillSpecialExcelData excelDataAsset = ScriptableObject.CreateInstance<SkillSpecialExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(SkillSpecialExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


