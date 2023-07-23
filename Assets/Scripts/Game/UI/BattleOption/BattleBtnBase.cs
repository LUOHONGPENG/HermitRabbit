using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BattleBtnBase : MonoBehaviour
{
    public Outline outlineSelect;
    public Button btnAction;

    public void RefreshOnSelect()
    {
        outlineSelect.enabled = true;
    }

    public void RefreshOffSelect()
    {
        outlineSelect.enabled = false;
    }

    public void InitButton(UnityAction action)
    {
        btnAction.onClick.RemoveAllListeners();
        btnAction.onClick.AddListener(action);
    }
}
