/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class SkillDescExcelItem : ExcelItemBase
{
	public string name_EN;
	public string name_CN;
	public string desc_EN;
	public string desc_CN;
}

[CreateAssetMenu(fileName = "SkillDescExcelData", menuName = "Excel To ScriptableObject/Create SkillDescExcelData", order = 1)]
public partial class SkillDescExcelData : ExcelDataBase<SkillDescExcelItem>
{
}

#if UNITY_EDITOR
public class SkillDescAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		SkillDescExcelItem[] items = new SkillDescExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new SkillDescExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].name_EN = allItemValueRowList[i]["name_EN"];
			items[i].name_CN = allItemValueRowList[i]["name_CN"];
			items[i].desc_EN = allItemValueRowList[i]["desc_EN"];
			items[i].desc_CN = allItemValueRowList[i]["desc_CN"];
		}
		SkillDescExcelData excelDataAsset = ScriptableObject.CreateInstance<SkillDescExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(SkillDescExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


