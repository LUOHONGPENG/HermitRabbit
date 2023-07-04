using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MapMgr : MonoBehaviour
{
    [Header("MapTile")]
    public Transform tfMapTile;
    public GameObject pfMapTile;
    public List<MapTileBase> listMapTile = new List<MapTileBase>();
    public Dictionary<Vector2Int, MapTileBase> dicMapTile = new Dictionary<Vector2Int, MapTileBase>();

    private LevelMgr parent;
    private Vector2Int targetTileID = new Vector2Int(-99,-99);
    private bool isInit = false;

    public void Init(LevelMgr parent)
    {
        this.parent = parent;
        InitMapTileView();
        isInit = true;
    }

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("SetTargetTile", SetTargetTileEvent);
    }

    public void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("SetTargetTile", SetTargetTileEvent);
    }

    #region MapTile
    public void InitMapTileView()
    {
        listMapTile.Clear();
        dicMapTile.Clear();
        PublicTool.ClearChildItem(tfMapTile);
        GenerateTile(5, 5);
    }

    public void GenerateTile(int sizeX, int sizeZ)
    {
        int centerX = (sizeX - 1) / 2;
        int centerZ = (sizeZ - 1) / 2;

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeZ; j++)
            {
                GameObject objMapTile = GameObject.Instantiate(pfMapTile, new Vector3(i - centerX, 0, j - centerZ), Quaternion.identity, tfMapTile);
                MapTileBase itemMapTile = objMapTile.GetComponent<MapTileBase>();
                itemMapTile.Init(new Vector2Int(i, j));
                listMapTile.Add(itemMapTile);
                dicMapTile.Add(new Vector2Int(i, j), objMapTile.GetComponent<MapTileBase>());
                objMapTile.name = string.Format("MapTile{0}_{1}", i, j);
            }
        }
    }

    #endregion

    #region TargetTile
    private void SetTargetTileEvent(object arg0)
    {
        targetTileID = (Vector2Int)arg0;
    }
    #endregion


    public void TimeGo()
    {
        foreach(MapTileBase mapTile in listMapTile)
        {
            //Try to reduce call time
            if (GetTargetCrossRange(1).Contains(mapTile.posID))
            {
                mapTile.SetIndicator(MapTileBase.MapIndicatorType.Red);
            }
            else
            {
                mapTile.SetIndicator(MapTileBase.MapIndicatorType.Normal);
            }
        }
    }
}
