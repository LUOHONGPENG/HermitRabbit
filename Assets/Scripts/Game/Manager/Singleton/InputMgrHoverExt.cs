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
        CheckRayHoverAction();
    }



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

    /// <summary>
    /// The main function of 
    /// </summary>
    private void CheckRayHoverAction()
    {
        //UI
        if (CheckWhetherHoverUI())
        {
            //Not hovering any map tile
            CancelHoverMapTile();
            return;
        }

        Ray ray = GetMouseRay();
        //Cast Map Tile
        if (!CheckWhetherHoverMapTile(ray))
        {
            CancelHoverMapTile();
        }
    }

    /// <summary>
    /// Check whether the mouse hover the map tile
    /// </summary>
    /// <param name="ray"></param>
    /// <returns></returns>
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

    private void CancelHoverMapTile()
    {
        EventCenter.Instance.EventTrigger("SetHoverTile", new Vector2Int(-99, -99));
    }


    /// <summary>
    /// Check whether the mouse hover the UI
    /// </summary>
    /// <returns></returns>
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
