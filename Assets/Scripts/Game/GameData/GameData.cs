using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Storing the Data
/// </summary>
public partial class GameData
{
    //Current Phase
    public GamePhase gamePhase = GamePhase.Peace;
    public int numDay = 1;

    public void NewGame()
    {
        numDay = 1;
        gamePhase = GamePhase.Peace;
        NewGameResourceData();
        NewGameMapData();
        NewGameCharacterData();
        NewGamePlantData();
        NewGameFoeData();
    }

    #region LoadSaveData
    public void LoadGameData(GameSaveData saveData)
    {
        //NeedToModify
        numDay = saveData.numDay;
        gamePhase = GamePhase.Peace;
        LoadResourceData(saveData);
        LoadMapData(saveData);
        LoadCharacterData (saveData);
        LoadPlantData(saveData);
        NewGameFoeData();

        
        //LoadCharacter

        //LoadPlant

        //LoadFoe(Maybe)
    }

    public void SaveGameData(GameSaveData saveData)
    {
        saveData.numDay = numDay;
        SaveResourceData(saveData);
        SaveMapData(saveData);
        SaveCharacterData(saveData);
        SavePlantData(saveData);
    }
    #endregion
}
