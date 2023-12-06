using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BattleBtnBase : MonoBehaviour
{
    public Image imgSelected;
    public Image imgIcon;
    public Image imgFrame;
    public Button btnAction;

    public List<Color> listColorSelected;

    public void RefreshOnSelect()
    {
        imgSelected.gameObject.SetActive(true);
        imgIcon.color = listColorSelected[1];
        imgFrame.color = listColorSelected[1];
    }

    public void RefreshOffSelect()
    {
        imgSelected.gameObject.SetActive(false);
        imgIcon.color = listColorSelected[0];
        imgFrame.color = listColorSelected[0];
    }

    public void InitButton(UnityAction action)
    {
        btnAction.onClick.RemoveAllListeners();
        btnAction.onClick.AddListener(action);
    }
}
