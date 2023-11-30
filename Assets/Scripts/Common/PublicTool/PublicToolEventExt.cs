using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{


    public static void EventSpotCharacterShow(Vector2Int posID)
    {
        Vector3 targetPos = ConvertPosFromID(posID);
        EventCenter.Instance.EventTrigger("SpotLightShowEvent", targetPos);
    }

    public static void EventSpotCharacterHide()
    {
        EventCenter.Instance.EventTrigger("SpotLightHideEvent", null);
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
                EventReadyAni(GetGameData().GetCurUnitInfo().keyID);
                EventSpotCharacterShow(GetGameData().GetCurUnitData().posID);
                RecalculateSkillCover();
                break;
            case InteractState.CharacterMove:
                EventReadyAni(-1);
                EventSpotCharacterShow(GetGameData().GetCurUnitData().posID);
                break;
            case InteractState.PeaceNormal:
            case InteractState.WaitAction:
                EventSpotCharacterHide();
                break;
        }
    }

    public static void EventReadyAni(int ID)
    {
        EventCenter.Instance.EventTrigger("CharacterReadyAni", ID);
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
    public static void PlayMusic(MusicType musicType)
    {
        EventCenter.Instance.EventTrigger("PlayMusic", musicType);

    }

    public static void StopMusic()
    {
        EventCenter.Instance.EventTrigger("StopMusic", null);

    }

    #region Tutorial

    public static void StartConditionalTutorial(TutorialGroup group,int startIndex = -1,int endIndex = -1)
    {
        EventCenter.Instance.EventTrigger("StartTutorial", new StartTutorialInfo(TutorialMode.First, group, startIndex,endIndex));

    }


    #endregion
}
