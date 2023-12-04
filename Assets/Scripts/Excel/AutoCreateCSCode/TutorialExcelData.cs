/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class TutorialExcelItem : ExcelItemBase
{
	public TutorialGroup group;
	public string strTitle;
	public string strContent;
	public string strContent_cn;
	public string gifUrl;
}

[CreateAssetMenu(fileName = "TutorialExcelData", menuName = "Excel To ScriptableObject/Create TutorialExcelData", order = 1)]
public partial class TutorialExcelData : ExcelDataBase<TutorialExcelItem>
{
}

#if UNITY_EDITOR
public class TutorialAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		TutorialExcelItem[] items = new TutorialExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new TutorialExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].group = (TutorialGroup) Enum.Parse(typeof(TutorialGroup), allItemValueRowList[i]["group"], true);
			items[i].strTitle = allItemValueRowList[i]["strTitle"];
			items[i].strContent = allItemValueRowList[i]["strContent"];
			items[i].strContent_cn = allItemValueRowList[i]["strContent_cn"];
			items[i].gifUrl = allItemValueRowList[i]["gifUrl"];
		}
		TutorialExcelData excelDataAsset = ScriptableObject.CreateInstance<TutorialExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(TutorialExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


