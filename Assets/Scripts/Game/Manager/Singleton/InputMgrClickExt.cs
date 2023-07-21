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
        GameData gameData = PublicTool.GetGameData();

        if(gameData.gamePhase == GamePhase.Battle)
        {
            switch (interactState)
            {
                case InteractState.Normal:
                    EventCenter.Instance.EventTrigger("InputChooseCharacter", character.GetTypeID());
                    PublicTool.EventCameraGo(character.characterData.posID);
                    return true;
                case InteractState.Move:
                    EventCenter.Instance.EventTrigger("InputChooseCharacter", character.GetTypeID());
                    PublicTool.EventCameraGo(character.characterData.posID);
                    return true;
                case InteractState.Skill:
                    break;
                case InteractState.WaitAction:
                    break;
            }
        }
        return false;
    }

    private bool DealClickMapAction(MapTileBase mapTile)
    {
        GameData gameData = PublicTool.GetGameData();


        if (gameData.gamePhase == GamePhase.Battle)
        {
            switch (interactState)
            {
                case InteractState.Normal:
                    PublicTool.EventCameraGo(mapTile.posID);
                    return true;
                case InteractState.Move:
                    EventCenter.Instance.EventTrigger("InputMoveAction", mapTile.posID);
                    return true;
                case InteractState.Skill:
                    EventCenter.Instance.EventTrigger("InputSkillAction", mapTile.posID);
                    return true;
            }
        }
        else if(gameData.gamePhase == GamePhase.Peace)
        {
            switch (interactState)
            {
                case InteractState.Normal:
                    PublicTool.EventCameraGo(mapTile.posID);
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
