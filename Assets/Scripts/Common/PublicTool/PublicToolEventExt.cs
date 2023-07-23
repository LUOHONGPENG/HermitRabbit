using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{
    public static void EventCameraGo(Vector2Int posID)
    {
        Vector3 targetPos = ConvertPosFromID(posID);
        EventCenter.Instance.EventTrigger("CameraGoTo", targetPos);
    }

    public static void EventChangeInteract(InteractState state, int data_0=-1)
    {
        InputMgr.Instance.SetInteractState(state);
        switch (state)
        {
            case InteractState.CharacterSkill:
                if (data_0 > 0)
                {
                    GetGameData().SetCurBattleSkill(data_0);
                }
                RecalculateSkillCover();
                break;
        }
    }

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


    #region RefreshUI
    public static void EventRefreshCharacterUI()
    {
        EventCenter.Instance.EventTrigger("RefreshCharacterInfo", null);
    }
    #endregion
}
