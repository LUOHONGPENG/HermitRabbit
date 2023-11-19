using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileBase : MonoBehaviour
{
    [Header("Indicator")]
    public SpriteRenderer spIndicator;
    public SpriteRenderer spPlantRange;
    public List<Color> listColorIndicator;

    [Header("Display")]
    public List<GameObject> listModel = new List<GameObject>();
    public GameObject objBurning;

    public void Init(MapTileData mapTileData)
    {
        this.mapTileData = mapTileData;

        RefreshMapTile();
    }

    public void RefreshMapTile()
    {
        foreach(var model in listModel)
        {
            model.SetActive(false);
        }

        switch (tileType)
        {
            case MapTileType.Normal:
                if(tileStatus == MapTileStatus.Wet)
                {
                    listModel[(int)MapTileType.Water].SetActive(true);
                }
                else
                {
                    listModel[(int)MapTileType.Normal].SetActive(true);
                }
                break;
            case MapTileType.Stone:
                if (tileStatus == MapTileStatus.Broken)
                {
                    listModel[(int)MapTileType.End].SetActive(true);
                }
                else
                {
                    listModel[(int)MapTileType.Stone].SetActive(true);
                }
                break;
            default:
                listModel[(int)tileType].SetActive(true);
                break;
        }


        //Burning
        if (tileStatus == MapTileStatus.Burning)
        {
            objBurning.SetActive(true);
        }
        else
        {
            objBurning.SetActive(false);
        }
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

    public void SetPlantRangeIndicator(bool isPlantRange)
    {
        if (isPlantRange)
        {
            spPlantRange.gameObject.SetActive(true);
        }
        else
        {
            spPlantRange.gameObject.SetActive(false);
        }
    }

    public void ResetRangeIndicator()
    {
        spPlantRange.gameObject.SetActive(false);

    }

    #endregion

    #region MaptileData
    [HideInInspector]
    public MapTileData mapTileData;

    public Vector2Int posID
    {
        get
        {
            if (mapTileData != null)
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
                return mapTileData.GetMapType();
            }
            return MapTileType.Normal;
        }
    }

    public MapTileStatus tileStatus
    {
        get
        {
            if (mapTileData != null)
            {
                return mapTileData.curMapTileStatus;
            }
            return MapTileStatus.None;
        }
    }

    #endregion
}
