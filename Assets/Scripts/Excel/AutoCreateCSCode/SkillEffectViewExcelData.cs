/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class SkillEffectViewExcelItem : ExcelItemBase
{
	public EffectViewType effectViewType;
	public EffectViewPosType effectViewPosType;
}

[CreateAssetMenu(fileName = "SkillEffectViewExcelData", menuName = "Excel To ScriptableObject/Create SkillEffectViewExcelData", order = 1)]
public partial class SkillEffectViewExcelData : ExcelDataBase<SkillEffectViewExcelItem>
{
}

#if UNITY_EDITOR
public class SkillEffectViewAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		SkillEffectViewExcelItem[] items = new SkillEffectViewExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new SkillEffectViewExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].effectViewType = (EffectViewType) Enum.Parse(typeof(EffectViewType), allItemValueRowList[i]["effectViewType"], true);
			items[i].effectViewPosType = (EffectViewPosType) Enum.Parse(typeof(EffectViewPosType), allItemValueRowList[i]["effectViewPosType"], true);
		}
		SkillEffectViewExcelData excelDataAsset = ScriptableObject.CreateInstance<SkillEffectViewExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(SkillEffectViewExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


