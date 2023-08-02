/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class SkillExcelItem : ExcelItemBase
{
	public string name;
	public string desc;
	public int costAP;
	public SkillElementType element;
	public int range;
	public bool isRangeSelf;
	public bool needExtraTarget;
	public int radius;
	public SkillRegionType regionType;
	public SkillEffectType foeEffect;
	public SkillEffectType characterEffect;
	public SkillEffectType plantEffect;
	public SkillDamageType damageType;
	public int damageDelta;
	public SkillDamageDeltaStd damageDeltaStd;
	public List<int> listSpecialEffect;
	public BattleUnitType skillSubjectType;
	public bool isNormalAttack;
	public int characterID;
}

[CreateAssetMenu(fileName = "SkillExcelData", menuName = "Excel To ScriptableObject/Create SkillExcelData", order = 1)]
public partial class SkillExcelData : ExcelDataBase<SkillExcelItem>
{
}

#if UNITY_EDITOR
public class SkillAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		SkillExcelItem[] items = new SkillExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new SkillExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].name = allItemValueRowList[i]["name"];
			items[i].desc = allItemValueRowList[i]["desc"];
			items[i].costAP = Convert.ToInt32(allItemValueRowList[i]["costAP"]);
			items[i].element = (SkillElementType) Enum.Parse(typeof(SkillElementType), allItemValueRowList[i]["element"], true);
			items[i].range = Convert.ToInt32(allItemValueRowList[i]["range"]);
			items[i].isRangeSelf = Convert.ToBoolean(allItemValueRowList[i]["isRangeSelf"]);
			items[i].needExtraTarget = Convert.ToBoolean(allItemValueRowList[i]["needExtraTarget"]);
			items[i].radius = Convert.ToInt32(allItemValueRowList[i]["radius"]);
			items[i].regionType = (SkillRegionType) Enum.Parse(typeof(SkillRegionType), allItemValueRowList[i]["regionType"], true);
			items[i].foeEffect = (SkillEffectType) Enum.Parse(typeof(SkillEffectType), allItemValueRowList[i]["foeEffect"], true);
			items[i].characterEffect = (SkillEffectType) Enum.Parse(typeof(SkillEffectType), allItemValueRowList[i]["characterEffect"], true);
			items[i].plantEffect = (SkillEffectType) Enum.Parse(typeof(SkillEffectType), allItemValueRowList[i]["plantEffect"], true);
			items[i].damageType = (SkillDamageType) Enum.Parse(typeof(SkillDamageType), allItemValueRowList[i]["damageType"], true);
			items[i].damageDelta = Convert.ToInt32(allItemValueRowList[i]["damageDelta"]);
			items[i].damageDeltaStd = (SkillDamageDeltaStd) Enum.Parse(typeof(SkillDamageDeltaStd), allItemValueRowList[i]["damageDeltaStd"], true);
			items[i].listSpecialEffect = new List<int>(Array.ConvertAll((allItemValueRowList[i]["listSpecialEffect"]).Split(';'), int.Parse));
			items[i].skillSubjectType = (BattleUnitType) Enum.Parse(typeof(BattleUnitType), allItemValueRowList[i]["skillSubjectType"], true);
			items[i].isNormalAttack = Convert.ToBoolean(allItemValueRowList[i]["isNormalAttack"]);
			items[i].characterID = Convert.ToInt32(allItemValueRowList[i]["characterID"]);
		}
		SkillExcelData excelDataAsset = ScriptableObject.CreateInstance<SkillExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(SkillExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


