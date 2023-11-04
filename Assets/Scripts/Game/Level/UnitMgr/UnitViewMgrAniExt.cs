using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UnitViewMgr
{
    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("CharacterReadyAni", CharacterReadyAniEvent);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("CharacterReadyAni", CharacterReadyAniEvent);
    }

    private void CharacterReadyAniEvent(object arg0)
    {
        int ID = (int)arg0;
        switch (ID)
        {
            case 1001:
                dicCharacterView[1001].ChangeAniState(UnitAniState.Ready);
                dicCharacterView[1002].ChangeAniState(UnitAniState.Idle);
                break;
            case 1002:
                dicCharacterView[1001].ChangeAniState(UnitAniState.Idle);
                dicCharacterView[1002].ChangeAniState(UnitAniState.Ready);
                break;
            default:
                dicCharacterView[1001].ChangeAniState(UnitAniState.Idle);
                dicCharacterView[1002].ChangeAniState(UnitAniState.Idle);
                break;
        }

    }

    private void UnitCommonAniEvent()
    {

    }
}
