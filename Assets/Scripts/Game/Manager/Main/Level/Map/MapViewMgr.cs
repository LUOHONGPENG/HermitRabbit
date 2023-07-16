using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MapViewMgr : MonoBehaviour
{
    [Header("MapTile")]
    public Transform tfMapTile;
    public GameObject pfMapTile;
    public List<MapTileBase> listMapTile = new List<MapTileBase>();
    public Dictionary<Vector2Int, MapTileBase> dicMapTile = new Dictionary<Vector2Int, MapTileBase>();

    private bool isInit = false;
    private BattleUnitData curUnitData;

    public void Init()
    {
        InitMapTileView();
        isInit = true;
    }


    #region Generate MapTile
    public void InitMapTileView()
    {
        listMapTile.Clear();
        dicMapTile.Clear();
        PublicTool.ClearChildItem(tfMapTile);
        //GenerateTile(GameGlobal.mapSize, GameGlobal.mapSize);
        foreach(var mapTileData in PublicTool.GetGameData().listMapTile)
        {
            GenerateTile(mapTileData); 
        }
    }

    public void GenerateTile(MapTileData mapTileData)
    {
        Vector2Int posID = mapTileData.posID;
        GameObject objMapTile = GameObject.Instantiate(pfMapTile, PublicTool.ConvertPosFromID(posID), Quaternion.identity, tfMapTile);
        MapTileBase itemMapTile = objMapTile.GetComponent<MapTileBase>();
        itemMapTile.Init(mapTileData);
        listMapTile.Add(itemMapTile);
        dicMapTile.Add(posID, objMapTile.GetComponent<MapTileBase>());
        objMapTile.name = string.Format("MapTile{0}_{1}", posID.x, posID.y);
    }
    #endregion

    #region Display the MapTile state
    public void TimeGo()
    {
        if (!isInit)
        {
            return;
        }

        UpdateMapUI();
    }

    public void RefreshCurUnit()
    {
        curUnitData = PublicTool.GetGameData().GetCurUnitData();
    }

    private void UpdateMapUI()
    {
        InteractState state = InputMgr.Instance.interactState;

        //I think this need a optimisation
        switch (state)
        {
            case InteractState.Normal:
                SetMapUI_Normal();
                break;
            case InteractState.Move:
                SetMapUI_Move();
                break;
            case InteractState.Skill:
                SetMapUI_Skill();
                break;
        }
    }

    private void SetMapUI_Normal()
    {
        ResetAllTile();
    }

    private void SetMapUI_Move()
    {
        //Go through
        foreach (MapTileBase mapTile in listMapTile)
        {
            if (curUnitData.listValidMove.Contains(mapTile.posID))
            {
                if (PublicTool.GetTargetCircleRange(PublicTool.GetGameData().hoverTileID, 0).Contains(mapTile.posID))
                {
                    mapTile.SetIndicator(MapIndicatorType.Blue);
                }
                else
                {
                    mapTile.SetIndicator(MapIndicatorType.Normal);
                }
            }
            else
            {
                mapTile.SetIndicator(MapIndicatorType.Hide);
            }
        }
    }

    private void SetMapUI_Skill()
    {
        Vector2Int hoverTileID = PublicTool.GetGameData().hoverTileID;
        SkillMapInfo skillMapInfo = PublicTool.GetGameData().GetCurSkillMapInfo();

        foreach (MapTileBase mapTile in listMapTile)
        {
            //Go through and show the view of range
            if (curUnitData.listViewSkill.Contains(mapTile.posID))
            {
                mapTile.SetIndicator(MapIndicatorType.Normal);
            }
            else
            {
                mapTile.SetIndicator(MapIndicatorType.Hide);
            }

            //Go through and show the mapTile that select
            if (curUnitData.listViewSkill.Contains(hoverTileID))
            {
                switch (skillMapInfo.regionType)
                {
                    case SkillRegionType.Circle:
                        if (PublicTool.GetTargetCircleRange(hoverTileID, skillMapInfo.radius).Contains(mapTile.posID))
                        {
                            mapTile.SetIndicator(MapIndicatorType.Red);
                        }
                        break;
                }
            }
        }
    }

    private void ResetAllTile()
    {
        foreach (MapTileBase mapTile in listMapTile)
        {
            mapTile.SetIndicator(MapIndicatorType.Hide);
        }
    }
    #endregion
}
