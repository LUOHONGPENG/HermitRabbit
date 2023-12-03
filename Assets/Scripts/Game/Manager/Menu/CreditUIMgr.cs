using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditUIMgr : MonoBehaviour
{
    public GameObject objPopup;

    public Button btnClose;

    public void Init()
    {
        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(delegate ()
        {
            objPopup.SetActive(false);
        });
    }

    public void ShowPopup()
    {
        objPopup.SetActive(true);
    }
}
