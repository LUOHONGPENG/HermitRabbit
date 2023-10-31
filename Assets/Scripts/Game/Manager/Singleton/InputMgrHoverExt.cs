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
        if(GameMgr.Instance.curSceneName == SceneName.Game || GameMgr.Instance.curSceneName == SceneName.Test)
        {
            CheckRayHoverAll();
        }
    }



    #region Hover



    /// <summary>
    /// The main function of 
    /// </summary>
    private void CheckRayHoverAll()
    {
        //UI
        if (CheckWhetherHoverUI())
        {
            //Not hovering any map tile
            CancelHoverMapTile();
            //GetRayUI
            UpdateMouseRayUI();
            CheckMouseRayUI();
            return;
        }
        else
        {
            //May Need Modify Later
            EventCenter.Instance.EventTrigger("HideUITip", null);
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
                    EventCenter.Instance.EventTrigger("InputSetHoverTile", itemMapTile.posID);
                    return true;
                }
            }
        }
        return false;
    }

    private void CancelHoverMapTile()
    {
        EventCenter.Instance.EventTrigger("InputSetHoverTile", new Vector2Int(-99, -99));
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

    //For RayCastUI
    private List<RaycastResult> raycastResults = new List<RaycastResult>();
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
    }

    private void CheckMouseRayUI()
    {
        foreach (RaycastResult item in raycastResults)
        {
            if (item.gameObject.tag == "SkillNodeUI")
            {
                if (item.gameObject.transform.parent.GetComponent<SkillNodeUIItem>() != null)
                {
                    SkillNodeUIItem nodeUI = item.gameObject.transform.parent.GetComponent<SkillNodeUIItem>();
                    UITipInfo uiTipInfo = new UITipInfo(UITipType.SkillNode, nodeUI.GetNodeID(), -1, GetMousePosUI());
                    EventCenter.Instance.EventTrigger("ShowUITip", uiTipInfo);
                    return;
                }
            }
            else if (item.gameObject.tag == "SkillButtonUI")
            {
                if (item.gameObject.GetComponent<BattleSkillBtnItem>() != null)
                {
                    BattleSkillBtnItem buttonUI = item.gameObject.transform.GetComponent<BattleSkillBtnItem>();
                    UITipInfo uiTipInfo = new UITipInfo(UITipType.SkillButton, buttonUI.GetSkillBtnID(), buttonUI.GetSkillBtnCharacterID(), GetMousePosUI());
                    EventCenter.Instance.EventTrigger("ShowUITip", uiTipInfo);
                    return;
                }
            }
        }
        EventCenter.Instance.EventTrigger("HideUITip", null);
    }
    #endregion
}
