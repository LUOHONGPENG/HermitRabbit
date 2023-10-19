using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PeaceMgr : MonoSingleton<PeaceMgr>
{
    private MapViewMgr mapViewMgr;
    private UnitViewMgr unitViewMgr;
    private GameData gameData;

    public void Init(LevelMgr levelMgr)
    {
        this.mapViewMgr = levelMgr.mapViewMgr;
        this.unitViewMgr = levelMgr.unitViewMgr;
        this.gameData = PublicTool.GetGameData();
    }

    #region Plant
    //Plant Controller
    public int plantID = -1;
    public List<Vector2Int> listValidForPlant = new List<Vector2Int>();

    public void StartPlantMode()
    {
        //Test
        plantID = 1001;
        RefreshValidPlant();
    }

    public void EndPlantMode()
    {
        plantID = -1;
    }

    public void AddPlant(Vector2Int pos)
    {
        if (!CheckWhetherCanAddPlant(pos))
        {
            return;
        }

        BattlePlantData newPlantData = gameData.GeneratePlantData(plantID);
        newPlantData.posID = pos;
        unitViewMgr.GeneratePlantView(newPlantData);
        RefreshValidPlant();
    }

    private bool CheckWhetherCanAddPlant(Vector2Int pos)
    {
        if (!listValidForPlant.Contains(pos))
        {
            EventCenter.Instance.EventTrigger("EffectWarningText", new EffectWarningTextInfo("InvalidTile", pos));
            return false;
        }

        return true;
    }

    private void RefreshValidPlant()
    {
        listValidForPlant = gameData.listTempEmptyPos;
    }

    #endregion

    #region MapClip
    //MapClip Type Controller
    public int mapClipTypeID = -1;


    public void StartMapClipMode()
    {
        mapClipTypeID = -1;
    }

    public void EndMapClipMode()
    {

    }

    public void SetMapClip(Vector2Int posID)
    {
        gameData.SetMapClip(posID, mapClipTypeID);
        gameData.ReadClipToTile();
        mapViewMgr.RefreshTileView();
    }

    #endregion
}
