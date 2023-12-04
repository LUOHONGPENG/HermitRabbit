using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{
    public static void SetDay()
    {
        GetGameData().gamePhase = GamePhase.Peace;
        RenderSettings.skybox = GameMgr.Instance.skyDay;
        if (GameMgr.Instance.curSceneGameMgr != null)
        {
            GameMgr.Instance.curSceneGameMgr.lightMgr.SetDay();
        }
    }

    public static void SetNight()
    {
        GetGameData().gamePhase = GamePhase.Battle;
        RenderSettings.skybox = GameMgr.Instance.skyNight;
        if (GameMgr.Instance.curSceneGameMgr != null)
        {
            GameMgr.Instance.curSceneGameMgr.lightMgr.SetNight();
        }
    }


}
