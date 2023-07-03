using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class InputMgr
{
    #region Touch

    private bool CheckWhetherHitUnit()
    {
        Physics.Raycast(GetMouseRay(), out RaycastHit hitData, 1000f, LayerMask.GetMask("Unit"));
        if (hitData.transform != null)
        {
            if (hitData.transform.parent.GetComponent<BattleCharacterView>() != null)
            {
                BattleCharacterView battleCharacter = hitData.transform.parent.GetComponent<BattleCharacterView>();
                EventCenter.Instance.EventTrigger("ShowBattleOption", battleCharacter.GetTypeID());
                Debug.Log("HitCharacter");
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
                Debug.Log("HitMap");
                return true;
            }
        }
        return false;
    }


    #endregion

    #region Touch Position
    private Vector2 GetMousePos()
    {
        Vector2 screenPosition = touchPositionAction.ReadValue<Vector2>();
        return screenPosition;
    }

    private Ray GetMouseRay()
    {
        Vector2 screenPosition = touchPositionAction.ReadValue<Vector2>();
        //Debug.Log(screenPosition);
        Ray ray = new Ray();
        if (GameMgr.Instance.curMapCamera != null)
        {
            ray = GameMgr.Instance.curMapCamera.ScreenPointToRay(screenPosition);
        }
        return ray;
    }
    #endregion
}
