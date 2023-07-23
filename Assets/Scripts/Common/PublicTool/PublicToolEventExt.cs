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
                    GetGameData().SetCurBattleSkillID(data_0);
                }
                EventRefreshSkill();
                break;
        }
    }

    public static void EventRefreshOccupancy()
    {
        EventCenter.Instance.EventTrigger("RefreshOccupancy", null);
    }

    public static void EventRefreshSkill()
    {
        EventCenter.Instance.EventTrigger("RefreshSkillRange", null);
    }

    public static void EventRefreshCharacterUI()
    {
        EventCenter.Instance.EventTrigger("RefreshCharacterInfo", null);
    }
}
