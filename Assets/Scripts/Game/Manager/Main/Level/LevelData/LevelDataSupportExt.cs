using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LevelData
{
    public List<Vector2Int> listTempFriendPos = new List<Vector2Int>();
    public List<Vector2Int> listTempFoePos = new List<Vector2Int>();
    public List<Vector2Int> listTempAllPos = new List<Vector2Int>();

    //Refresh Valid Range for display map
    public void RefreshTempPos()
    {
        listTempFriendPos = GetFriendPos();
        listTempFoePos = GetFoePos();
        listTempAllPos = GetFullPos();

        foreach (var character in listCharacter)
        {
            character.RefreshValidMove();
        }
        foreach (var foe in listFoe)
        {
            //foe.RefreshValidMove();
            foe.RefreshValidRange();
        }

        Debug.Log("Refresh Unit Move Range");
    }

    public List<Vector2Int> GetFriendPos()
    {
        List<Vector2Int> temp = new List<Vector2Int>();
        foreach(var character in listCharacter)
        {
            temp.Add(character.posID);
        }
        foreach (var plant in listPlant)
        {
            temp.Add(plant.posID);
        }
        return temp;
    }

    public List<Vector2Int> GetFoePos()
    {
        List<Vector2Int> temp = new List<Vector2Int>();
        foreach (var foe in listFoe)
        {
            temp.Add(foe.posID);
        }
        return temp;
    }

    public List<Vector2Int> GetFullPos()
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
}
