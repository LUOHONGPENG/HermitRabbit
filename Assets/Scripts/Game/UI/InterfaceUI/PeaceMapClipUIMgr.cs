using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeaceMapClipUIMgr : MonoBehaviour
{
    public GameObject objPopup;

    public Transform tfMapClip;
    public GameObject pfMapClip;

    public Button btnClose;

    private List<PeaceMapClipUIItem> listClip = new List<PeaceMapClipUIItem>();

    public void Init()
    {
        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(delegate ()
        {

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

}
