using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class InputMgr
{
    #region Touch
    public InteractState interactState;

    public void SetInteractState(InteractState state)
    {
        interactState = state;
    }

    private void CheckClickAction()
    {
        //ClickUI
        if (CheckWhetherHoverUI())
        {
            return;
        }

        if (CheckWhetherHitUnit())
        {
            return;
        }
        if (CheckWhetherHitMap())
        {
            return;
        }
        Debug.Log("Click No Action");
    }




    private bool CheckWhetherHitUnit()
    {
        Physics.Raycast(GetMouseRay(), out RaycastHit hitData, 1000f, LayerMask.GetMask("Unit"));
        if (hitData.transform != null)
        {
            //Hit the character
            if (hitData.transform.parent.GetComponent<BattleCharacterView>() != null)
            {
                BattleCharacterView character = hitData.transform.parent.GetComponent<BattleCharacterView>();
                return DealClickCharacterAction(character);
            }
        }
        return false;
    }

    private bool CheckWhetherHitMap()
    {
        Physics.Raycast(GetMouseRay(), out RaycastHit hitData, 1000f, LayerMask.GetMask("Map"));
        if (hitData.transform != null)
        {
            if (hitData.transform.parent.parent.GetComponent<MapTileBase>() != null)
            {
                MapTileBase mapTile = hitData.transform.parent.parent.GetComponent<MapTileBase>();
                return DealClickMapAction(mapTile);
            }
        }
        return false;
    }
    #endregion

    #region DealClickAction

    private bool DealClickCharacterAction(BattleCharacterView character)
    {
        LevelMgr level;
        if (PublicTool.GetLevelMgr() != null)
        {
            level = PublicTool.GetLevelMgr();
        }
        else
        {
            return false;
        }

        if(level.levelPhase == LevelPhase.Battle)
        {
            switch (interactState)
            {
                case InteractState.Normal:
                    EventCenter.Instance.EventTrigger("InputChooseCharacter", character.GetTypeID());
                    return true;
                case InteractState.Move:
                    EventCenter.Instance.EventTrigger("InputChooseCharacter", character.GetTypeID());
                    break;
                case InteractState.Target:
                    break;
                case InteractState.WaitAction:
                    break;
            }
        }
        return false;
    }

    private bool DealClickMapAction(MapTileBase mapTile)
    {
        LevelMgr level;
        if (PublicTool.GetLevelMgr() != null)
        {
            level = PublicTool.GetLevelMgr();
        }
        else
        {
            return false;
        }


        if (level.levelPhase == LevelPhase.Battle)
        {
            switch (interactState)
            {
                case InteractState.Normal:
                    EventCenter.Instance.EventTrigger("CameraGoTo", mapTile.transform.position);
                    return true;
                case InteractState.Move:
                    EventCenter.Instance.EventTrigger("InputMoveAction", mapTile.posID);
                    return true;
            }
        }
        else if(level.levelPhase == LevelPhase.Peace)
        {
            switch (interactState)
            {
                case InteractState.Normal:
                    EventCenter.Instance.EventTrigger("CameraGoTo", mapTile.transform.position);
                    return true;
                case InteractState.Move:

                    break;
            }
        }
        return false;
    }
    #endregion

    #region Touch Position

    private Ray GetMouseRay()
    {
        Vector2 screenPosition = touchPositionAction.ReadValue<Vector2>();
        Ray ray = new Ray();
        if (GameMgr.Instance.curMapCamera != null)
        {
            ray = GameMgr.Instance.curMapCamera.ScreenPointToRay(screenPosition);
        }
        return ray;
    }
    #endregion
}
