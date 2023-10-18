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

    public void Init()
    {
        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(delegate ()
        {

        });
    }

    public void ShowPopup()
    {
        PublicTool.ClearChildItem(tfMapClip);
        for(int i = 0; i < PublicTool.GetGameData().listMapClipHeld.Count; i++)
        {
            GameObject objClip = GameObject.Instantiate(pfMapClip, tfMapClip);
            PeaceMapClipUIItem itemClip = objClip.GetComponent<PeaceMapClipUIItem>();
            
        }
        objPopup.SetActive(true);
    }

    public void HidePopup()
    {
        objPopup.SetActive(false);
    }
}
