using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameData
{
    #region Basic-Map
    //MapTileData
    public List<MapTileData> listMapTile = new List<MapTileData>();
    public Dictionary<Vector2Int, MapTileData> dicMapTile = new Dictionary<Vector2Int, MapTileData>();
    //MapClipUsedData
    public List<MapClipUsedData> listMapClipUsed = new List<MapClipUsedData>();
    public Dictionary<Vector2Int, MapClipUsedData> dicMapClipUsed = new Dictionary<Vector2Int, MapClipUsedData>();
    //MapClipHeld
    public List<int> listMapClipHeld = new List<int>();

    public void NewGameMapData()
    {
        listMapTile.Clear();
        dicMapTile.Clear();

        for (int i = 0; i < GameGlobal.mapClipSize * GameGlobal.mapClipNumX; i++)
        {
            for (int j = 0; j < GameGlobal.mapClipSize * GameGlobal.mapClipNumY + GameGlobal.mapRowFriend + GameGlobal.mapRowFoe; j++)
            {
                Vector2Int posID = new Vector2Int(i, j);

                MapTileData mapTileData = new MapTileData(posID);
                listMapTile.Add(mapTileData);
                dicMapTile.Add(posID, mapTileData);
            }
        }

        listMapClipUsed.Clear();
        dicMapClipUsed.Clear();
        for (int i = 0; i < GameGlobal.mapClipNumX; i++)
        {
            for (int j = 0; j < GameGlobal.mapClipNumY; j++)
            {
                Vector2Int clipPosID = new Vector2Int(i, j);

                MapClipUsedData mapClipUsedData = new MapClipUsedData(clipPosID);
                listMapClipUsed.Add(mapClipUsedData);
                dicMapClipUsed.Add(clipPosID, mapClipUsedData);
            }
        }

        listMapClipHeld.Clear();


        //
        ReadClipToTile();
    }


    public void ReadClipToTile()
    {
        //(0,0)->()
        foreach(var clip in listMapClipUsed)
        {
            Vector2Int startPos = clip.clipPosID * GameGlobal.mapClipSize + new Vector2Int(0,GameGlobal.mapRowFriend);
            //Calculate the center of the clip
            if (clip.clipID < 0)
            {
                for (int i = 0; i < GameGlobal.mapClipSize; i++)
                {
                    for (int j = 0; j < GameGlobal.mapClipSize; j++)
                    {
                        Vector2Int tarPos = startPos + new Vector2Int(j, i);
                        if (dicMapTile.ContainsKey(tarPos))
                        {
                            dicMapTile[tarPos].tileType = MapTileType.Normal;
                        }
                    }
                }
            }
            else
            {
                MapClipExcelItem clipItem = PublicTool.GetMapClipItem(clip.clipID);
                for(int i = 0;i< GameGlobal.mapClipSize; i++)
                {
                    for (int j = 0; j < GameGlobal.mapClipSize; j++)
                    {
                        Vector2Int tarPos = startPos + new Vector2Int(j, i);
                        if (dicMapTile.ContainsKey(tarPos))
                        {
                            dicMapTile[tarPos].tileType = clipItem.listMapTile[i * GameGlobal.mapClipSize + j];
                        }
                    }
                }
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

    public List<Vector2Int> listMapStonePos = new List<Vector2Int>();

    //Refresh Valid Range for display map
    public void RecalculateOccupancy()
    {
        //Scan Stone Pos
        ScanMapStonePos();
        //Scan Unit Pos
        ScanMapUnitInfo();

        foreach (var foe in listFoe)
        {
            foe.RefreshAttackRange();
        }

        //Need Optimise
        if(BattleMgr.Instance.battleTurnPhase == BattlePhase.CharacterPhase)
        {
            //Scan the possible position of the character
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

    /// <summary>
    /// Scan Map Unit Pos
    /// </summary>
    private void ScanMapUnitInfo()
    {
        dicTempMapUnit.Clear();
        listTempCharacterPos.Clear();
        listTempFoePos.Clear();
        listTempPlantPos.Clear();
        listTempFriendPos.Clear();
        listTempAllPos.Clear();
        listTempEmptyPos.Clear();
        //Scan character to get position
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

    //Scan the position of the stone
    public void ScanMapStonePos()
    {
        //listMapStonePos
        listMapStonePos.Clear();

        foreach (var map in listMapTile)
        {
            if(map.tileType == MapTileType.Stone)
            {
                listMapStonePos.Add(map.posID);
                Debug.Log(map.posID);
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

    //Help the foe to find the position of the character or plants
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

