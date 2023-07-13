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
        Debug.Log("ClickNoHit");
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
                DealClickCharacterAction(character);
                return true;
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
                DealClickMapAction(mapTile);
                return true;
            }
        }
        return false;
    }
    #endregion

    #region DealClickAction

    private void DealClickCharacterAction(BattleCharacterView character)
    {
        switch (interactState)
        {
            case InteractState.Normal:
                EventCenter.Instance.EventTrigger("ShowBattleOption", character.GetTypeID());
                break;
        }
    }

    private void DealClickMapAction(MapTileBase mapTile)
    {
        switch (interactState)
        {
            case InteractState.Normal:
                EventCenter.Instance.EventTrigger("CameraGoTo", mapTile.transform.position);
                break;
        }
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
