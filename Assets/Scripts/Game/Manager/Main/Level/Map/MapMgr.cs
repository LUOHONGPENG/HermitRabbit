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
    private Vector2Int hoverTileID = new Vector2Int(-99,-99);
    private bool isInit = false;

    public void Init(LevelMgr parent)
    {
        this.parent = parent;
        InitMapTileView();
        isInit = true;
    }

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("SetHoverTile", SetHoverTileEvent);
    }

    public void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("SetHoverTile", SetHoverTileEvent);
    }

    #region MapTile
    public void InitMapTileView()
    {
        listMapTile.Clear();
        dicMapTile.Clear();
        PublicTool.ClearChildItem(tfMapTile);
        GenerateTile(GameGlobal.mapSize, GameGlobal.mapSize);
    }

    public void GenerateTile(int sizeX, int sizeZ)
    {
        int centerX = (sizeX - 1) / 2;
        int centerZ = (sizeZ - 1) / 2;


        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeZ; j++)
            {
                Vector2Int posID = new Vector2Int(i, j);

                GameObject objMapTile = GameObject.Instantiate(pfMapTile, PublicTool.ConvertPosFromID(posID), Quaternion.identity, tfMapTile);
                MapTileBase itemMapTile = objMapTile.GetComponent<MapTileBase>();
                itemMapTile.Init(posID);
                listMapTile.Add(itemMapTile);
                dicMapTile.Add(new Vector2Int(i, j), objMapTile.GetComponent<MapTileBase>());
                objMapTile.name = string.Format("MapTile{0}_{1}", i, j);
            }
        }
    }

    #endregion

    #region HoverTile
    private void SetHoverTileEvent(object arg0)
    {
        hoverTileID = (Vector2Int)arg0;
    }
    #endregion


    public void TimeGo()
    {
        if (!isInit)
        {
            return;
        }

        UpdateMapUI();
/*        foreach(MapTileBase mapTile in listMapTile)
        {
            //Try to reduce call time
            if (GetTargetCrossRange(1).Contains(mapTile.posID))
            {
                mapTile.SetIndicator(MapIndicatorType.Red);
            }
            else
            {
                mapTile.SetIndicator(MapIndicatorType.Normal);
            }
        }*/
    }

    private void UpdateMapUI()
    {
        switch (parent.interactState)
        {
            case InteractState.Normal:
                SetMapUI_Normal();
                break;
            case InteractState.Move:
                SetMapUI_Move();
                break;
        }
    }

    private void SetMapUI_Normal()
    {
        foreach (MapTileBase mapTile in listMapTile)
        {
            mapTile.SetIndicator(MapIndicatorType.Hide);
        }
    }

    private void SetMapUI_Move()
    {
        BattleCharacterData characterData = (BattleCharacterData)parent.GetCurrentUnit(BattleUnitType.Character);
        Vector2Int posID = characterData.posID;

        foreach (MapTileBase mapTile in listMapTile)
        {
            if (GetTargetCrossRange(posID,characterData.curMOV).Contains(mapTile.posID))
            {
                mapTile.SetIndicator(MapIndicatorType.Normal);
            }
            else
            {
                mapTile.SetIndicator(MapIndicatorType.Hide);
            }
        }
    }

}
