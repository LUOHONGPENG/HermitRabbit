using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileBase : MonoBehaviour
{
    public SpriteRenderer spIndicator;
    public List<Color> listColorIndicator;

    public Vector2Int posID;

    public void Init(Vector2Int posID)
    {
        this.posID = posID;
    }

    public enum MapIndicatorType
    {
        Normal,
        Red
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
            case MapIndicatorType.Normal:
                spIndicator.color = listColorIndicator[0];
                break;
            case MapIndicatorType.Red:
                spIndicator.color = listColorIndicator[1];
                break;
        }
    }

    #endregion
}
