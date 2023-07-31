using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameData
{
    #region Basic-Map

    //MapTileData
    public List<MapTileData> listMapTile = new List<MapTileData>();
    public Dictionary<Vector2Int, MapTileData> dicMapTile = new Dictionary<Vector2Int, MapTileData>();

    public void NewGameMapTileData()
    {
        listMapTile.Clear();
        dicMapTile.Clear();

        for (int i = 0; i < GameGlobal.mapSize; i++)
        {
            for (int j = 0; j < GameGlobal.mapSize; j++)
            {
                Vector2Int posID = new Vector2Int(i, j);

                MapTileData mapTileData = new MapTileData(posID, 1);
                listMapTile.Add(mapTileData);
                dicMapTile.Add(posID, mapTileData);
            }
        }
    }

    #endregion


    #region HoverPos
    public Vector2Int hoverTileID = new Vector2Int(-99, -99);
    #endregion


    #region CalculateRegion
    public List<Vector2Int> listTempDeadCharacterPos = new List<Vector2Int>();
    public List<Vector2Int> listTempCharacterPos = new List<Vector2Int>();
    public List<Vector2Int> listTempFoePos = new List<Vector2Int>();
    public List<Vector2Int> listTempPlantPos = new List<Vector2Int>();
    public List<Vector2Int> listTempFriendPos = new List<Vector2Int>();
    public List<Vector2Int> listTempObstablePos = new List<Vector2Int>();

    public List<Vector2Int> listTempAllPos = new List<Vector2Int>();
    public List<Vector2Int> listTempEmptyPos = new List<Vector2Int>();
    public Dictionary<Vector2Int, UnitInfo> dicTempMapUnit = new Dictionary<Vector2Int, UnitInfo>();

    //Refresh Valid Range for display map
    public void RecalculateOccupancy()
    {
        ScanMapUnitInfo();

        foreach (var foe in listFoe)
        {
            foe.RefreshAttackRange();
        }

        //Need Optimise
        if(BattleMgr.Instance.battleTurnPhase == BattlePhase.CharacterPhase)
        {
            foreach (var character in listCharacter)
            {
                character.RefreshValidCharacterMoveBFSNode();
            }
        }
    }

    public void RecalculateSkillCover()
    {
        GetCurUnitData().RefreshValidSkill();
    }

    private void ScanMapUnitInfo()
    {
        dicTempMapUnit.Clear();
        listTempCharacterPos.Clear();
        listTempFoePos.Clear();
        listTempPlantPos.Clear();
        listTempFriendPos.Clear();
        listTempAllPos.Clear();
        listTempEmptyPos.Clear();

        foreach (var character in listCharacter)
        {
            dicTempMapUnit.Add(character.posID, new UnitInfo(BattleUnitType.Character, character.keyID));
            if (character.isDead)
            {
                listTempDeadCharacterPos.Add(character.posID);
            }
            else
            {
                listTempCharacterPos.Add(character.posID);
            }
            listTempFriendPos.Add(character.posID);
            listTempAllPos.Add(character.posID);
        }
        foreach (var plant in listPlant)
        {
            if (plant.isDead)
            {
                continue;
            }
            dicTempMapUnit.Add(plant.posID, new UnitInfo(BattleUnitType.Plant, plant.keyID));
            listTempPlantPos.Add(plant.posID);
            listTempFriendPos.Add(plant.posID);
            listTempAllPos.Add(plant.posID);
        }
        foreach (var foe in listFoe)
        {
            if (foe.isDead)
            {
                continue;
            }
            dicTempMapUnit.Add(foe.posID, new UnitInfo(BattleUnitType.Foe, foe.keyID));
            listTempFoePos.Add(foe.posID);
            listTempAllPos.Add(foe.posID);
        }

        foreach (var map in listMapTile)
        {
            if (!listTempAllPos.Contains(map.posID))
            {
                listTempEmptyPos.Add(map.posID);
            }
        }
    }

    #endregion

    #region Application

    public List<Vector2Int> GetFoeBlockPos()
    {
        List<Vector2Int> listTemp = new List<Vector2Int>();
        AddPosForList(listTemp, listTempCharacterPos);
        AddPosForList(listTemp, listTempPlantPos);
        AddPosForList(listTemp, listTempObstablePos);
        return listTemp;
    }

    public List<Vector2Int> GetFoeTargetPos()
    {
        List<Vector2Int> listTemp = new List<Vector2Int>();
        AddPosForList(listTemp, listTempCharacterPos);
        AddPosForList(listTemp, listTempPlantPos);
        return listTemp;
    }

    public void AddPosForList(List<Vector2Int> listContainer, List<Vector2Int> listTarget)
    {
        for (int i = 0; i < listTarget.Count; i++)
        {
            Vector2Int tempPos = listTarget[i];
            if (!listContainer.Contains(tempPos))
            {
                listContainer.Add(tempPos);
            }
        }
    }
    #endregion


}

