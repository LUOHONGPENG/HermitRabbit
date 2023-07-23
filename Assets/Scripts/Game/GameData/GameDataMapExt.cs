using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameData
{
    #region CalculateRegion
    public List<Vector2Int> listTempCharacterPos = new List<Vector2Int>();
    public List<Vector2Int> listTempFoePos = new List<Vector2Int>();
    public List<Vector2Int> listTempPlantPos = new List<Vector2Int>();
    public List<Vector2Int> listTempAllPos = new List<Vector2Int>();
    public List<Vector2Int> listTempEmptyPos = new List<Vector2Int>();
    public Dictionary<Vector2Int, UnitInfo> dicTempMapUnit = new Dictionary<Vector2Int, UnitInfo>();

    //Refresh Valid Range for display map
    public void RefreshOccupancyInfo()
    {
        listTempCharacterPos = GetCharacterPos();
        listTempFoePos = GetFoePos();
        listTempPlantPos = GetPlantPos();
        listTempAllPos = GetAllPos();
        listTempEmptyPos = GetEmptyPos();
        GenerateDicMapUnit();

        foreach (var foe in listFoe)
        {
            foe.RefreshAttackRange();
        }

        foreach (var character in listCharacter)
        {
            character.RefreshValidMove();
        }
        Debug.Log("Refresh Occupancy Info");
    }

    public void RefreshSkillTileInfo()
    {
        GetCurUnitData().RefreshValidSkill();
    }

    private List<Vector2Int> GetCharacterPos()
    {
        List<Vector2Int> temp = new List<Vector2Int>();
        foreach(var character in listCharacter)
        {
            temp.Add(character.posID);
        }
        return temp;
    }

    private List<Vector2Int> GetFoePos()
    {
        List<Vector2Int> temp = new List<Vector2Int>();
        foreach (var foe in listFoe)
        {
            temp.Add(foe.posID);
        }
        return temp;
    }

    private List<Vector2Int> GetPlantPos()
    {
        List<Vector2Int> temp = new List<Vector2Int>();
        foreach (var plant in listPlant)
        {
            temp.Add(plant.posID);
        }
        return temp;
    }


    private List<Vector2Int> GetAllPos()
    {
        List<Vector2Int> temp = new List<Vector2Int>();
        foreach (var character in listCharacter)
        {
            temp.Add(character.posID);
        }
        foreach (var plant in listPlant)
        {
            temp.Add(plant.posID);
        }
        foreach (var foe in listFoe)
        {
            temp.Add(foe.posID);
        }
        return temp;
    }

    private List<Vector2Int> GetEmptyPos()
    {
        List<Vector2Int> temp = new List<Vector2Int>();

        foreach(var map in listMapTile)
        {
            if (!listTempAllPos.Contains(map.posID))
            {
                temp.Add(map.posID);
            }
        }
        return temp;
    }

    private void GenerateDicMapUnit()
    {
        dicTempMapUnit.Clear();

        foreach (var character in listCharacter)
        {
            dicTempMapUnit.Add(character.posID, new UnitInfo(BattleUnitType.Character, character.keyID));
        }
        foreach (var plant in listPlant)
        {
            dicTempMapUnit.Add(plant.posID, new UnitInfo(BattleUnitType.Plant, plant.keyID));
        }
        foreach (var foe in listFoe)
        {
            dicTempMapUnit.Add(foe.posID, new UnitInfo(BattleUnitType.Foe, foe.keyID));
        }
    }

    #endregion

    #region HoverPos
    public Vector2Int hoverTileID = new Vector2Int(-99, -99);
    #endregion
}

