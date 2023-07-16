using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileBase : MonoBehaviour
{
    public SpriteRenderer spIndicator;
    public List<Color> listColorIndicator;

    public MapTileData mapTileData;
    public Vector2Int posID;

    public void Init(MapTileData mapTileData)
    {
        this.mapTileData = mapTileData;
        this.posID = mapTileData.posID;
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
            case MapIndicatorType.Red:
                ShowIndicator();
                spIndicator.color = listColorIndicator[2];
                break;
        }
    }

    #endregion
}
