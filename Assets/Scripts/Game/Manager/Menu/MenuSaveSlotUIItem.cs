using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSaveSlotUIItem : MonoBehaviour
{
    public Text txSlotName;
    public Button btnLoadSlot;
    public GameObject objNull;


    public void Init()
    {
        btnLoadSlot.onClick.RemoveAllListeners();
        btnLoadSlot.onClick.AddListener(delegate ()
        {

        });


    }



}
