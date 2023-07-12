using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButtonMgr : MonoBehaviour
{
    public GameObject objPopup;

    public Button btnEndTurn;

    public void Init()
    {
        btnEndTurn.onClick.RemoveAllListeners();
        btnEndTurn.onClick.AddListener(delegate ()
        {
            Debug.Log("EndTurn");
        });
    }
}
