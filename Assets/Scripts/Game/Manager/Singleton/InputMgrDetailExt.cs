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
                Debug.Log("HitCharacter ATK" + battleCharacter.characterData.curATK);
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
                EventCenter.Instance.EventTrigger("CameraGoTo", mapTile.transform.position);
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
        if (CheckWhetherHoverUI())
        {
            EventCenter.Instance.EventTrigger("SetHoverTile", new Vector2Int(-99, -99));
            return;
        }

        Ray ray = GetMouseRay();
        if (!CheckWhetherHoverMapTile(ray))
        {
            EventCenter.Instance.EventTrigger("SetHoverTile", new Vector2Int(-99, -99));
        }
    }

    private bool CheckWhetherHoverMapTile(Ray ray)
    {
        //Mouse above MapTile
        if (Physics.Raycast(ray, out RaycastHit hitDataMap, 999f, LayerMask.GetMask("Map")))
        {
            if (hitDataMap.transform != null)
            {
                if (hitDataMap.transform.parent.parent.GetComponent<MapTileBase>() != null)
                {
                    MapTileBase itemMapTile = hitDataMap.transform.parent.parent.GetComponent<MapTileBase>();
                    EventCenter.Instance.EventTrigger("SetHoverTile", itemMapTile.posID);
                    return true;
                }
            }
        }
        return false;
    }


    private bool CheckWhetherHoverUI()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }

    #endregion
}
