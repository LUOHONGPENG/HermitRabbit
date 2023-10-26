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

    public InteractState GetInteractState()
    {
        return interactState;
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

        switch (interactState)
        {
            case InteractState.BattleNormal:
                EventCenter.Instance.EventTrigger("InputChooseCharacter", character.GetTypeID());
                return true;
        }
        return false;
    }

    private bool DealClickMapAction(MapTileBase mapTile)
    {
        GameData gameData = PublicTool.GetGameData();
        switch (interactState)
        {
            case InteractState.PeaceNormal:
                PublicTool.EventCameraGoPosID(mapTile.posID);
                return true;
            case InteractState.PeacePlant:
                EventCenter.Instance.EventTrigger("InputModifyPlant", mapTile.posID);
                return true;
            case InteractState.PeaceMap:
                Vector2Int mapClipClickID = PublicTool.ConvertMapTileIDToClip(mapTile.posID);
                if (mapClipClickID.x >= 0)
                {
                    EventCenter.Instance.EventTrigger("InputSetMapClip", mapClipClickID);
                    return true;
                }
                break;
            case InteractState.BattleNormal:
                PublicTool.EventCameraGoPosID(mapTile.posID);
                return true;
            case InteractState.CharacterMove:
                EventCenter.Instance.EventTrigger("InputMoveAction", mapTile.posID);
                return true;
            case InteractState.CharacterSkill:
                EventCenter.Instance.EventTrigger("InputSkillAction", mapTile.posID);
                return true;
        }

        return false;
    }
    #endregion

    #region Touch Position
    public Vector2 GetMousePos()
    {
        Vector2 screenPosition = touchPositionAction.ReadValue<Vector2>();

        return screenPosition;
    }

    public Vector2 GetMousePosUI()
    {
        return GameMgr.Instance.curUICamera.ScreenToWorldPoint(GetMousePos());

    }

    private Ray GetMouseRay()
    {
        Vector2 screenPosition = GetMousePos();
        Ray ray = new Ray();
        if (GameMgr.Instance.curMapCamera != null)
        {
            ray = GameMgr.Instance.curMapCamera.ScreenPointToRay(screenPosition);
        }
        return ray;
    }


    #endregion
}
