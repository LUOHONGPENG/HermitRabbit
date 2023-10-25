/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class SkillExcelItem : ExcelItemBase
{
	public string name;
	public int costAP;
	public int costMOV;
	public int costHP;
	public int range;
	public bool isRangeSelf;
	public bool needExtraTarget;
	public int radius;
	public SkillRegionType regionType;
	public SkillEffectType foeEffect;
	public SkillEffectType characterEffect;
	public SkillEffectType plantEffect;
	public SkillDamageType damageType;
	public SkillDamageDeltaStd damageDeltaStd;
	public int damageDelta;
	public int damageModifier;
	public List<int> listBuffEffect;
	public List<int> listBuffDelta;
	public List<int> listSpecialEffect;
	public List<int> listSpecialDelta;
	public ActiveSkillType activeSkillType;
	public int characterID;
	public string iconUrl;
	public int unlockNodeID;
	public string desc;
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
			items[i].costAP = Convert.ToInt32(allItemValueRowList[i]["costAP"]);
			items[i].costMOV = Convert.ToInt32(allItemValueRowList[i]["costMOV"]);
			items[i].costHP = Convert.ToInt32(allItemValueRowList[i]["costHP"]);
			items[i].range = Convert.ToInt32(allItemValueRowList[i]["range"]);
			items[i].isRangeSelf = Convert.ToBoolean(allItemValueRowList[i]["isRangeSelf"]);
			items[i].needExtraTarget = Convert.ToBoolean(allItemValueRowList[i]["needExtraTarget"]);
			items[i].radius = Convert.ToInt32(allItemValueRowList[i]["radius"]);
			items[i].regionType = (SkillRegionType) Enum.Parse(typeof(SkillRegionType), allItemValueRowList[i]["regionType"], true);
			items[i].foeEffect = (SkillEffectType) Enum.Parse(typeof(SkillEffectType), allItemValueRowList[i]["foeEffect"], true);
			items[i].characterEffect = (SkillEffectType) Enum.Parse(typeof(SkillEffectType), allItemValueRowList[i]["characterEffect"], true);
			items[i].plantEffect = (SkillEffectType) Enum.Parse(typeof(SkillEffectType), allItemValueRowList[i]["plantEffect"], true);
			items[i].damageType = (SkillDamageType) Enum.Parse(typeof(SkillDamageType), allItemValueRowList[i]["damageType"], true);
			items[i].damageDeltaStd = (SkillDamageDeltaStd) Enum.Parse(typeof(SkillDamageDeltaStd), allItemValueRowList[i]["damageDeltaStd"], true);
			items[i].damageDelta = Convert.ToInt32(allItemValueRowList[i]["damageDelta"]);
			items[i].damageModifier = Convert.ToInt32(allItemValueRowList[i]["damageModifier"]);
			items[i].listBuffEffect = new List<int>(Array.ConvertAll((allItemValueRowList[i]["listBuffEffect"]).Split(';'), int.Parse));
			items[i].listBuffDelta = new List<int>(Array.ConvertAll((allItemValueRowList[i]["listBuffDelta"]).Split(';'), int.Parse));
			items[i].listSpecialEffect = new List<int>(Array.ConvertAll((allItemValueRowList[i]["listSpecialEffect"]).Split(';'), int.Parse));
			items[i].listSpecialDelta = new List<int>(Array.ConvertAll((allItemValueRowList[i]["listSpecialDelta"]).Split(';'), int.Parse));
			items[i].activeSkillType = (ActiveSkillType) Enum.Parse(typeof(ActiveSkillType), allItemValueRowList[i]["activeSkillType"], true);
			items[i].characterID = Convert.ToInt32(allItemValueRowList[i]["characterID"]);
			items[i].iconUrl = allItemValueRowList[i]["iconUrl"];
			items[i].unlockNodeID = Convert.ToInt32(allItemValueRowList[i]["unlockNodeID"]);
			items[i].desc = allItemValueRowList[i]["desc"];
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


