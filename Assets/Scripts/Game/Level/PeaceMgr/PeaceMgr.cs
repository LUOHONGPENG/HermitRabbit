using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

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
        PublicTool.RecalculateOccupancy();

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
        PublicTool.RecalculateOccupancy();
        RefreshValidPlant();
    }

    private bool CheckWhetherCanAddPlant(Vector2Int posID)
    {
        if (!listValidForPlant.Contains(posID))
        {
            EventCenter.Instance.EventTrigger("EffectWarningText", new EffectWarningTextInfo("InvalidTile", posID));
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
        PublicTool.RecalculateOccupancy();
    }

    public void SetMapClip(Vector2Int clipPosID)
    {
        if(gameData.GetMapClipTypeID(clipPosID) == mapClipTypeID)
        {
            gameData.SetMapClipTypeID(clipPosID, -1);
        }
        else
        {
            if (gameData.CheckWhetherClipUsed(mapClipTypeID))
            {
                EventCenter.Instance.EventTrigger("EffectWarningText", new EffectWarningTextInfo("UsedClip", PublicTool.ConvertMapClipToTile(clipPosID) + Vector2Int.one));
            }
            else
            {
                gameData.SetMapClipTypeID(clipPosID, mapClipTypeID);
            }
        }

        gameData.ReadClipToTile();
        mapViewMgr.RefreshTileView();
        EventCenter.Instance.EventTrigger("RefreshMapClipUI", null);
    }

    #endregion
}
