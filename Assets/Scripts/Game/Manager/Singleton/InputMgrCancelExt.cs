using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class InputMgr
{
    private void CheckCancelAction()
    {
        if (CheckWhetherHoverUI())
        {
            return;
        }

        DealCancelAction();
    }

    private void DealCancelAction()
    {
        switch (interactState)
        {
            case InteractState.PeacePlant:/*
                SetInteractState(InteractState.PeaceNormal);
                EventCenter.Instance.EventTrigger("PeacePlantEnd", null);*/
                break;
            case InteractState.PeaceMap:
/*                SetInteractState(InteractState.PeaceNormal);
                EventCenter.Instance.EventTrigger("PeaceMapEnd", null);*/
                break;
            case InteractState.CharacterMove:
                SetInteractState(InteractState.BattleNormal);
                EventCenter.Instance.EventTrigger("InputCancelCharacter", null);
                break;
            case InteractState.CharacterSkill:
                if (BattleMgr.Instance.isFirstTargetSelected)
                {
                    BattleMgr.Instance.BattleSkillReset();
                }
                else
                {
                    SetInteractState(InteractState.BattleNormal);
                    EventCenter.Instance.EventTrigger("InputCancelCharacter", null);
                }
                break;
        }
    }
}
