/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class SkillPerformExcelItem : ExcelItemBase
{
	public string name;
	public float totalTime;
	public List<string> listUnitAniState;
	public List<int> listUnitAniTime;
	public List<string> listEffectViewType;
	public List<string> listEffectPosType;
	public List<int> listEffectViewTime;
	public List<string> listSoundType;
	public List<int> listSoundTime;
}

[CreateAssetMenu(fileName = "SkillPerformExcelData", menuName = "Excel To ScriptableObject/Create SkillPerformExcelData", order = 1)]
public partial class SkillPerformExcelData : ExcelDataBase<SkillPerformExcelItem>
{
}

#if UNITY_EDITOR
public class SkillPerformAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		SkillPerformExcelItem[] items = new SkillPerformExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new SkillPerformExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].name = allItemValueRowList[i]["name"];
			items[i].totalTime = Convert.ToSingle(allItemValueRowList[i]["totalTime"]);
			items[i].listUnitAniState = new List<string>(allItemValueRowList[i]["listUnitAniState"].Split(';'));
			items[i].listUnitAniTime = new List<int>(Array.ConvertAll((allItemValueRowList[i]["listUnitAniTime"]).Split(';'), int.Parse));
			items[i].listEffectViewType = new List<string>(allItemValueRowList[i]["listEffectViewType"].Split(';'));
			items[i].listEffectPosType = new List<string>(allItemValueRowList[i]["listEffectPosType"].Split(';'));
			items[i].listEffectViewTime = new List<int>(Array.ConvertAll((allItemValueRowList[i]["listEffectViewTime"]).Split(';'), int.Parse));
			items[i].listSoundType = new List<string>(allItemValueRowList[i]["listSoundType"].Split(';'));
			items[i].listSoundTime = new List<int>(Array.ConvertAll((allItemValueRowList[i]["listSoundTime"]).Split(';'), int.Parse));
		}
		SkillPerformExcelData excelDataAsset = ScriptableObject.CreateInstance<SkillPerformExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(SkillPerformExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


