using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{
    public static void SetDay()
    {
        GetGameData().gamePhase = GamePhase.Peace;
        RenderSettings.skybox = GameMgr.Instance.skyDay;
    }

    public static void SetNight()
    {
        GetGameData().gamePhase = GamePhase.Battle;
        RenderSettings.skybox = GameMgr.Instance.skyNight;

    }


}
