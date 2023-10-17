using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileBase : MonoBehaviour
{
    public SpriteRenderer spIndicator;
    public List<Color> listColorIndicator;

    public MapTileData mapTileData;
    public Vector2Int posID
    {
        get
        {
            if(mapTileData != null)
            {
                return mapTileData.posID;
            }
            return Vector2Int.zero;
        }
    }

    public MapTileType tileType
    {
        get
        {
            if (mapTileData != null)
            {
                return mapTileData.tileType;
            }
            return MapTileType.Normal;
        }
    }

    public List<GameObject> listModel = new List<GameObject>();


    public void Init(MapTileData mapTileData)
    {
        this.mapTileData = mapTileData;

        RefreshMapTile();
    }

    public void TestRandomSetTileType()
    {
        mapTileData.RandomMapType();
        RefreshMapTile();
    }

    public void RefreshMapTile()
    {
        foreach(var model in listModel)
        {
            model.SetActive(false);
        }

        listModel[(int)tileType].SetActive(true);
    }


    #region Indicator
    
    public void ShowIndicator()
    {
        spIndicator.gameObject.SetActive(true);
    }

    public void HideIndicator()
    {
        spIndicator.gameObject.SetActive(false);
    }

    public void SetIndicator(MapIndicatorType type)
    {
        switch (type)
        {
            case MapIndicatorType.Hide:
                HideIndicator();
                break;
            case MapIndicatorType.Normal:
                ShowIndicator();
                spIndicator.color = listColorIndicator[0];
                break;
            case MapIndicatorType.Blue:
                ShowIndicator();
                spIndicator.color = listColorIndicator[1];
                break;
            case MapIndicatorType.AttackRadius:
                ShowIndicator();
                spIndicator.color = listColorIndicator[2];
                break;
            case MapIndicatorType.AttackCover:
                ShowIndicator();
                spIndicator.color = listColorIndicator[3];
                break;
        }
    }

    #endregion
}
