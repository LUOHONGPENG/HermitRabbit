/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class CharacterSkillExcelItem : ExcelItemBase
{
	public string name;
	public string desc;
	public int costSP;
	public SkillElementType element;
	public int range;
	public int radius;
	public SkillRegionType regionType;
	public bool isTargetFoe;
}

[CreateAssetMenu(fileName = "CharacterSkillExcelData", menuName = "Excel To ScriptableObject/Create CharacterSkillExcelData", order = 1)]
public partial class CharacterSkillExcelData : ExcelDataBase<CharacterSkillExcelItem>
{
}

#if UNITY_EDITOR
public class CharacterSkillAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		CharacterSkillExcelItem[] items = new CharacterSkillExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new CharacterSkillExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].name = allItemValueRowList[i]["name"];
			items[i].desc = allItemValueRowList[i]["desc"];
			items[i].costSP = Convert.ToInt32(allItemValueRowList[i]["costSP"]);
			items[i].element = (SkillElementType) Enum.Parse(typeof(SkillElementType), allItemValueRowList[i]["element"], true);
			items[i].range = Convert.ToInt32(allItemValueRowList[i]["range"]);
			items[i].radius = Convert.ToInt32(allItemValueRowList[i]["radius"]);
			items[i].regionType = (SkillRegionType) Enum.Parse(typeof(SkillRegionType), allItemValueRowList[i]["regionType"], true);
			items[i].isTargetFoe = Convert.ToBoolean(allItemValueRowList[i]["isTargetFoe"]);
		}
		CharacterSkillExcelData excelDataAsset = ScriptableObject.CreateInstance<CharacterSkillExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(CharacterSkillExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


