using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{
    public static LevelMgr GetLevelMgr()
    {
        if (GameMgr.Instance.curSceneGameMgr != null && GameMgr.Instance.curSceneGameMgr.levelMgr != null)
        {
            return GameMgr.Instance.curSceneGameMgr.levelMgr;
        }
        else
        {
            return null;
        }
    }
}
