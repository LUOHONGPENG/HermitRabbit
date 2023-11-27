using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class PeaceMapClipUIMgr : MonoBehaviour
{
    public GameObject objPopup;

    public Transform tfMapClip;
    public GameObject pfMapClip;

    public Button btnClose;
    public Button btnSave;
    public GameObject objPath;

    private List<PeaceMapClipUIItem> listClip = new List<PeaceMapClipUIItem>();

    public void Init()
    {
        /*        btnClose.onClick.RemoveAllListeners();
                btnClose.onClick.AddListener(delegate ()
                {

                });*/

        btnSave.onClick.RemoveAllListeners();
        btnSave.onClick.AddListener(delegate ()
        {
            SaveFunction();
        });
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("RefreshMapClipUI", RefreshMapClipUIEvent);
    }


    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("RefreshMapClipUI", RefreshMapClipUIEvent);
    }

    public void ShowPopup()
    {
        PublicTool.ClearChildItem(tfMapClip);
        listClip.Clear();

        //Default
        GameObject objDefault= GameObject.Instantiate(pfMapClip, tfMapClip);
        PeaceMapClipUIItem itemDefault = objDefault.GetComponent<PeaceMapClipUIItem>();
        itemDefault.Init(-1);
        listClip.Add(itemDefault);
        //Clip
        for (int i = 0; i < PublicTool.GetGameData().listMapClipHeld.Count; i++)
        {
            int typeID = PublicTool.GetGameData().listMapClipHeld[i];
            GameObject objClip = GameObject.Instantiate(pfMapClip, tfMapClip);
            PeaceMapClipUIItem itemClip = objClip.GetComponent<PeaceMapClipUIItem>();
            itemClip.Init(typeID);
            listClip.Add(itemClip);
        }
        RefreshMapClipUsed();
        objPath.SetActive(false);
        objPopup.SetActive(true);
    }

    public void HidePopup()
    {
        objPopup.SetActive(false);
    }

    private void Update()
    {
        RefreshMapClipUI();
    }


    private void RefreshMapClipUI()
    {
        for(int i = 0;i< listClip.Count; i++)
        {
            if (PeaceMgr.Instance.mapClipTypeID == listClip[i].GetTypeID())
            {
                listClip[i].UpdateSelect(true);
            }
            else
            {
                listClip[i].UpdateSelect(false);
            }
        }
    }

    private void RefreshMapClipUsed()
    {
        for (int i = 0; i < listClip.Count; i++)
        {
            if (PublicTool.GetGameData().CheckWhetherClipUsed(listClip[i].GetTypeID()))
            {
                listClip[i].UpdateWhetherUsed(true);
            }
            else
            {
                listClip[i].UpdateWhetherUsed(false);
            }
        }
    }

    private void RefreshMapClipUIEvent(object arg0)
    {
        RefreshMapClipUsed();
    }



    public void SaveFunction()
    {
        PublicTool.RecalculateOccupancy();

        if (!CheckWhetherBlock())
        {
            RemovePlant();
            PublicTool.EventChangeInteract(InteractState.PeaceNormal);
            EventCenter.Instance.EventTrigger("PeaceMapEnd", null);
        }
        else
        {
            objPath.SetActive(true);

        }


    }


    public bool CheckWhetherBlock()
    {
        Vector2Int startPos = new Vector2Int(0, 0);
        Vector2Int endPos = new Vector2Int(0, GameGlobal.mapMaxNumY - 1);

        List<Vector2Int> listExistPos = new List<Vector2Int>();
        List<MapTileData> listMap = PublicTool.GetGameData().listMapTile;
        for (int i = 0; i < listMap.Count; i++)
        {
            listExistPos.Add(listMap[i].posID);
        }

        //Get Block Pos
        List<Vector2Int> listBlock = new List<Vector2Int>();
        for (int i = 0; i < PublicTool.GetGameData().listMapCurStonePos.Count; i++)
        {
            listBlock.Add(PublicTool.GetGameData().listMapCurStonePos[i]);
        }

        //Prepare the container for search
        Queue<Vector2Int> ququeOpen = new Queue<Vector2Int>();
        List<Vector2Int> listClose = new List<Vector2Int>();

        listClose.Clear();

        ququeOpen.Enqueue(startPos);
        while (ququeOpen.Count > 0)
        {

            Vector2Int checkPos = ququeOpen.Dequeue();
            Vector2Int tempPos;
            tempPos = checkPos + new Vector2Int(0, 1);
            if (listExistPos.Contains(tempPos) && !listBlock.Contains(tempPos) && !ququeOpen.Contains(tempPos) && !listClose.Contains(tempPos))
            {
                ququeOpen.Enqueue(tempPos);
            }

            tempPos = checkPos + new Vector2Int(0, -1);
            if (listExistPos.Contains(tempPos) && !listBlock.Contains(tempPos) && !ququeOpen.Contains(tempPos) && !listClose.Contains(tempPos))
            {
                ququeOpen.Enqueue(tempPos);
            }

            tempPos = checkPos + new Vector2Int(1, 0);
            if (listExistPos.Contains(tempPos) && !listBlock.Contains(tempPos) && !ququeOpen.Contains(tempPos) && !listClose.Contains(tempPos))
            {
                ququeOpen.Enqueue(tempPos);
            }

            tempPos = checkPos + new Vector2Int(-1, 0);
            if (listExistPos.Contains(tempPos) && !listBlock.Contains(tempPos) && !ququeOpen.Contains(tempPos) && !listClose.Contains(tempPos))
            {
                ququeOpen.Enqueue(tempPos);
            }

            listClose.Add(checkPos);
        }

        if (listClose.Contains(endPos))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void RemovePlant()
    {
        GameData gameData = PublicTool.GetGameData();
        List<Vector2Int> listBlock = new List<Vector2Int>();
        for (int i = 0; i < gameData.listMapCurStonePos.Count; i++)
        {
            listBlock.Add(gameData.listMapCurStonePos[i]);
        }

        List<BattlePlantData> listPlant = gameData.listPlant;

        for(int i = 0; i < listPlant.Count; i++)
        {
            BattlePlantData plantData = listPlant[i];
            if (listBlock.Contains(plantData.posID))
            {
                if (plantData != null)
                {
                    GameMgr.Instance.curSceneGameMgr.levelMgr.unitViewMgr.RemovePlantView(plantData.keyID);
                    gameData.RemovePlantData(plantData.keyID);
                    PublicTool.RecalculateOccupancy();
                    EventCenter.Instance.EventTrigger("RefreshResourceUI", null);
                }
            }
        }

    }
}
