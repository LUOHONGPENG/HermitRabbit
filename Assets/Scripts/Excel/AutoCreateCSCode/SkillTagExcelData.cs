/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class SkillTagExcelItem : ExcelItemBase
{
	public SkillTag tag;
	public string name_EN;
	public string name_CN;
	public string desc_EN;
	public string desc_CN;
	public string iconUrl;
}

[CreateAssetMenu(fileName = "SkillTagExcelData", menuName = "Excel To ScriptableObject/Create SkillTagExcelData", order = 1)]
public partial class SkillTagExcelData : ExcelDataBase<SkillTagExcelItem>
{
}

#if UNITY_EDITOR
public class SkillTagAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		SkillTagExcelItem[] items = new SkillTagExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new SkillTagExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].tag = (SkillTag) Enum.Parse(typeof(SkillTag), allItemValueRowList[i]["tag"], true);
			items[i].name_EN = allItemValueRowList[i]["name_EN"];
			items[i].name_CN = allItemValueRowList[i]["name_CN"];
			items[i].desc_EN = allItemValueRowList[i]["desc_EN"];
			items[i].desc_CN = allItemValueRowList[i]["desc_CN"];
			items[i].iconUrl = allItemValueRowList[i]["iconUrl"];
		}
		SkillTagExcelData excelDataAsset = ScriptableObject.CreateInstance<SkillTagExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(SkillTagExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


