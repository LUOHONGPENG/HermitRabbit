/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class MapTileExcelItem : ExcelItemBase
{
	public MapTileType tileType;
	public string name_EN;
	public string name_CN;
	public string desc_EN;
	public string desc_CN;
	public string iconUrl;
}

[CreateAssetMenu(fileName = "MapTileExcelData", menuName = "Excel To ScriptableObject/Create MapTileExcelData", order = 1)]
public partial class MapTileExcelData : ExcelDataBase<MapTileExcelItem>
{
}

#if UNITY_EDITOR
public class MapTileAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		MapTileExcelItem[] items = new MapTileExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new MapTileExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].tileType = (MapTileType) Enum.Parse(typeof(MapTileType), allItemValueRowList[i]["tileType"], true);
			items[i].name_EN = allItemValueRowList[i]["name_EN"];
			items[i].name_CN = allItemValueRowList[i]["name_CN"];
			items[i].desc_EN = allItemValueRowList[i]["desc_EN"];
			items[i].desc_CN = allItemValueRowList[i]["desc_CN"];
			items[i].iconUrl = allItemValueRowList[i]["iconUrl"];
		}
		MapTileExcelData excelDataAsset = ScriptableObject.CreateInstance<MapTileExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(MapTileExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


