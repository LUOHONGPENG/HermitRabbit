using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public partial class MapViewMgr : MonoBehaviour
{
    [Header("MapTile")]
    public Transform tfMapTile;
    public GameObject pfMapTile;
    public List<MapTileBase> listMapTile = new List<MapTileBase>();
    public Dictionary<Vector2Int, MapTileBase> dicMapTile = new Dictionary<Vector2Int, MapTileBase>();

    private bool isInit = false;
    private BattleUnitData curUnitData;
    private GameData gameData;

    public void Init()
    {
        gameData = PublicTool.GetGameData();
        PublicTool.SetDay();
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
        foreach(var mapTileData in gameData.listMapTile)
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

    public void RefreshTileView()
    {
        for (int i = 0; i < listMapTile.Count; i++)
        {
            listMapTile[i].RefreshMapTile();
        }
    }

    #endregion

    #region Display the MapTile state Common
    public void TimeGo()
    {
        if (!isInit)
        {
            return;
        }

        UpdateMapUI();
        UpdateMapHavorUnitUI();
    }

    public void RefreshCurUnit()
    {
        curUnitData = PublicTool.GetGameData().GetCurUnitData();
    }

    private void UpdateMapUI()
    {
        InteractState state = InputMgr.Instance.GetInteractState();

        //I think this need a optimisation
        switch (state)
        {
            case InteractState.PeaceNormal:
                ResetAllTile();
                ResetHitAlly();
                break;
            case InteractState.PeacePlant:
                SetMapUI_Plant();
                ResetHitAlly();
                break;
            case InteractState.PeaceMap:
                SetMapUI_MapClip();
                ResetHitAlly();
                break;
            case InteractState.BattleNormal:
                ResetAllTile();
                ResetHitAlly();
                break;
            case InteractState.CharacterMove:
                SetMapUI_CharacterMove();
                ResetHitAlly();
                break;
            case InteractState.CharacterSkill:
                SetMapUI_Skill();
                break;
            case InteractState.WaitAction:
                ResetAllTile();
                ResetHitAlly();
                break;
        }

        if(state!= InteractState.WaitAction && state != InteractState.PeaceMap)
        {
            Vector2Int hoverTileID = gameData.hoverTileID;

            foreach (MapTileBase mapTile in listMapTile)
            {
                if(mapTile.posID == hoverTileID)
                {
                    mapTile.UpdateSelect(true);
                }
                else
                {
                    mapTile.UpdateSelect(false);
                }
            }
        }
        else
        {
            foreach (MapTileBase mapTile in listMapTile)
            {
                mapTile.UpdateSelect(false);
            }
        }

    }
    #endregion

    #region Display Common Interact

    private void SetMapUI_CharacterMove()
    {
        //Go through
        foreach (MapTileBase mapTile in listMapTile)
        {
            if (curUnitData.dicValidMoveNode.ContainsKey(mapTile.posID))
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
        SkillBattleInfo skillMapInfo = gameData.GetCurSkillBattleInfo();

        //Deal with the hover radius
        Vector2Int hoverTileID = gameData.hoverTileID;
        bool isHover = false;
        List<Vector2Int> listHoverPos = new List<Vector2Int>();
        if (curUnitData != null && curUnitData.listViewSkill.Contains(hoverTileID))
        {
            isHover = true;
            switch (skillMapInfo.regionType)
            {
                case SkillRegionType.Circle:
                    listHoverPos = PublicTool.GetTargetCircleRange(hoverTileID, skillMapInfo.radius);
                    break;
                case SkillRegionType.Square:
                    listHoverPos = PublicTool.GetTargetSquareRange(hoverTileID, skillMapInfo.radius);
                    break;
                case SkillRegionType.Line:
                    listHoverPos = PublicTool.GetTargetLineRange(hoverTileID, curUnitData.posID,skillMapInfo.range, skillMapInfo.radius) ;
                    break;
                case SkillRegionType.Water:
                    listHoverPos = PublicTool.GetTargetCircleRange(hoverTileID, skillMapInfo.radius);
                    listHoverPos = BattleMgr.Instance.GetTargetWaterRange(listHoverPos);
                    break;
                case SkillRegionType.BurnUnit:
                    listHoverPos = PublicTool.GetTargetBurningRange();
                    listHoverPos = PublicTool.GetTargetBurningRadius(listHoverPos,skillMapInfo.radius);
                    break;
            }
        }

        //Check whether cover a target
        bool isCover = false;
        if (curUnitData.listValidSkill.Contains(hoverTileID))
        {
            isCover = true;
        }

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


            ResetThisHitAlly(mapTile);

            if (isHover)
            {
                if (isCover)
                {
                    if (listHoverPos.Contains(mapTile.posID))
                    {
                        mapTile.SetIndicator(MapIndicatorType.AttackCover);
                        CheckHitAlly(mapTile, skillMapInfo);
                    }
                }
                else
                {
                    if (listHoverPos.Contains(mapTile.posID))
                    {
                        mapTile.SetIndicator(MapIndicatorType.AttackRadius);
                    }
                }
            }


        }
    }

    private void SetMapUI_Plant()
    {
        //Go through
        if (PeaceMgr.Instance.plantTypeID > 0)
        {
            foreach (MapTileBase mapTile in listMapTile)
            {
                if (PeaceMgr.Instance.listValidForPlant.Contains(mapTile.posID))
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
        else
        {
            foreach (MapTileBase mapTile in listMapTile)
            {
                if (PeaceMgr.Instance.listHavePlant.Contains(mapTile.posID))
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
    }

    private void SetMapUI_MapClip()
    {
        foreach (MapTileBase mapTile in listMapTile)
        {
            Vector2Int clipPosID = PublicTool.ConvertMapTileIDToClip(mapTile.posID);

            if (clipPosID.x < 0)
            {
                mapTile.SetIndicator(MapIndicatorType.Hide);
            }
            else
            {
                Vector2 hoverClipPosID = PublicTool.ConvertMapTileIDToClip(PublicTool.GetGameData().hoverTileID);
                if(clipPosID == hoverClipPosID)
                {
                    mapTile.SetIndicator(MapIndicatorType.Blue);
                }
                else
                {
                    mapTile.SetIndicator(MapIndicatorType.Normal);
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

    private void CheckHitAlly(MapTileBase mapView,SkillBattleInfo skillInfo)
    {
        if(skillInfo.characterEffect == SkillEffectType.Harm)
        {
            Vector2Int posID = mapView.posID;
            if(gameData.GetUnitInfoFromPosID(posID).type == BattleUnitType.Character)
            {
                mapView.SetHitAlly(true);
                return;
            }
        }

        if (skillInfo.plantEffect == SkillEffectType.Harm)
        {
            Vector2Int posID = mapView.posID;
            if (gameData.GetUnitInfoFromPosID(posID).type == BattleUnitType.Plant)
            {
                mapView.SetHitAlly(true);
                return;
            }
        }

        mapView.SetHitAlly(false);
    }

    private void ResetThisHitAlly(MapTileBase mapTile)
    {
        mapTile.SetHitAlly(false);
    }
    private void ResetHitAlly()
    {
        foreach (MapTileBase mapTile in listMapTile)
        {
            ResetThisHitAlly(mapTile);
        }
    }
    #endregion


    #region Display Havor Unit Range

    public void UpdateMapHavorUnitUI()
    {
        InteractState state = InputMgr.Instance.GetInteractState();

        if(state != InteractState.WaitAction && state != InteractState.CharacterSkill)
        {
            if (gameData.GetUnitInfoFromHoverTileID().type == BattleUnitType.Plant)
            {
                BattlePlantData plantData = (BattlePlantData)gameData.GetDataFromUnitInfo(gameData.GetUnitInfoFromHoverTileID());
                SetMapRangeUI_Plant(plantData.listValidTouchRange);
            }
            else if(gameData.GetUnitInfoFromHoverTileID().type == BattleUnitType.Foe)
            {
                BattleFoeData foeData = (BattleFoeData)gameData.GetDataFromUnitInfo(gameData.GetUnitInfoFromHoverTileID());
                SetMapRangeUI_Foe(foeData.listValidTouchRange, foeData.listValidAttackRange);
            }
            else
            {
                ResetAllTileHavorUnit();
            }
        }
        else
        {
            ResetAllTileHavorUnit();
        }
    }


    private void SetMapRangeUI_Plant(List<Vector2Int> listPos)
    {
        foreach (MapTileBase mapTile in listMapTile)
        {
            mapTile.SetPlantRangeIndicator(listPos.Contains(mapTile.posID));
        }
    }

    private void SetMapRangeUI_Foe(List<Vector2Int> listMove,List<Vector2Int> listAttack)
    {
        foreach(MapTileBase mapTile in listMapTile)
        {
            bool isInMove = listMove.Contains(mapTile.posID);
            bool isInAttack = listAttack.Contains(mapTile.posID);
            mapTile.SetFoeRangeIndicator(isInMove, isInAttack);
        }


    }


/*    private void SetMapRangeUI_Foe()
    {

    }*/

    private void ResetAllTileHavorUnit()
    {
        foreach (MapTileBase mapTile in listMapTile)
        {
            mapTile.ResetRangeIndicator();
        }
    }

    #endregion
}
