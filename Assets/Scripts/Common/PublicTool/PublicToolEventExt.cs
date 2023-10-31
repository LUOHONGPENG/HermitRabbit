using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{
    public static void EventCameraGoPosID(Vector2Int posID)
    {
        Vector3 targetPos = ConvertPosFromID(posID);
        EventCenter.Instance.EventTrigger("CameraGoTo", targetPos);
    }

    public static void EventChangeInteract(InteractState state, int data_0=-1)
    {
        Debug.Log("ChangeState " + state);
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


    #region RefreshUI
    public static void EventRefreshCharacterUI()
    {
        EventCenter.Instance.EventTrigger("RefreshCharacterInfo", null);
    }
    #endregion

    public static void PlaySound(SoundType soundType)
    {
        EventCenter.Instance.EventTrigger("PlaySound", soundType);

    }
}
