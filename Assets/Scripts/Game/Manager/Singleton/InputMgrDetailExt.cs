using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public partial class InputMgr
{
    public void FixedUpdate()
    {
        if (!isInitInput)
        {
            return;
        }
        CheckRayHover();
    }

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
        Ray ray = new Ray();
        if (GameMgr.Instance.curMapCamera != null)
        {
            ray = GameMgr.Instance.curMapCamera.ScreenPointToRay(screenPosition);
        }
        return ray;
    }
    #endregion

    #region Hover

    //For RayCastUI
/*    private List<RaycastResult> raycastResults = new List<RaycastResult>();
    private void UpdateMouseRayUI()
    {
        //Mouse
        if (EventSystem.current == null)
        {
            return;
        }
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = GetMousePos();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
    }*/

    private void CheckRayHover()
    {
        Ray ray = GetMouseRay();
        //Mouse above Map
        if (Physics.Raycast(ray, out RaycastHit hitDataMap, 999f, LayerMask.GetMask("Map")))
        {
            if (hitDataMap.transform != null)
            {
                if (hitDataMap.transform.parent.parent.GetComponent<MapTileBase>() != null)
                {
                    MapTileBase itemMapTile = hitDataMap.transform.parent.parent.GetComponent<MapTileBase>();
                    EventCenter.Instance.EventTrigger("SetTargetTile", itemMapTile.posID);
                    return;
                }
            }
        }

        EventCenter.Instance.EventTrigger("SetTargetTile", new Vector2Int(-99, -99));
    }
    #endregion
}
