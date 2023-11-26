using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BattleBtnBase : MonoBehaviour
{
    public Image imgSelected;
    public Button btnAction;

    public void RefreshOnSelect()
    {
        imgSelected.gameObject.SetActive(true);
    }

    public void RefreshOffSelect()
    {
        imgSelected.gameObject.SetActive(false);
    }

    public void InitButton(UnityAction action)
    {
        btnAction.onClick.RemoveAllListeners();
        btnAction.onClick.AddListener(action);
    }
}
