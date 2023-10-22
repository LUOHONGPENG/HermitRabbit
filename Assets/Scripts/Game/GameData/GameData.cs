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
        NewGameResourceData();
        NewGameMapData();
        NewGameCharacterData();
        NewGamePlantData();
        NewGameFoeData();
    }

    public void LoadGameData()
    {
        //LoadCharacter

        //LoadPlant

        //LoadFoe(Maybe)
    }



}
