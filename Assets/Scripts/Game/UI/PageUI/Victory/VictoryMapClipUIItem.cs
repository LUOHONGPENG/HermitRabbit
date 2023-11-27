using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryMapClipUIItem : MonoBehaviour
{
    public Image imgMapClip;
    public Button btnMapClip;

    public MapClipDisplayUI mapClipDisplayUI;

    public Text codeRarity;
    public Image imgBG;
    public List<Color> listTxColor = new List<Color>();
    public List<Sprite> listSpRare = new List<Sprite>();


    private int typeID = -1;
    private VictoryUIMgr parent;

    public void Init(int typeID)
    {
        this.typeID = typeID;

        MapClipExcelItem mapClipExcelItem = PublicTool.GetMapClipItem(typeID);

        mapClipDisplayUI.Init(typeID);

        codeRarity.text = mapClipExcelItem.rarity.ToString();

        switch (mapClipExcelItem.rarity)
        {
            case Rarity.Common:
                imgBG.sprite = listSpRare[0];
                codeRarity.color = listTxColor[0];
                break;
            case Rarity.UnCommon:
                imgBG.sprite = listSpRare[1];
                codeRarity.color = listTxColor[1];
                break;
            case Rarity.Rare:
                imgBG.sprite = listSpRare[2];
                codeRarity.color = listTxColor[2];
                break;
            case Rarity.Epic:
                imgBG.sprite = listSpRare[3];
                codeRarity.color = listTxColor[3];
                break;
            case Rarity.Legendary:
                imgBG.sprite = listSpRare[4];
                codeRarity.color = listTxColor[4];
                break;
        }

        btnMapClip.onClick.RemoveAllListeners();
        btnMapClip.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("VictoryAddMapClip", typeID);
        });
    }

}
