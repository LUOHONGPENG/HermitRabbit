using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BattleOptionItem : MonoBehaviour
{
    public Button btnOption;
    public Text txOption;

    public void Init(string strOption,UnityAction action)
    {
        txOption.text = strOption;
        btnOption.onClick.RemoveAllListeners();
        btnOption.onClick.AddListener(action);
    }
}
