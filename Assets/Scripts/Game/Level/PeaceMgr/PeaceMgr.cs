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
    public int plantTypeID = -1;
    public List<Vector2Int> listValidForPlant = new List<Vector2Int>();
    public List<Vector2Int> listHavePlant = new List<Vector2Int>();

    public void StartPlantMode()
    {
        //Test
        plantTypeID = -1;
        RefreshPlantPosInfo();
    }

    public void EndPlantMode()
    {
        plantTypeID = -1;
        PublicTool.RecalculateOccupancy();

    }

    public void ModifyPlant(Vector2Int pos)
    {
        if (plantTypeID > 0)
        {
            if (!CheckWhetherCanAddPlant(pos))
            {
                return;
            }

            BattlePlantData newPlantData = gameData.GeneratePlantData(plantTypeID);
            newPlantData.posID = pos;
            unitViewMgr.GeneratePlantView(newPlantData);
            PublicTool.RecalculateOccupancy();
            RefreshPlantPosInfo();
            EventCenter.Instance.EventTrigger("RefreshResourceUI", null);

        }
        else
        {
            //RemovePlant
            if (!CheckWhetherCanRemovePlant(pos))
            {
                return;
            }
            BattlePlantData removePlantData = PublicTool.GetPlantFromPosID(pos);
            if (removePlantData != null)
            {
                unitViewMgr.RemovePlantView(removePlantData.keyID);
                gameData.RemovePlantData(removePlantData.keyID);
                PublicTool.RecalculateOccupancy();
                RefreshPlantPosInfo();
                EventCenter.Instance.EventTrigger("RefreshResourceUI", null);
            }
        }

    }

    private bool CheckWhetherCanAddPlant(Vector2Int posID)
    {
        if (!listValidForPlant.Contains(posID))
        {
            EventCenter.Instance.EventTrigger("EffectWarningText", new EffectWarningTextInfo("InvalidTile", posID));
            return false;
        }
        else
        {
            PlantExcelItem plantExcelItem = PublicTool.GetPlantItem(plantTypeID);
            if(plantExcelItem != null && (gameData.curEssence + plantExcelItem.essence > gameData.essence))
            {
                EventCenter.Instance.EventTrigger("EffectWarningText", new EffectWarningTextInfo("No Enough Essence", posID));
                return false;
            }
        }
        return true;
    }

    private bool CheckWhetherCanRemovePlant(Vector2Int posID)
    {
        if (!listHavePlant.Contains(posID))
        {
            EventCenter.Instance.EventTrigger("EffectWarningText", new EffectWarningTextInfo("InvalidTile", posID));
            return false;
        }
        return true;
    }


    private void RefreshPlantPosInfo()
    {
        listHavePlant = gameData.listTempPlantPos;
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
