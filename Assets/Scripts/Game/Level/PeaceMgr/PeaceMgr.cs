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

    public int plantID = -1;
    public List<Vector2Int> listValidPlant = new List<Vector2Int>();


    public void StartPlant()
    {
        plantID = 1001;
        RefreshValidPlant();
    }

    public void EndPlant()
    {

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
        if (!listValidPlant.Contains(pos))
        {
            EventCenter.Instance.EventTrigger("EffectWarningText", new EffectWarningTextInfo("InvalidTile", pos));
            return false;
        }

        return true;
    }

    private void RefreshValidPlant()
    {
        listValidPlant = gameData.listTempEmptyPos;
    }
}