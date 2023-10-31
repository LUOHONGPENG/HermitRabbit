using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{

    public static void RecalculateOccupancy()
    {
        if (GetGameData() != null)
        {
            GetGameData().RecalculateOccupancy();
        }
    }

    public static void RecalculateSkillCover()
    {
        if (GetGameData() != null)
        {
            GetGameData().RecalculateSkillCover();
        }
    }


}
