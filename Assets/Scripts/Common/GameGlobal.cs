using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameGlobal
{
    public static SceneName targetScene = SceneName.Menu;

    public static float cameraLimit = 3.2f;

    #region Map

    //The size of a clip
    public static int mapClipSize = 3;
    public static float commonUnitPosY = 0.35f;
    //How many clip in a map
    public static int mapClipNumX = 3;
    public static int mapClipNumY = 3;
    public static int mapRowFriend = 1;
    public static int mapRowFoe = 1;
    public static int mapRowCanPlant = 5;

    public static float skillNodeSpacingX = 120f;
    public static float skillNodeSpacingY = 200f;

    public static float waitTimeText = 0.6f;

    public static int mapMaxNumX
    {
        get
        {
            return mapClipNumX * mapClipSize;
        }
    }

    public static int mapMaxNumY
    {
        get
        {
            return mapClipNumY * mapClipSize + mapRowFriend + mapRowFoe;
        }
    }

    #endregion

    #region Cost

    public static int CostRefreshMapClip = 50;
    public static int CostRefreshPlant = 50;
    public static int AddSkipMapClip = 20;
    public static int AddSkipPlant = 20;

    #endregion
}
