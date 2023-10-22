using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryMapClipUIItem : MonoBehaviour
{
    public Image imgMapClip;
    public Button btnMapClip;

    public MapClipDisplayUI mapClipDisplayUI;

    private int typeID = -1;
    private VictoryUIMgr parent;

    public void Init(int typeID)
    {
        this.typeID = typeID;

        mapClipDisplayUI.Init(typeID);

        btnMapClip.onClick.RemoveAllListeners();
        btnMapClip.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("VictoryAddMapClip", typeID);
        });
    }

}
