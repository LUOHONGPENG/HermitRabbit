using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public partial class PublicTool
{
    public static void SetDay()
    {
        GetGameData().gamePhase = GamePhase.Peace;

    }

    public static void SetNight()
    {
        GetGameData().gamePhase = GamePhase.Battle;

    }


}
