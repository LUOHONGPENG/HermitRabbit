/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class MapClipExcelItem : ExcelItemBase
{
	public Rarity rarity;
	public int weight;
	public MapTileType tile0;
	public MapTileType tile1;
	public MapTileType tile2;
	public MapTileType tile3;
	public MapTileType tile4;
	public MapTileType tile5;
	public MapTileType tile6;
	public MapTileType tile7;
	public MapTileType tile8;
	public float waterValue;
	public float grassValue;
	public float blockValue;
	public int remark;
}

[CreateAssetMenu(fileName = "MapClipExcelData", menuName = "Excel To ScriptableObject/Create MapClipExcelData", order = 1)]
public partial class MapClipExcelData : ExcelDataBase<MapClipExcelItem>
{
}

#if UNITY_EDITOR
public class MapClipAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		MapClipExcelItem[] items = new MapClipExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new MapClipExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].rarity = (Rarity) Enum.Parse(typeof(Rarity), allItemValueRowList[i]["rarity"], true);
			items[i].weight = Convert.ToInt32(allItemValueRowList[i]["weight"]);
			items[i].tile0 = (MapTileType) Enum.Parse(typeof(MapTileType), allItemValueRowList[i]["tile0"], true);
			items[i].tile1 = (MapTileType) Enum.Parse(typeof(MapTileType), allItemValueRowList[i]["tile1"], true);
			items[i].tile2 = (MapTileType) Enum.Parse(typeof(MapTileType), allItemValueRowList[i]["tile2"], true);
			items[i].tile3 = (MapTileType) Enum.Parse(typeof(MapTileType), allItemValueRowList[i]["tile3"], true);
			items[i].tile4 = (MapTileType) Enum.Parse(typeof(MapTileType), allItemValueRowList[i]["tile4"], true);
			items[i].tile5 = (MapTileType) Enum.Parse(typeof(MapTileType), allItemValueRowList[i]["tile5"], true);
			items[i].tile6 = (MapTileType) Enum.Parse(typeof(MapTileType), allItemValueRowList[i]["tile6"], true);
			items[i].tile7 = (MapTileType) Enum.Parse(typeof(MapTileType), allItemValueRowList[i]["tile7"], true);
			items[i].tile8 = (MapTileType) Enum.Parse(typeof(MapTileType), allItemValueRowList[i]["tile8"], true);
			items[i].waterValue = Convert.ToSingle(allItemValueRowList[i]["waterValue"]);
			items[i].grassValue = Convert.ToSingle(allItemValueRowList[i]["grassValue"]);
			items[i].blockValue = Convert.ToSingle(allItemValueRowList[i]["blockValue"]);
			items[i].remark = Convert.ToInt32(allItemValueRowList[i]["remark"]);
		}
		MapClipExcelData excelDataAsset = ScriptableObject.CreateInstance<MapClipExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(MapClipExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


